// See https://aka.ms/new-console-template for more information
using b.Interop;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace b.Benchmark
{
    public static class Program
    {
        public static void Main()
        {
            var summary = BenchmarkRunner.Run<Benchmarkables>( new DebugBuildConfig());
        }

        
        
    }

    public class Benchmarkables
    {
        [Benchmark]
        public void GetConsoleWindowSize()
        {
            new WindowUtil();
        }
    }
}