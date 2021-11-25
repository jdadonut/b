using System.IO;
using System;
using System.Text;
using b.Extensions;

namespace b.Interface
{
    public static class KittyImageWriter
    {
        public static void WriteImage(string filePath)
        {
            Stream stdout = Console.OpenStandardOutput();
            using var f = File.Open(filePath, FileMode.Open, FileAccess.Read);
            string controlKey; // \\033
            byte[] last = Array.Empty<byte>();
            bool first = false;
            bool flushed = false;
            foreach (byte[] payloadBuffer in f.GetBase64ByteEnumerable(4096) ) // asserts it exists
            {
                // write payload to stream, then push payload to stdout (and flush?)
                if (last.Length == 0) // Checks if payloadBuffer is first payload
                {
                    first = true;
                    last = payloadBuffer;
                    continue;
                }
                stdout.WriteByte(0x1B);
                stdout.Write(Encoding.UTF8.GetBytes("_G"));
                if (first)
                {
                    first = false;
                    stdout.Write(Encoding.UTF8.GetBytes("a=T,f=100,m=1;")); // headers for graphics protocol first payload
                }
                else
                {
                    stdout.Write(Encoding.UTF8.GetBytes("m=1;"));
                }
                stdout.Write(last);
                stdout.WriteByte(0x1B);
                stdout.Write(Encoding.UTF8.GetBytes("\\\n"));
                last = payloadBuffer;
                stdout.Flush();
                flushed = true;
            }
            // deal with last payload

            stdout.WriteByte(0x1B);
            stdout.Write(Encoding.UTF8.GetBytes("_G"));
            if (!flushed) stdout.Write(Encoding.UTF8.GetBytes("a=T,f=100,m=0;")); // headers for graphics protocol first payload
            else stdout.Write(Encoding.UTF8.GetBytes("m=1;"));
            stdout.Write(last);
            stdout.WriteByte(0x1B);
            stdout.Write(Encoding.UTF8.GetBytes("\\\n"));
            stdout.Flush();

        }
    }
}