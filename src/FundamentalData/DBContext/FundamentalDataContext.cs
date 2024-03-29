﻿using System;
using Microsoft.EntityFrameworkCore;
using DataStructures;


namespace FundamentalData
{
    public class FundamentalDataContext : DbContext
    {
        private readonly IEventBus _eventBus;

        public FundamentalDataContext(IEventBus eventBus = null) : base()
        {
            _eventBus = eventBus;
        }
        public FundamentalDataContext(DbContextOptions<FundamentalDataContext> options, IEventBus eventBus = null)
            : base(options)
        {
            _eventBus = eventBus;
        }

        public DbSet<Market> Markets { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Filing> Filings { get; set; }
        public DbSet<StockPrice> Prices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        #region Market
        public async Task<Market> AddMarket(Market newMarket)
        {
            if (Markets.FirstOrDefault(e => e.Name == newMarket.Name) != null)
                throw new ArgumentException(String.Format(" Market {0} already exists.", newMarket.Name));

            Markets.Add(newMarket);

            await SaveChangesAsync();

            Debug.WriteLine("Added new Market with name " + newMarket.Name);

            var newMarketInDatabase = Markets.FirstOrDefault(e => e.Name == newMarket.Name);

            _eventBus.Publish(new NewMarketEvent { NewMarket = newMarketInDatabase.WithoutCompanies() });

            return newMarketInDatabase;
        }
        public async Task<IEnumerable<Market>> AddMarkets(IEnumerable<Market> markets)
        {
            foreach (var market in markets)
            {
                await AddMarket(market);
            }

            return Markets;
        }

        public IEnumerable<Market> GetMarkets()
        {
            return Markets.Include(e => e.Sectors).ThenInclude(e => e.Industries).ThenInclude(e => e.Companies).ThenInclude(e => e.Filings);
        }
        public async Task<Market> GetMarket(string name)
        {
            return await Markets.FirstOrDefaultAsync(e => e.Name == name);
        }
        public async Task<Market> GetMarket(int marketId)
        {
            return await Markets.FindAsync(marketId);
        }

        public async Task<Market> UpdateMarket(int marketId, Market newMarket)
        {
            var market = await Markets.FindAsync(marketId);

            if (market == null)
                throw new ArgumentException(String.Format("Market with ID {0} not found.", marketId));

            market.Name = newMarket.Name;

            foreach (var newSector in newMarket.Sectors)
            {
                var oldSector = GetSector(marketId, newSector.Name); // search for sector with new name in old sector list
                if (oldSector == null)
                    await AddSector(newSector, marketId); // name is not in old sector list -> Add
                else
                    await UpdateSector(oldSector.Id, newSector); // name is in old sector list -> Update
            }
            foreach (var oldSector in market.Sectors)
            {
                var newSector = newMarket.Sectors.FirstOrDefault(e => e.Name == oldSector.Name); // search for company with old ticker in new company list
                if (newSector == null)
                    await DeleteSector(oldSector.ID); // name is not in new sector list -> Delete
            }

            await SaveChangesAsync();

            return await Markets.FindAsync(marketId);
        }

        public async Task DeleteMarket(int marketId)
        {
            var market = await Markets.FindAsync(marketId);

            if (market == null)
                throw new ArgumentException(String.Format("Market with ID {0} not found.", marketId));

            Remove(market);

            await SaveChangesAsync();

            _eventBus.Publish(new DeletedMarketEvent { OldMarket = market.WithoutCompanies() });
        }
        #endregion

        #region Sector
        public async Task<Sector> AddSector(Sector newSector, int marketId)
        {
            var market = await Markets.FindAsync(marketId);

            if (market == null)
                throw new ArgumentException(String.Format("Market with ID {0} does not exist", marketId));

            if (market.Sectors.FirstOrDefault(e => e.Name == newSector.Name) != null)
                throw new ArgumentException(String.Format("Sector {0} in Market {1} already exists.", newSector.Name, market.Name));

            market.Sectors.Add(newSector);

            await SaveChangesAsync();

            Debug.WriteLine("Added new sector with name " + newSector.Name);

            var newSectorInDatabase = (await Markets.FindAsync(marketId)).Sectors.FirstOrDefault(e => e.Name == newSector.Name);

            _eventBus.Publish(new NewSectorEvent { MarketName = market.Name, NewSector = newSectorInDatabase.WithoutCompanies() });

            return newSectorInDatabase;
        }
        public async Task<Sector> AddSector(Sector newSector, string marketName)
        {
            var market = await Markets.FirstOrDefaultAsync(e => e.Name == marketName);

            if (market == null)
                throw new ArgumentException(String.Format("Market with name {0} does not exist", marketName));

            return await AddSector(newSector, market.ID);
        }
        public async Task<IEnumerable<Sector>> AddSectors(IEnumerable<Sector> sectors, int marketId)
        {
            foreach (var sector in sectors)
            {
                await AddSector(sector, marketId);
            }

            return (await Markets.FindAsync(marketId)).Sectors;
        }

        public async Task<IEnumerable<Sector>> GetSectors(int marketId)
        {
            return (await Markets.FindAsync(marketId)).Sectors;
        }
        public async Task<Sector> GetSector(int marketId, string name)
        {
            var market = await Markets.FindAsync(marketId);

            if (market == null)
                throw new ArgumentException(String.Format("Market with ID {0} does not exist", marketId));

            return market.Sectors.FirstOrDefault(e => e.Name == name);
        }
        public async Task<Sector> GetSector(string marketName, string sectorName)
        {
            var market = await Markets.FirstOrDefaultAsync(e => e.Name == marketName);

            if (market == null)
                throw new ArgumentException(String.Format("Market with name {0} does not exist", marketName));

            return market.Sectors.FirstOrDefault(e => e.Name == sectorName);
        }
        public async Task<Sector> GetSector(int sectorId)
        {
            return await Sectors.FindAsync(sectorId);
        }

        public async Task<Sector> UpdateSector(int sectorId, Sector newSector)
        {
            var sector = await Sectors.FindAsync(sectorId);

            if (sector == null)
                throw new ArgumentException(String.Format("Sector with ID {0} not found.", sectorId));

            sector.Name = newSector.Name;

            foreach (var newIndustry in newSector.Industries)
            {
                var oldIndustry = GetIndustry(sectorId, newIndustry.Name); // search for industry with new name in old industry list
                if (oldIndustry == null)
                    await AddIndustry(newIndustry, sectorId); // name is not in old industry list -> Add
                else
                    await UpdateIndustry(oldIndustry.Id, newIndustry); // name is in old industry list -> Update
            }
            foreach (var oldIndustry in sector.Industries)
            {
                var newIndustry = newSector.Industries.FirstOrDefault(e => e.Name == oldIndustry.Name); // search for industry with old ticker in new industry list
                if (newIndustry == null)
                    await DeleteIndustry(oldIndustry.ID); // ticker is not in new industry list -> Delete
            }

            await SaveChangesAsync();

            return await Sectors.FindAsync(sectorId);
        }

        public async Task DeleteSector(int sectorId)
        {
            var sector = await Sectors.FindAsync(sectorId);
            var marketName = sector.Market.Name;

            if (sector == null)
                throw new ArgumentException(String.Format("Sector with ID {0} not found.", sectorId));

            Remove(sector);

            await SaveChangesAsync();

            _eventBus.Publish(new DeletedSectorEvent { MarketName = marketName, OldSector = sector });
        }
        #endregion

        #region Industry
        public async Task<Industry> AddIndustry(Industry newIndustry, int sectorId)
        {
            var sector = await Sectors.FindAsync(sectorId);

            if (sector == null)
                throw new ArgumentException(String.Format("Sector with ID {0} does not exist", sectorId));

            if (sector.Industries.FirstOrDefault(e => e.Name == newIndustry.Name) != null)
                throw new ArgumentException(String.Format("Industry {0} in Sector {1} already exists.", newIndustry.Name, sector.Name));

            sector.Industries.Add(newIndustry);

            await SaveChangesAsync();

            Debug.WriteLine("Added new industry with name " + newIndustry.Name);

            var newIndustryInDatabase = (await Sectors.FindAsync(sectorId)).Industries.FirstOrDefault(e => e.Name == newIndustry.Name);

            _eventBus.Publish(new NewIndustryEvent { MarketName = sector.Market.Name, SectorName = sector.Name, NewIndustry = newIndustryInDatabase });

            return newIndustryInDatabase;
        }
        public async Task<Industry> AddIndustry(Industry newIndustry, string sectorName, string marketName)
        {
            var market = await Markets.FirstOrDefaultAsync(e => e.Name == marketName);

            if (market == null)
                throw new ArgumentException(String.Format("Market with name {0} does not exist", marketName));

            var sector = market.Sectors.FirstOrDefault(e => e.Name == sectorName);

            if (sector == null)
                throw new ArgumentException(String.Format("Sector with name {0} does not exist", sectorName));

            return await AddIndustry(newIndustry, sector.ID);
        }
        public async Task<IEnumerable<Industry>> AddIndustries(IEnumerable<Industry> industries, int sectorId)
        {
            foreach (var industry in industries)
            {
                await AddIndustry(industry, sectorId);
            }

            return (await Sectors.FindAsync(sectorId)).Industries;
        }

        public async Task<IEnumerable<Industry>> GetIndustries(int sectorId)
        {
            return (await Sectors.FindAsync(sectorId)).Industries;
        }
        public async Task<Industry> GetIndustry(int sectorId, string name)
        {
            var sector = await Sectors.FindAsync(sectorId);

            if (sector == null)
                throw new ArgumentException(String.Format("Sector with ID {0} does not exist", sectorId));

            return sector.Industries.FirstOrDefault(e => e.Name == name);
        }
        public async Task<Industry> GetIndustry(int industryId)
        {
            return await Industries.FindAsync(industryId);
        }
        public async Task<Industry> GetIndustry(string marketName, string sectorName, string industryName)
        {
            var market = await Markets.FirstOrDefaultAsync(e => e.Name == marketName);

            if (market == null)
                throw new ArgumentException(String.Format("Market with name {0} does not exist", marketName));

            var sector = await Sectors.FirstOrDefaultAsync(e => e.Name == sectorName && e.MarketID == market.ID);

            if (sector == null)
                throw new ArgumentException(String.Format("Sector with name {0} does not exist", sectorName));

            return sector.Industries.FirstOrDefault(e => e.Name == industryName);
        }

        public async Task<Industry> UpdateIndustry(int industryId, Industry newIndustry)
        {
            var industry = await Industries.FindAsync(industryId);

            if (industry == null)
                throw new ArgumentException(String.Format("Industry with ID {0} not found.", industryId));

            industry.Name = newIndustry.Name;

            foreach (var newCompany in newIndustry.Companies)
            {
                var oldCompany = industry.Companies.FirstOrDefault(e => e.Ticker == newCompany.Ticker); // search for company with new ticker in old company list
                if (oldCompany == null) 
                    await AddCompany(newCompany, industryId); // ticker is not in old company list -> Add
                else
                    await UpdateCompany(oldCompany.ID, newCompany); // ticker is in old company list -> Update
            }
            foreach (var oldCompany in industry.Companies)
            {
                var newCompany = newIndustry.Companies.FirstOrDefault(e => e.Ticker == oldCompany.Ticker); // search for company with old ticker in new company list
                if (newCompany == null)
                    await DeleteCompany(oldCompany.ID); // ticker is not in new company list -> Delete
            }

            await SaveChangesAsync();

            return await Industries.FindAsync(industryId);
        }


        public async Task DeleteIndustry(int industryId)
        {
            var industry = await Industries.FindAsync(industryId);
            var sector = await Sectors.FindAsync(industry.SectorID);
            var market = await Markets.FindAsync(sector.MarketID);

            if (industry == null)
                throw new ArgumentException(String.Format("Industry with ID {0} not found.", industryId));

            Remove(industry);

            await SaveChangesAsync();

            _eventBus.Publish(new DeletedIndustryEvent { MarketName = market.Name, SectorName = sector.Name, OldIndustry = industry });
        }
        #endregion

        #region Company
        public async Task<Company> AddCompany(Company newCompany, int industryId)
        {
            var industry = await Industries.FindAsync(industryId);
            var sector = await Sectors.FindAsync(industry.SectorID);
            var market = await Markets.FindAsync(sector.MarketID);

            if (industry == null)
                throw new ArgumentException(String.Format("Industry with ID {0} does not exist", industryId));

            if (industry.Companies.FirstOrDefault(e => e.Name == newCompany.Name && e.Ticker == newCompany.Ticker) != null)
                throw new ArgumentException(String.Format("Company {0} in Industry {1} already exists.", newCompany.Name, industry.Name));

            industry.Companies.Add(newCompany);

            await SaveChangesAsync();

            Debug.WriteLine("Added new company with name " + newCompany.Name);

            var newCompanyInDatabase = (await Industries.FindAsync(industryId)).Companies.FirstOrDefault(e => e.Name == newCompany.Name && e.Ticker == newCompany.Ticker);

            _eventBus.Publish(new NewCompanyEvent { MarketName = market.Name, SectorName = sector.Name, IndustryName = industry.Name, NewCompany = newCompanyInDatabase });

            return newCompanyInDatabase;
        }
        public async Task<Company> AddCompany(Company newCompany, string industryName, string sectorName, string marketName)
        {
            var market = await Markets.FirstOrDefaultAsync(e => e.Name == marketName);

            if (market == null)
                throw new ArgumentException(String.Format("Market with name {0} does not exist", marketName));

            if (sectorName == null)
                sectorName = "";

            var sector = market.Sectors.FirstOrDefault(e => e.Name == sectorName);

            if (sector == null)
                throw new ArgumentException(String.Format("Sector with name {0} does not exist", sectorName));

            if (industryName == null)
                industryName = "";

            var industry = sector.Industries.FirstOrDefault(e => e.Name == industryName);

            if (industry == null)
                throw new ArgumentException(String.Format("Industry with name {0} does not exist", industryName));

            return await AddCompany(newCompany, industry.ID);
        }
        public async Task<IEnumerable<Company>> AddCompany(IEnumerable<Company> companies, int industryId)
        {
            foreach (var company in companies)
            {
                await AddCompany(company, industryId);
            }

            return (await Industries.FindAsync(industryId)).Companies;
        }

        public async Task<IEnumerable<Company>> GetCompanies(int industryId)
        {
            return (await Industries.FindAsync(industryId)).Companies;
        }
        public async Task<Company> GetCompany(string ticker)
        {
            return await Companies.FirstOrDefaultAsync(e => e.Ticker == ticker);
        }
        public async Task<Company> GetCompany(int companyId)
        {
            return await Companies.FindAsync(companyId);
        }

        public async Task<Company> UpdateCompany(int companyId, Company newCompany)
        {
            var company = await Companies.FindAsync(companyId);

            var industry = await Industries.FindAsync(company.IndustryID);
            var sector = await Sectors.FindAsync(industry.SectorID);
            var market = await Markets.FindAsync(sector.MarketID);

            if (company == null)
                throw new ArgumentException(String.Format("Company with ID {0} not found.", companyId));

            company.Name = newCompany.Name;
            company.Ticker = newCompany.Ticker;
            company.BusinessSummary = newCompany.BusinessSummary;
            company.NumberEmployees = newCompany.NumberEmployees;
            company.MonthFyEnd = newCompany.MonthFyEnd;

            foreach (var newFiling in newCompany.Filings)
            {
                var oldFiling = GetFiling(companyId, newFiling.FilingDate, newFiling.PeriodStartDate);
                if (oldFiling == null)
                    await AddFiling(newFiling, companyId);
                else
                    await UpdateFiling(oldFiling.Id, newFiling);
            }
            foreach (var oldFiling in company.Filings)
            {
                var newFiling = newCompany.Filings.FirstOrDefault(e => e.FilingDate == oldFiling.FilingDate && e.PeriodStartDate == oldFiling.PeriodStartDate);
                if (newFiling == null)
                    await DeleteFiling(oldFiling.ID);
            }

            await UpdateStockPrices(newCompany.Prices, companyId);

            await SaveChangesAsync();

            var newCompanyInDatabase = await Companies.FindAsync(companyId);

            _eventBus.Publish(new UpdatedCompanyEvent() { NewCompany = newCompanyInDatabase, MarketName = market.Name, SectorName = sector.Name, IndustryName = industry.Name });

            return newCompanyInDatabase;
        }

        public async Task DeleteCompany(int companyId)
        {
            var company = await Companies.FindAsync(companyId);
            var industry = await Industries.FindAsync(company.IndustryID);
            var sector = await Sectors.FindAsync(industry.SectorID);
            var market = await Markets.FindAsync(sector.MarketID);

            if (company == null)
                throw new ArgumentException(String.Format("Company with ID {0} not found.", companyId));

            Remove(company);

            await SaveChangesAsync();

            _eventBus.Publish(new DeletedCompanyEvent { MarketName = market.Name, SectorName = sector.Name, IndustryName = industry.Name, OldCompany = company });
        }
        #endregion

        #region Filing
        public async Task<Filing> AddFiling(Filing newFiling, int companyId)
        {
            var company = await Companies.FindAsync(companyId);
            var industry = await Industries.FindAsync(company.IndustryID);
            var sector = await Sectors.FindAsync(industry.SectorID);
            var market = await Markets.FindAsync(sector.MarketID);

            if (company == null)
                throw new ArgumentException(String.Format("Company with ID {0} does not exist", companyId));

            if(company.Filings.FirstOrDefault(e => e.FilingDate == newFiling.FilingDate && e.PeriodStartDate == newFiling.PeriodStartDate) != null)
                throw new ArgumentException(String.Format("Filing (Filing Date = {0} & Period Start = {1}) for Company with ID {2} already exists.", newFiling.FilingDate.ToShortDateString(), newFiling.PeriodStartDate.ToShortDateString(), companyId));

            company.Filings.Add(newFiling);

            await SaveChangesAsync();

            var newFilingInDatabase = (await Companies.FindAsync(companyId)).Filings.FirstOrDefault(e => e.FilingDate == newFiling.FilingDate);

            _eventBus.Publish(new NewFilingEvent { MarketName = market.Name, SectorName = sector.Name, IndustryName = industry.Name, CompanyTicker = company.Ticker, NewFiling = newFilingInDatabase });

            return newFilingInDatabase;
        }
        public async Task<IEnumerable<Filing>> AddFilings(IEnumerable<Filing> filings, int companyId)
        {
            foreach (var filing in filings)
            {
                await AddFiling(filing, companyId);
            }

            return (await Companies.FindAsync(companyId)).Filings;
        }

        public async Task<IEnumerable<Filing>> GetFilings(int companyId)
        {
            return (await Companies.FindAsync(companyId)).Filings;
        }
        public async Task<Filing> GetFiling(int companyId, DateTime filingDate, DateTime periodStartDate)
        {
            var company = await Companies.FindAsync(companyId);

            if (company == null)
                throw new ArgumentException(String.Format("Company with ID {0} does not exist", companyId));

            return company.Filings.FirstOrDefault(e => e.FilingDate == filingDate && e.PeriodStartDate == periodStartDate);
        }
        public async Task<Filing> GetFiling(int filingId)
        {
            return await Filings.FindAsync(filingId);
        }

        public async Task<Filing> UpdateFiling(int filingId, Filing newFiling)
        {
            var filing = await Filings.FindAsync(filingId);

            filing.IncomeStatement = newFiling.IncomeStatement;
            filing.BalanceSheet = newFiling.BalanceSheet;
            filing.CashflowStatement = newFiling.CashflowStatement;
            filing.Ticker = newFiling.Ticker;
            filing.Currency = newFiling.Currency;
            filing.FilingDate = newFiling.FilingDate;
            filing.PeriodStartDate = newFiling.PeriodStartDate;
            filing.PublishedDate = newFiling.PublishedDate;
            filing.RestatedDate = newFiling.RestatedDate;

            await SaveChangesAsync();

            return await Filings.FindAsync(filingId);
        }

        public async Task DeleteFiling(int filingId)
        {
            var filing = await Filings.FindAsync(filingId);

            var company = await Companies.FindAsync(filing.CompanyID);
            var industry = await Industries.FindAsync(company.IndustryID);
            var sector = await Sectors.FindAsync(industry.SectorID);
            var market = await Markets.FindAsync(sector.MarketID);

            if (filing == null)
                throw new ArgumentException(String.Format("Filing with ID {0} not found.", filingId));

            Remove(filing);

            await SaveChangesAsync();

            _eventBus.Publish(new DeletedFilingEvent { MarketName = market.Name, SectorName = sector.Name, IndustryName = industry.Name, CompanyTicker = company.Ticker, OldFiling = filing });
        }
        #endregion

        #region StockPrice
        public async Task<IEnumerable<StockPrice>> AddStockPrices(IEnumerable<StockPrice> prices, int companyId)
        {
            var company = await Companies.FindAsync(companyId);
            var industry = await Industries.FindAsync(company.IndustryID);
            var sector = await Sectors.FindAsync(industry.SectorID);
            var market = await Markets.FindAsync(sector.MarketID);

            if (company == null)
                throw new ArgumentException(String.Format("Company with ID {0} does not exist", companyId));

            if (company.Prices == null || company.Prices.Count() == 0)
            {
                company.Prices = new List<StockPrice>();
                company.Prices.AddRange(prices);
            }
            else
            {
                var lastPriceDate = company.Prices.OrderBy(e => e.Date).Last().Date;
                foreach (var price in prices.Where(e => e.Date > lastPriceDate))
                {
                    company.Prices.Add(price);
                }
            }

            company.Prices.Sort((x,y) => x.Date.CompareTo(y.Date));

            await SaveChangesAsync();

            var newPrices = (await Companies.FindAsync(companyId)).Prices;

            _eventBus.Publish(new UpdatedStockPricesEvent { MarketName = market.Name, SectorName = sector.Name, IndustryName = industry.Name, CompanyTicker = company.Ticker, NewPrices = newPrices.ToList() });

            return newPrices;
        }
        public async Task<IEnumerable<StockPrice>> AddStockPrices(IEnumerable<StockPrice> prices, string ticker)
        {
            var companyId = (await GetCompany(ticker)).ID;
            return await AddStockPrices(prices, companyId);
        }

        public async Task<IEnumerable<StockPrice>> GetPrices(int companyId)
        {
            return (await Companies.FindAsync(companyId)).Prices;
        }
        public async Task<IEnumerable<StockPrice>> GetPrices(string ticker)
        {
            return (await GetCompany(ticker)).Prices;
        }

        public async Task<IEnumerable<StockPrice>> UpdateStockPrices(IEnumerable<StockPrice> prices, int companyId)
        {
            return await AddStockPrices(prices, companyId);
        }
        public async Task<IEnumerable<StockPrice>> UpdateStockPrices(IEnumerable<StockPrice> prices, string ticker)
        {
            return await AddStockPrices(prices, ticker);
        }
        #endregion

        #region Events
        #endregion
    }
}
