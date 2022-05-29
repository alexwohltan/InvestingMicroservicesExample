using System;
namespace DataStructures.Benchmark
{
	public class SectorBenchmark : Benchmark
	{
        public virtual string MarketName { get; set; }
        public virtual string SectorName { get; set; }

        public override string ToString()
        {
            return "Sector Benchmark: " + MarketName + " > " + SectorName;
        }
    }
}

