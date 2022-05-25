using System;
namespace Benchmark
{
	public class IndustryBenchmark : Benchmark
	{
        public string MarketName { get; set; }
        public string SectorName { get; set; }
        public string IndustryName { get; set; }

        public IndustryBenchmark()
            : base()
        {

        }
        public IndustryBenchmark(string marketName, string sectorName, string industryName)
            : base()
        {
            MarketName = marketName;
            SectorName = sectorName;
            IndustryName = industryName;
        }

        public override string ToString()
        {
            return "Industry Benchmark: " + MarketName + " > " + SectorName + " > " + IndustryName;
        }
    }
}

