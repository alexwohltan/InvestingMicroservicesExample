using System;
namespace DataStructures.Benchmark
{
	public class IndustryBenchmark : Benchmark
	{
        public virtual string MarketName { get; set; }
        public virtual string SectorName { get; set; }
        public virtual string IndustryName { get; set; }

        public override string ToString()
        {
            return "Industry Benchmark: " + MarketName + " > " + SectorName + " > " + IndustryName;
        }
    }
}

