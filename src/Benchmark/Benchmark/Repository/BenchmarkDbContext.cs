using System;

namespace Benchmark
{
	public class BenchmarkDbContext : DbContext
	{
        public DbSet<MarketBenchmark> _MarketBenchmarks { get; set; }
        public DbSet<SectorBenchmark> _SectorBenchmarks { get; set; }
        public DbSet<IndustryBenchmark> _IndustryBenchmarks { get; set; }
        public DbSet<Metric> _Metrics { get; set; }

        private readonly IEventBus _eventBus;

        public BenchmarkDbContext(IEventBus eventBus = null) : base()
		{
            _eventBus = eventBus;
		}
        public BenchmarkDbContext(DbContextOptions<BenchmarkDbContext> options, IEventBus eventBus = null)
            : base(options)
        {
            _eventBus = eventBus;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Metric>()
                .Property(e => e.TopCompanies)
                .HasConversion(
                    v => string.Join(";", v),
                    v => v.Split(";", StringSplitOptions.RemoveEmptyEntries).ToList());
            modelBuilder.Entity<Metric>()
                .Property(e => e.WorstCompanies)
                .HasConversion(
                    v => string.Join(";", v),
                    v => v.Split(";", StringSplitOptions.RemoveEmptyEntries).ToList());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public IEnumerable<MarketBenchmark> MarketBenchmarks { get => _MarketBenchmarks.Include(e => e.Metrics).ToList(); }
        public IEnumerable<SectorBenchmark> SectorBenchmarks { get => _SectorBenchmarks.Include(e => e.Metrics).Include(e => e.CompanyDates).ToList(); }
        public IEnumerable<IndustryBenchmark> IndustryBenchmarks { get => _IndustryBenchmarks.Include(e => e.Metrics).Include(e => e.CompanyDates).ToList(); }


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

            var companyM = market.CompanyDates.FirstOrDefault(e => e.Ticker == ticker) != null;
            var companyS = sector.CompanyDates.FirstOrDefault(e => e.Ticker == ticker) != null;
            var companyI = industry.CompanyDates.FirstOrDefault(e => e.Ticker == ticker) != null;

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

        public async void AddCompany(CompanyFundamentals companyFundamentals, string marketName, string sectorName, string industryName)
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

            market.AddCompany(companyFundamentals);
            sector.AddCompany(companyFundamentals);
            industry.AddCompany(companyFundamentals);

            SaveChanges();
            //await SaveChangesAsync();
        }
        public async void DeleteCompany(string ticker, string marketName, string sectorName, string industryName)
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

            SaveChanges();
            //await SaveChangesAsync();
        }
        public async void UpdateCompany(CompanyFundamentals companyFundamentals, string marketName, string sectorName, string industryName)
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

