namespace SerializersBenchmark
{
    using System.Collections.Generic;

    using Application.DTOs;

    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Order;

    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
    [MemoryDiagnoser]
    public class Dictionaries
    {
        [Benchmark(Baseline = true)]
        public void Small_Dictionary_DynamicCapacity()
        {
            int size = 100;
            var dict = new Dictionary<int, Merchant>();
            for (int i = 0; i < size; i++)
            {
                dict.Add(i, new Merchant { MerchantId = i });
            }
        }

        [Benchmark]
        public void Small_Dictionary_PlannedCapacity()
        {
            int size = 100;
            var dict = new Dictionary<int, Merchant>(size);
            for (int i = 0; i < size; i++)
            {
                dict.Add(i, new Merchant { MerchantId = i });
            }
        }

        [Benchmark]
        public void Large_Dictionary_DynamicCapacity()
        {
            int size = 5000;
            var dict = new Dictionary<int, Merchant>();
            for (int i = 0; i < size; i++)
            {
                dict.Add(i, new Merchant { MerchantId = i });
            }
        }

        [Benchmark]
        public void Large_Dictionary_PlannedCapacity()
        {
            int size = 5000;
            var dict = new Dictionary<int, Merchant>(size);
            for (int i = 0; i < size; i++)
            {
                dict.Add(i, new Merchant { MerchantId = i });
            }
        }

        [Benchmark]
        public void Large_Dictionary_BelowPlannedCapacity()
        {
            int size = 100;
            var dict = new Dictionary<int, Merchant>(size);
            for (int i = 0; i < 5000; i++)
            {
                dict.Add(i, new Merchant { MerchantId = i });
            }
        }
    }
}
