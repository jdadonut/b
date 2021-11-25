using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace b.Extensions
{
    public static class IterateByteArray
    {
        public static IEnumerable<byte[]> Iterate(this byte[] byteArr, int chunkSize)
        {
            return byteArr.Chunk(chunkSize);
        }
    }
}