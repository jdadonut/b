using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using b.Extensions;
using b.Interface.Kitty;
using b.Objects;
using SixLabors.ImageSharp;

namespace b.Interface
{
    public class Gallery
    {
        public List<ConsoleKey>? ValidKeys = new List<ConsoleKey>()
        {
            ConsoleKey.LeftArrow, // Same meaning as Z
            ConsoleKey.DownArrow, // Same meaning as X
            ConsoleKey.RightArrow, // Same meaning as C
            ConsoleKey.Z,
            ConsoleKey.X,
            ConsoleKey.C,
            ConsoleKey.Enter,
            ConsoleKey.Spacebar // Same meaning as Enter
            
        };

        private Select CurrentlySelected = Select.Next; 
        private List<IImagePost> cachedPosts = new List<IImagePost>();
        public Gallery(IService service, params string[] args)
        {
            IEnumerator<IImagePost> enumerator = service.GetEnumerator(args);
            enumerator.MoveNext();
            var Current = enumerator.Current;
            while (true)
            {
                DisplayPost(Current, Select.Next);
                cachedPosts.Add(Current);
                ConsoleKey? key = null;
                while (!ValidKeys.Contains(key.HasValue ? key.Value : ConsoleKey.Attention))
                {
                    key = Console.ReadKey(true).Key;
                }

                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.Z:
                        DisplayPost(Current, Select.Previous);
                        break;
                    case ConsoleKey.X:
                    case ConsoleKey.DownArrow:
                        DisplayPost(Current, Select.Options);
                        break;
                    case ConsoleKey.C:
                    case ConsoleKey.RightArrow:
                        DisplayPost(Current, Select.Next);
                        break;
                    default:
                        switch (CurrentlySelected)
                        {
                            case Select.Next:
                                enumerator.MoveNext();
                                Current = enumerator.Current;
                                continue;
                            case Select.Options:
                                DisplayOptionsMenu(Current);
                                break;
                            case Select.Previous:
                                cachedPosts.RemoveAt(cachedPosts.Count-1);
                                Current = cachedPosts[^1];
                                cachedPosts.RemoveAt(cachedPosts.Count-1);
                                continue;
                            default:
                                break;
                        }
                        break;

                }
            }
            
        }

        private void DisplayOptionsMenu(IImagePost current)
        {
            throw new NotImplementedException();
        }

        private void DisplayPost(IImagePost current, Select next)
        {
            CurrentlySelected = next;
            Stream img = current.GetImage(default);
            Console.Clear();
            Console.Write(" ".Times((int) (Console.WindowWidth / 2 - ((current.PostTitle.Length + 2) / 2))));
            Console.Write("[" + current.PostTitle + "]\n");
            KittyImageWriter.WriteImageStream(img);
            int RemainingLines = Console.WindowHeight - Console.GetCursorPosition().Top;
            Console.Write("\n".Times(RemainingLines/3));
            
            int Width3 = Console.WindowWidth / 3;
            Console.Write(" ".Times(Width3 - "[[Z]Previous]".Length / 2));
            if (next == Select.Previous)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            Console.Write("[[Z]Previous");
            if (next == Select.Previous)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.Write(" ".Times((int) (Width3 * 2.5) - "[[C]Next]".Length / 2));
            if (next == Select.Next)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            Console.Write("[[C]Next");
            if (next == Select.Next)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.Write('\n');
            Console.Write(" ".Times((int)(Width3 * 1.5) - "[[X]Options]".Length / 2));
            if (next == Select.Options)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            Console.Write("[[X]Options");
            if (next == Select.Options)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
            


        }
    }
}