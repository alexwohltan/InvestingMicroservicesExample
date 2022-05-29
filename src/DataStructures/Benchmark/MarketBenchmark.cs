using System;
namespace DataStructures.Benchmark
{
	public class MarketBenchmark : Benchmark
	{
        public virtual string MarketName { get; set; }

        public override string ToString()
        {
            return "Market Benchmark: " + MarketName;
        }
    }
}

