using System;

namespace Benchmark
{
	public class BenchmarkDbContext : DbContext, IBenchmarkRepository
	{
        private DbSet<MarketBenchmark> _MarketBenchmarks;
        private DbSet<SectorBenchmark> _SectorBenchmarks;
        private DbSet<IndustryBenchmark> _IndustryBenchmarks;

        public BenchmarkDbContext()
		{
			throw new NotImplementedException();
		}

        public IEnumerable<MarketBenchmark> MarketBenchmarks { get => _MarketBenchmarks; }
        public IEnumerable<SectorBenchmark> SectorBenchmarks { get => _SectorBenchmarks; }
        public IEnumerable<IndustryBenchmark> IndustryBenchmarks { get => _IndustryBenchmarks; }

        public void AddCompany(CompanyFundamentals companyFundamentals, string marketName, string sectorName, string industryName)
        {
            throw new NotImplementedException();
        }

        public void DeleteCompany(string ticker, string marketName, string sectorName, string industryName)
        {
            throw new NotImplementedException();
        }

        public void UpdateCompany(CompanyFundamentals companyFundamentals, string marketName, string sectorName, string industryName)
        {
            throw new NotImplementedException();
        }

        public bool CompanyInRepository(string ticker, string marketName, string sectorName, string industryName)
        {
            throw new NotImplementedException();
        }

        public void AddMarket(string marketName)
        {
            throw new NotImplementedException();
        }

        public void DeleteMarket(string marketName)
        {
            throw new NotImplementedException();
        }

        public void AddSector(string marketName, string sectorName)
        {
            throw new NotImplementedException();
        }

        public void DeleteSector(string marketName, string sectorName)
        {
            throw new NotImplementedException();
        }

        public void AddIndustry(string marketName, string sectorName, string industryName)
        {
            throw new NotImplementedException();
        }

        public void DeleteIndustry(string marketName, string sectorName, string industryName)
        {
            throw new NotImplementedException();
        }

        public bool MarketInRepository(string marketName)
        {
            throw new NotImplementedException();
        }

        public bool SectorInRepository(string marketName, string sectorName)
        {
            throw new NotImplementedException();
        }

        public bool IndustryInRepository(string marketName, string sectorName, string industryName)
        {
            throw new NotImplementedException();
        }
    }
}

