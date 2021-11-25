using System.IO;
using System;
using System.Text;
using b.Extensions;
using b;
using b.Util;

namespace b.Interface
{
    public static class KittyImageWriter
    {
        public static void WriteImage(string filePath)
        {
            WindowUtil.Update();
            // Stream stdout = File.OpenWrite("code_output.txt");
            Stream stdout = Console.OpenStandardOutput();
            using var f = File.Open(filePath, new FileStreamOptions());
            byte[] last = Array.Empty<byte>();
            bool first = false;
            bool flushed = false;
            byte[] ControlKey = new byte[]{0x1B};
            // byte[] ControlKey = Encoding.UTF8.GetBytes("033");
            foreach (byte[] payloadBuffer in f.GetBase64ByteEnumerable(4096) ) // asserts it exists
            {
                // write payload to stream, then push payload to stdout (and flush?)
                if (last.Length == 0) // Checks if payloadBuffer is first payload
                {
                    first = true;
                    last = payloadBuffer;
                    continue;
                }
                stdout.Write(ControlKey);
                stdout.Write(Encoding.UTF8.GetBytes("_G"));
                if (first)
                {
                    first = false;
                    stdout.Write(Encoding.UTF8.GetBytes("m=1,a=T,f=100;")); // headers for graphics protocol first payload
                }
                else
                {
                    stdout.Write(Encoding.UTF8.GetBytes("m=1;"));
                }
                stdout.Write(last);
                stdout.Write(ControlKey);
                stdout.Write(Encoding.UTF8.GetBytes("\\"));
                last = payloadBuffer;
                stdout.Flush();
                flushed = true;
            }
            // deal with last payload

            stdout.Write(ControlKey);
            stdout.Write(Encoding.UTF8.GetBytes("_G"));
            if (!flushed) stdout.Write(Encoding.UTF8.GetBytes("a=T,f=100,m=0;")); // headers for graphics protocol first payload
            else stdout.Write(Encoding.UTF8.GetBytes("m=0;"));
            stdout.Write(last);
            stdout.Write(ControlKey);
            stdout.Write(Encoding.UTF8.GetBytes("\\"));
            stdout.Flush();

        }
    }
}