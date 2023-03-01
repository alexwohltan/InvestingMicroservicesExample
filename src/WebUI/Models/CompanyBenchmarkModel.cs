using System;
namespace WebUI.Models
{
	public class CompanyBenchmarkModel
	{
        public DataStructures.FundamentalData.Company CompanyData { get; set; }

        public DataStructures.Benchmark.IndustryBenchmark? IndustryData { get; set; }
        public DataStructures.Benchmark.SectorBenchmark? SectorData { get; set; }
        public DataStructures.Benchmark.MarketBenchmark? MarketData { get; set; }
    }
}

