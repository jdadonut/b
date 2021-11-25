using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace b.Util
{
    
    // TODO: Interop to support windows using ioctl in kernel32.dll, for now only supports linux (kitty terminal) due to this.
    public static class WindowUtil
    {
        public static int WindowSizeX;
        public static int WindowSizeY;
        
        static WindowUtil()
        {
            Update();
        }

        public static void Update()
        {
            Stream stdout = Console.OpenStandardOutput();
            Stream stdin = Console.OpenStandardInput();
            stdout.WriteByte(0x1B);
            stdout.Write(Encoding.UTF8.GetBytes("[14t"));
            stdout.Flush();
            ConsoleKeyInfo t = default;
            string dimensions = String.Empty;
            while (true)
            {
                t = Console.ReadKey(true);
                if (t.KeyChar == 't') break;
                dimensions += t.KeyChar;
            }
            List<string> dim = dimensions.Split(";").ToList();
            WindowSizeX = int.Parse(dim[1]);
            WindowSizeY = int.Parse(dim[2]);
        }
    }
}