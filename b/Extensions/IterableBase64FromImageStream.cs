using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using b;
using System.Text;
using System.Threading;
using b.Util;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace b.Extensions
{
    public static class IterableBase64FromImageStream
    {
        public static IEnumerable<byte[]> GetBase64ByteEnumerable(this Stream stream, int chunkSize)
        {

            Image img = Image.Load(stream);
            
            int image_x = img.Width;
            int image_y = img.Height;
            int maxDim_x = (int)(WindowUtil.WindowSizeX * 0.9);
            int maxDim_y = (int)(WindowUtil.WindowSizeY * 0.8);
            
            
            if ((image_x > maxDim_x || image_y > maxDim_y) )
            {
                if (image_x == image_y)
                    if (maxDim_x < maxDim_y)
                        img.Mutate(x => x.Resize(maxDim_x, 0));
                    else
                        img.Mutate(x => x.Resize(maxDim_y, 0));
                /*
                 * differential key
                 * diff * aspect_dir
                 * assuming dir = x
                 * diff = image_x - maxDim_x
                 * aspect = x:y -> float (if dir = y, y:x -> float)
                 */
                

                int diffkey_x = (image_x - maxDim_x) * (image_x / image_y);
                int diffkey_y = (image_y - maxDim_y) * (image_y / image_x);
                if (diffkey_x < diffkey_y)
                    img.Mutate(x => x.Resize(0, maxDim_y, KnownResamplers.Lanczos2));
                else
                    img.Mutate(x => x.Resize(maxDim_x, 0, KnownResamplers.Lanczos2));
            

            }
            
            MemoryStream asPng = new ();
            img.SaveAsPng(asPng);
            return Encoding.UTF8.GetBytes(
                Convert.ToBase64String(
                    asPng.ToArray()
                )
            ).Iterate(chunkSize);
            
        }
    }
}