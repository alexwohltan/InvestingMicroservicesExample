using System;
namespace WebUI
{
	public class IndustryViewModel
	{
        public Industry Industry { get; set; }
        public IndustryBenchmark IndustryBenchmark { get; set; }

        public string MarketName => IndustryBenchmark.MarketName;
        public string SectorName => IndustryBenchmark.SectorName;
        public string IndustryName => IndustryBenchmark.IndustryName;
    }
}

