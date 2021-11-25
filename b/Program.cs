// See https://aka.ms/new-console-template for more information

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using b.Interface;
using b.Util;


namespace b
{
    public static class Program
    {
        
        public static void Main(string[] args)
        {
            Console.Clear();
            KittyImageWriter.WriteImage("/home/jai/Downloads/catgirl_laying.png");
        }
    }
}