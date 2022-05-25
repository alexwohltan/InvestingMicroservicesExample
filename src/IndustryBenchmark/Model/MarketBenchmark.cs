using System;
namespace Benchmark
{
	public class MarketBenchmark : Benchmark
	{
        public string MarketName { get; set; }

        public MarketBenchmark()
            : base()
        {

        }
        public MarketBenchmark(string marketName)
            :base()
        {
            MarketName = marketName;
        }

        public override string ToString()
        {
            return "Market Benchmark: " + MarketName;
        }
    }
}

