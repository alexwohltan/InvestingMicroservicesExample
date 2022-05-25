using System;

namespace Benchmark
{
	public class BenchmarkInMemoryRepository : IBenchmarkRepository
	{
        private List<MarketBenchmark> _MarketBenchmarks;
        private List<SectorBenchmark> _SectorBenchmarks;
        private List<IndustryBenchmark> _IndustryBenchmarks;

        public BenchmarkInMemoryRepository()
		{
            _MarketBenchmarks = new List<MarketBenchmark>();
            _SectorBenchmarks = new List<SectorBenchmark>();
            _IndustryBenchmarks = new List<IndustryBenchmark>();
		}

        public IEnumerable<MarketBenchmark> MarketBenchmarks { get => _MarketBenchmarks; }
        public IEnumerable<SectorBenchmark> SectorBenchmarks { get => _SectorBenchmarks; }
        public IEnumerable<IndustryBenchmark> IndustryBenchmarks { get => _IndustryBenchmarks; }

        public bool CompanyInRepository(string ticker, string marketName, string sectorName, string industryName)
        {
            var market = _MarketBenchmarks.FirstOrDefault(e => e.MarketName == marketName);
            var sector = _SectorBenchmarks.FirstOrDefault(e => e.MarketName == marketName && e.SectorName == sectorName);
            var industry = _IndustryBenchmarks.FirstOrDefault(e => e.MarketName == marketName && e.SectorName == sectorName && e.IndustryName == industryName);

            if (market == null)
                throw new ArgumentException(String.Format("Market {0} not found.", marketName));

            if (sector == null)
                throw new ArgumentException(String.Format("Sector {0} > {1} not found.", marketName, sectorName));

            if (industry == null)
                throw new ArgumentException(String.Format("Market {0} > {1} > {2} not found.", marketName, sectorName, industryName));

            var companyM = market.Companies.ContainsKey(ticker);
            var companyS = sector.Companies.ContainsKey(ticker);
            var companyI = industry.Companies.ContainsKey(ticker);

            if (companyM && companyS && companyI)
                return true;

            if (companyM || companyS || companyI)
                throw new Exception("Company is in one of the specified benchmarks but not all of them.");

            return false;
        }
        public bool MarketInRepository(string marketName)
        {
            return _MarketBenchmarks.FirstOrDefault(e => e.MarketName == marketName) != null;
        }
        public bool SectorInRepository(string marketName, string sectorName)
        {
            var market = _MarketBenchmarks.FirstOrDefault(e => e.MarketName == marketName);
            var sector = _SectorBenchmarks.FirstOrDefault(e => e.MarketName == marketName && e.SectorName == sectorName);

            if (market == null)
                throw new ArgumentException(String.Format("Market {0} not found.", marketName));

            return sector != null;
         }
        public bool IndustryInRepository(string marketName, string sectorName, string industryName)
        {
            var market = _MarketBenchmarks.FirstOrDefault(e => e.MarketName == marketName);
            var sector = _SectorBenchmarks.FirstOrDefault(e => e.MarketName == marketName && e.SectorName == sectorName);
            var industry = _IndustryBenchmarks.FirstOrDefault(e => e.MarketName == marketName && e.SectorName == sectorName && e.IndustryName == industryName);

            if (market == null)
                throw new ArgumentException(String.Format("Market {0} not found.", marketName));

            if (sector == null)
                throw new ArgumentException(String.Format("Sector {0} > {1} not found.", marketName, sectorName));

            return industry != null;
        }

        public void AddCompany(CompanyFundamentals companyFundamentals, string marketName, string sectorName, string industryName)
        {
            var market = _MarketBenchmarks.FirstOrDefault(e => e.MarketName == marketName);
            var sector = _SectorBenchmarks.FirstOrDefault(e => e.MarketName == marketName && e.SectorName == sectorName);
            var industry = _IndustryBenchmarks.FirstOrDefault(e => e.MarketName == marketName && e.SectorName == sectorName && e.IndustryName == industryName);

            if(market == null)
                throw new ArgumentException("Market " + marketName + " does not exist in Benchmark Repository.");
            if (sector == null)
                throw new ArgumentException("Sector " + marketName + " > " + sectorName + " does not exist in Benchmark Repository.");
            if (industry == null)
                throw new ArgumentException("Industry " + marketName + " > " + sectorName + " > " + industryName + " does not exist in Benchmark Repository.");

            market.AddCompany(companyFundamentals);
            sector.AddCompany(companyFundamentals);
            industry.AddCompany(companyFundamentals);
        }
        public void DeleteCompany(string ticker, string marketName, string sectorName, string industryName)
        {
            var market = _MarketBenchmarks.FirstOrDefault(e => e.MarketName == marketName);
            var sector = _SectorBenchmarks.FirstOrDefault(e => e.MarketName == marketName && e.SectorName == sectorName);
            var industry = _IndustryBenchmarks.FirstOrDefault(e => e.MarketName == marketName && e.SectorName == sectorName && e.IndustryName == industryName);

            if (market == null)
                throw new ArgumentException("Market " + marketName + " does not exist in Benchmark Repository.");
            if (sector == null)
                throw new ArgumentException("Sector " + marketName + " > " + sectorName + " does not exist in Benchmark Repository.");
            if (industry == null)
                throw new ArgumentException("Industry " + marketName + " > " + sectorName + " > " + industryName + " does not exist in Benchmark Repository.");

            market.DeleteCompany(ticker);
            sector.DeleteCompany(ticker);
            industry.DeleteCompany(ticker);
        }
        public void UpdateCompany(CompanyFundamentals companyFundamentals, string marketName, string sectorName, string industryName)
        {
            var market = _MarketBenchmarks.FirstOrDefault(e => e.MarketName == marketName);
            var sector = _SectorBenchmarks.FirstOrDefault(e => e.MarketName == marketName && e.SectorName == sectorName);
            var industry = _IndustryBenchmarks.FirstOrDefault(e => e.MarketName == marketName && e.SectorName == sectorName && e.IndustryName == industryName);

            if (market == null)
                throw new ArgumentException("Market " + marketName + " does not exist in Benchmark Repository.");
            if (sector == null)
                throw new ArgumentException("Sector " + marketName + " > " + sectorName + " does not exist in Benchmark Repository.");
            if (industry == null)
                throw new ArgumentException("Industry " + marketName + " > " + sectorName + " > " + industryName + " does not exist in Benchmark Repository.");

            market.UpdateCompany(companyFundamentals);
            sector.UpdateCompany(companyFundamentals);
            industry.UpdateCompany(companyFundamentals);
        }

