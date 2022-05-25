using System;
namespace Benchmark
{
	public class SectorBenchmark : Benchmark
	{
        public string MarketName { get; set; }
        public string SectorName { get; set; }

        public SectorBenchmark()
            : base()
        {

        }
        public SectorBenchmark(string marketName, string sectorName)
            : base()
        {
            MarketName = marketName;
            SectorName = sectorName;
        }

        public override string ToString()
        {
            return "Sector Benchmark: " + MarketName + " > " + SectorName;
        }
    }
}

