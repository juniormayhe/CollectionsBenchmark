namespace SerializersBenchmark
{
    using BenchmarkDotNet.Running;

    using System;

    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Lists>();
            //BenchmarkRunner.Run<Dictionaries>();
            //BenchmarkRunner.Run<Arrays>();

            Console.WriteLine("done!");
            Console.ReadLine();
        }
    }
}