        public void AddMarket(string marketName)
        {
            var market = _MarketBenchmarks.FirstOrDefault(e => e.MarketName == marketName);

            if (market != null)
                throw new ArgumentException("Market " + marketName + " already exists in Benchmark Repository.");

            _MarketBenchmarks.Add(new MarketBenchmark(marketName));
        }
        public void DeleteMarket(string marketName)
        {
            _MarketBenchmarks.RemoveAll(e => e.MarketName == marketName);
            _SectorBenchmarks.RemoveAll(e => e.MarketName == marketName);
            _IndustryBenchmarks.RemoveAll(e => e.MarketName == marketName);
        }

        public void AddSector(string marketName, string sectorName)
        {
            var sector = _SectorBenchmarks.FirstOrDefault(e => e.MarketName == marketName && e.SectorName == sectorName);

            if (sector != null)
                throw new ArgumentException("Sector " + marketName + " > " + sectorName + " already exists in Benchmark Repository.");

            _SectorBenchmarks.Add(new SectorBenchmark(marketName, sectorName));

            var market = _MarketBenchmarks.FirstOrDefault(e => e.MarketName == marketName);

            if (market == null)
                _MarketBenchmarks.Add(new MarketBenchmark(marketName));
        }
        public void DeleteSector(string marketName, string sectorName)
        {
            var sector = _SectorBenchmarks.FirstOrDefault(e => e.MarketName == marketName && e.SectorName == sectorName);

            if(sector != null)
            {
                var market = _MarketBenchmarks.FirstOrDefault(e => e.MarketName == marketName);

                if(market != null)
                {
                    foreach (var companyTicker in sector.Companies.Keys)
                    {
                        if (market.Companies.ContainsKey(companyTicker))
                            market.DeleteCompany(companyTicker);
                    }
                }
            }

            _SectorBenchmarks.RemoveAll(e => e.MarketName == marketName && e.SectorName == sectorName);
            _IndustryBenchmarks.RemoveAll(e => e.MarketName == marketName && e.SectorName == sectorName);
        }

        public void AddIndustry(string marketName, string sectorName, string industryName)
        {
            var industry = _IndustryBenchmarks.FirstOrDefault(e => e.MarketName == marketName && e.SectorName == sectorName && e.IndustryName == industryName);

            if (industry != null)
                throw new ArgumentException("Industry " + marketName + " > " + sectorName + " > " + industryName + " already exists in Benchmark Repository.");

            _IndustryBenchmarks.Add(new IndustryBenchmark(marketName, sectorName, industryName));

            var market = _MarketBenchmarks.FirstOrDefault(e => e.MarketName == marketName);

            if (market == null)
                _MarketBenchmarks.Add(new MarketBenchmark(marketName));

            var sector = _SectorBenchmarks.FirstOrDefault(e => e.MarketName == marketName && e.SectorName == sectorName);

            if (sector == null)
                _SectorBenchmarks.Add(new SectorBenchmark(marketName, sectorName));
        }
        public void DeleteIndustry(string marketName, string sectorName, string industryName)
        {
            var industry = _IndustryBenchmarks.FirstOrDefault(e => e.MarketName == marketName && e.SectorName == sectorName && e.IndustryName == industryName);

            if (industry != null)
            {
                var market = _MarketBenchmarks.FirstOrDefault(e => e.MarketName == marketName);
                var sector = _SectorBenchmarks.FirstOrDefault(e => e.MarketName == marketName && e.SectorName == sectorName);

                if (market != null)
                {
                    foreach (var companyTicker in industry.Companies.Keys)
                    {
                        if (market.Companies.ContainsKey(companyTicker))
                            market.DeleteCompany(companyTicker);
                    }
                }
                if(sector != null)
                {
                    foreach (var companyTicker in industry.Companies.Keys)
                    {
                        if (sector.Companies.ContainsKey(companyTicker))
                            sector.DeleteCompany(companyTicker);
                    }
                }
            }

            _IndustryBenchmarks.RemoveAll(e => e.MarketName == marketName && e.SectorName == sectorName);
        }
    }
}

