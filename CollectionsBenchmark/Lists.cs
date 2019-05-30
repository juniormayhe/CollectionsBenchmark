namespace SerializersBenchmark
{
    using System.Collections.Generic;

    using Application.DTOs;

    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Order;

    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
    [MemoryDiagnoser]
    public class Lists
    {
        [Benchmark(Baseline = true)]
        public void Small_List_DynamicCapacity()
        {
            int size = 100;
            var list = new List<Merchant>();
            for (int i = 0; i < size; i++)
            {
                list.Add(new Merchant { MerchantId = i });
            }
        }

        [Benchmark]
        public void Small_List_PlannedCapacity()
        {
            int size = 100;
            var list = new List<Merchant>(size);
            for (int i = 0; i < size; i++)
            {
                list.Add(new Merchant { MerchantId = i });
            }
        }

        [Benchmark]
        public void Large_List_DynamicCapacity()
        {
            int size = 5000;
            var list = new List<Merchant>();
            for (int i = 0; i < size; i++)
            {
                list.Add(new Merchant { MerchantId = i });
            }
        }

        [Benchmark]
        public void Large_List_PlannedCapacity()
        {
            int size = 5000;
            var list = new List<Merchant>(size);
            for (int i = 0; i < size; i++)
            {
                list.Add(new Merchant { MerchantId = i });
            }
        }

        [Benchmark]
        public void Large_List_BelowPlannedCapacity()
        {
            int size = 100;
            var list = new List<Merchant>(size);
            for (int i = 0; i < 5000; i++)
            {
                list.Add(new Merchant { MerchantId = i });
            }
        }
    }
}
