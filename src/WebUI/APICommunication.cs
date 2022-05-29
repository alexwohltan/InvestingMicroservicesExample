namespace WebUI
{
	public class APICommunication
	{
		public FundamentalDataClient FundamentalDataClient { get; set; }
		public BenchmarkClient.BenchmarkClient BenchmarkClient { get; set; }

        public APICommunication()
        {
			FundamentalDataClient = new FundamentalDataClient("localhost", 60958, "api/");
			BenchmarkClient = new BenchmarkClient.BenchmarkClient("localhost", 7126, "api/");
        }

		public async Task<IEnumerable<Market>> GetMarketsForIndexView()
        {
			var marketsData = await FundamentalDataClient.GetMarkets();

			if (marketsData == null)
				return null;

			return marketsData;
        }

		public async Task<MarketViewModel> GetMarketForMarketView(string marketName)
		{
			var marketData = await FundamentalDataClient.GetMarket(marketName);

			if (marketData == null)
				return null;

			var benchmarkData = await BenchmarkClient.GetMarketBenchmark(marketName);

			var result = new MarketViewModel()
			{
				Market = marketData,
				MarketBenchmark = benchmarkData
			};

			return result;
		}

		public async Task<SectorViewModel> GetSectorForSectorView(string marketName, string sectorName)
		{
			var sectorData = await FundamentalDataClient.GetSector(marketName, sectorName);

			if (sectorData == null)
				return null;

			var benchmarkData = await BenchmarkClient.GetSectorBenchmark(marketName, sectorName);

			var result = new SectorViewModel()
			{
				Sector = sectorData,
				SectorBenchmark = benchmarkData
			};

			return result;
		}

		public async Task<IndustryViewModel> GetIndustryForIndustryView(string marketName, string sectorName, string industryName)
        {
			var industryData = await FundamentalDataClient.GetIndustry(marketName, sectorName, industryName);

			if (industryData == null)
				return null;

			var benchmarkData = await BenchmarkClient.GetIndustryBenchmark(marketName, sectorName, industryName);

			var result = new IndustryViewModel()
			{
				Industry = industryData,
				IndustryBenchmark = benchmarkData
			};

			return result;
		}
	}
}

