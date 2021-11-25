using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SixLabors.ImageSharp;

namespace b.Extensions
{
    public static class IterableBase64FromImageStream
    {
        public static IEnumerable<byte[]> GetBase64ByteEnumerable(this Stream stream, int chunkSize)
        {

            Image img = Image.Load(stream);
            MemoryStream asPng = new ();
            img.SaveAsPng(asPng);
            return Encoding.ASCII.GetBytes(
                Convert.ToBase64String(
                    asPng.ToArray()
                )
            ).Iterate(chunkSize);
            
        }
    }
}