            SaveChanges();
            //await SaveChangesAsync();
        }

        public async void AddMarket(string marketName)
        {
            var market = _MarketBenchmarks.FirstOrDefault(e => e.MarketName == marketName);

            if (market != null)
                throw new ArgumentException("Market " + marketName + " already exists in Benchmark Repository.");

            var mktBenchmark = new MarketBenchmark() { MarketName = marketName };
            mktBenchmark.Initialize();

            _MarketBenchmarks.Add(mktBenchmark);

            SaveChanges();
            //await SaveChangesAsync();
        }
        public async void DeleteMarket(string marketName)
        {
            _MarketBenchmarks.RemoveRange(_MarketBenchmarks.Where(e => e.MarketName == marketName));
            _SectorBenchmarks.RemoveRange(_SectorBenchmarks.Where(e => e.MarketName == marketName));
            _IndustryBenchmarks.RemoveRange(_IndustryBenchmarks.Where(e => e.MarketName == marketName));

            await SaveChangesAsync();
        }

        public async void AddSector(string marketName, string sectorName)
        {
            var sector = _SectorBenchmarks.FirstOrDefault(e => e.MarketName == marketName && e.SectorName == sectorName);

            if (sector != null)
                throw new ArgumentException("Sector " + marketName + " > " + sectorName + " already exists in Benchmark Repository.");

            var sctBenchmark = new SectorBenchmark() { MarketName = marketName, SectorName = sectorName };
            sctBenchmark.Initialize();
            _SectorBenchmarks.Add(sctBenchmark);

            var market = _MarketBenchmarks.FirstOrDefault(e => e.MarketName == marketName);

            if (market == null)
            {
                var mktBenchmark = new MarketBenchmark() { MarketName = marketName };
                mktBenchmark.Initialize();
                _MarketBenchmarks.Add(mktBenchmark);
            }

            SaveChanges();
            //await SaveChangesAsync();
        }
        public async void DeleteSector(string marketName, string sectorName)
        {
            var sector = _SectorBenchmarks.FirstOrDefault(e => e.MarketName == marketName && e.SectorName == sectorName);

            if (sector != null)
            {
                var market = _MarketBenchmarks.FirstOrDefault(e => e.MarketName == marketName);

                if (market != null)
                {
                    foreach (var companyTicker in sector.CompanyDates.Select(e => e.Ticker))
                    {
                        if (market.CompanyDates.FirstOrDefault(e => e.Ticker == companyTicker) != null)
                            market.DeleteCompany(companyTicker);
                    }
                }
            }

            _SectorBenchmarks.RemoveRange(_SectorBenchmarks.Where(e => e.MarketName == marketName && e.SectorName == sectorName));
            _IndustryBenchmarks.RemoveRange(_IndustryBenchmarks.Where(e => e.MarketName == marketName && e.SectorName == sectorName));

            SaveChanges();
            //await SaveChangesAsync();
        }

        public async void AddIndustry(string marketName, string sectorName, string industryName)
        {
            var industry = _IndustryBenchmarks.FirstOrDefault(e => e.MarketName == marketName && e.SectorName == sectorName && e.IndustryName == industryName);

            if (industry != null)
                throw new ArgumentException("Industry " + marketName + " > " + sectorName + " > " + industryName + " already exists in Benchmark Repository.");

            var indBenchmark = new IndustryBenchmark() { MarketName = marketName, SectorName = sectorName, IndustryName = industryName };
            indBenchmark.Initialize();
            _IndustryBenchmarks.Add(indBenchmark);

            var market = _MarketBenchmarks.FirstOrDefault(e => e.MarketName == marketName);

            if (market == null)
            {
                var mktBenchmark = new MarketBenchmark() { MarketName = marketName };
                mktBenchmark.Initialize();
                _MarketBenchmarks.Add(mktBenchmark);
            }

            var sector = _SectorBenchmarks.FirstOrDefault(e => e.MarketName == marketName && e.SectorName == sectorName);

            if (sector == null)
            {
                var sctBenchmark = new SectorBenchmark() { MarketName = marketName, SectorName = sectorName };
                sctBenchmark.Initialize();
                _SectorBenchmarks.Add(sctBenchmark);
            }

            SaveChanges();
            //await SaveChangesAsync();
        }
        public async void DeleteIndustry(string marketName, string sectorName, string industryName)
        {
            var industry = _IndustryBenchmarks.FirstOrDefault(e => e.MarketName == marketName && e.SectorName == sectorName && e.IndustryName == industryName);

            if (industry != null)
            {
                var market = _MarketBenchmarks.FirstOrDefault(e => e.MarketName == marketName);
                var sector = _SectorBenchmarks.FirstOrDefault(e => e.MarketName == marketName && e.SectorName == sectorName);

                if (market != null)
                {
                    foreach (var companyTicker in industry.CompanyDates.Select(e => e.Ticker))
                    {
                        if (market.CompanyDates.FirstOrDefault(e => e.Ticker == companyTicker) != null)
                            market.DeleteCompany(companyTicker);
                    }
                }
                if (sector != null)
                {
                    foreach (var companyTicker in industry.CompanyDates.Select(e => e.Ticker))
                    {
                        if (sector.CompanyDates.FirstOrDefault(e => e.Ticker == companyTicker) != null)
                            sector.DeleteCompany(companyTicker);
                    }
                }
            }

            _IndustryBenchmarks.RemoveRange(_IndustryBenchmarks.Where(e => e.MarketName == marketName && e.SectorName == sectorName && e.IndustryName == industryName));

            SaveChanges();
            //await SaveChangesAsync();
        }
    }
}

