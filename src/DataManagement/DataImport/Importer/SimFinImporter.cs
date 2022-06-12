using System;
namespace DataImport
{
    public class SimFinImporter : IImporter
    {
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly ILogger _logger;
        private readonly CancellationToken _cancellationToken;

        private readonly SimFinClient _simFinWebClient;
        private readonly SimFinBulkClient _simFinBulkClient;
        private readonly FundamentalDataClient _fundamentalsClient;

        public SimFinImporter(IBackgroundTaskQueue taskQueue,
        ILogger<SimFinImporter> logger,
        IHostApplicationLifetime applicationLifetime)
        {
            _taskQueue = taskQueue;
            _logger = logger;
            _cancellationToken = applicationLifetime.ApplicationStopping;

            _simFinWebClient = new SimFinClient("simfin.com", 443, "api/v2/", "e3Vs37gGTuE15kRKL4QJketpT7lOkLZl");
            _simFinBulkClient = new SimFinBulkClient();
            _fundamentalsClient = new FundamentalDataClient("localhost", 60958, "api/");
        }

        public async Task<IEnumerable<string>> GetAvialableTickers()
        {
            return await _simFinWebClient.ListAllCompaniesInSimFin();
        }

        public void ImportBulkCompanyFundamentalData()
        {
            try
            {
                _logger.LogInformation("Queued bulk import.");

                _taskQueue.QueueBackgroundWorkItemAsync(ct => RunBulkImportAsync(ct));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public void ImportCompany(string ticker, string marketName, string sectorName, string industryName)
        {
            try
            {
                _logger.LogInformation("Queued " + ticker + " for import.");

                _taskQueue.QueueBackgroundWorkItemAsync(ct => RunImportCompanyAsync(ticker, marketName, sectorName, industryName, ct));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public void ImportStockPrice(string ticker)
        {
            throw new NotImplementedException();
        }

        public void UpdateCompany(string ticker)
        {
            try
            {
                _logger.LogInformation("Queued " + ticker + " for update.");

                _taskQueue.QueueBackgroundWorkItemAsync(ct => RunUpdateCompanyAsync(ticker, ct));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public void UpdateStockPrice(string ticker)
        {
            throw new NotImplementedException();
        }

        #region ImportTasks

        private async ValueTask RunBulkImportAsync(CancellationToken cancellationToken)
        {
            var markets = _simFinBulkClient.RetrieveMarkets();

            foreach (var market in markets)
            {
                var marketInDb = await _fundamentalsClient.GetMarket(market.Name);
                if(marketInDb == null)
                {
                    await _fundamentalsClient.AddMarket(market.WithoutSectors());
                    marketInDb = await _fundamentalsClient.GetMarket(market.Name);
                }
                var marketId = marketInDb.ID;

                foreach (var sector in market.Sectors)
                {
                    var sectorInDb = await _fundamentalsClient.GetSector(sector.Name, marketId);
                    if (sectorInDb == null)
                    {
                        await _fundamentalsClient.AddSector(sector.WithoutIndustries(), marketId);
                        sectorInDb = await _fundamentalsClient.GetSector(sector.Name, marketId);
                    }
                    var sectorId = sectorInDb.ID;

                    foreach (var industry in sector.Industries)
                    {
                        var industryInDb = await _fundamentalsClient.GetIndustry(industry.Name, sectorId);
                        if (industryInDb == null)
                        {
                            await _fundamentalsClient.AddIndustry(industry.WithoutCompanies(), sectorId);
                            industryInDb = await _fundamentalsClient.GetIndustry(industry.Name, sectorId);
                        }
                        var industryId = industryInDb.ID;

                        foreach (var company in industry.Companies)
                        {
                            if (cancellationToken.IsCancellationRequested)
                                return;

                            var companyInDb = await _fundamentalsClient.GetCompany(company.Ticker);
                            if (companyInDb == null)
                            {
                                await _fundamentalsClient.AddCompany(company, industryId);
                                companyInDb = await _fundamentalsClient.GetCompany(company.Ticker);
                            }
                            var companyId = companyInDb.ID;

                            _logger.LogInformation(String.Format("Added company {0}. ID = {1}.", company.Name, companyId));
                        }

                        _logger.LogInformation(String.Format("Added industry {0}. ID = {1}.", industry.Name, industryId));
                    }

                    _logger.LogInformation(String.Format("Added sector {0}. ID = {1}.", sector.Name, sectorId));
                }

                _logger.LogInformation(String.Format("Added market {0}. ID = {1}.", market.Name, marketId));
            }
        }

        private async ValueTask RunImportCompanyAsync(string ticker, string marketName, string sectorName, string industryName, CancellationToken cancellationToken)
        {
            var company = await _simFinWebClient.GetCompany(ticker);

            if (company == null)
                return;

            var result = await _fundamentalsClient.AddCompany(company, marketName, sectorName, industryName);
            var companyInDatabase = await _fundamentalsClient.GetCompany(company.Ticker);
            var companyId = companyInDatabase.ID;

            //Debug.WriteLine(String.Format("Added company {0}. ID = {1}.", company.Name, companyId));
            _logger.LogInformation(String.Format("Added company {0}. ID = {1}.", company.Name, companyId));
        }

        private async ValueTask RunUpdateCompanyAsync(string ticker, CancellationToken cancellationToken)
        {
            var company = await _simFinWebClient.GetCompany(ticker);
            var companyInDb = await _fundamentalsClient.GetCompany(ticker);

            if (company == null)
                return;

            if(companyInDb == null)
            {
                _logger.LogError(String.Format("Company with ticker {0} does not exist in database.", company.Ticker));
                throw new Exception(String.Format("Company with ticker {0} does not exist in database. You must import it first before you can use update.", company.Ticker));
            }

            company.ID = companyInDb.ID;
            company.Ticker = ticker;

            if (company.Name == null || company.Name == "")
                company.Name = companyInDb.Name;
            if (company.MonthFyEnd == 0)
                company.MonthFyEnd = companyInDb.MonthFyEnd;
            if (company.NumberEmployees == 0)
                company.NumberEmployees = companyInDb.NumberEmployees;
            if (company.BusinessSummary == null || company.BusinessSummary == "")
                company.BusinessSummary = companyInDb.BusinessSummary;

            if (company.Filings == null || company.Filings.Count() == 0)
                company.Filings = companyInDb.Filings;
            else
                company.Filings.AddRange(companyInDb.Filings.Where(e => !company.Filings.Select(f => f.FilingDate).Contains(e.FilingDate)));

            if (company.Prices == null || company.Prices.Count() == 0)
                company.Prices = companyInDb.Prices;
            else
                company.Prices.AddRange(companyInDb.Prices.Where(e => !company.Prices.Select(f => f.Date).Contains(e.Date)));

            var result = await _fundamentalsClient.UpdateCompany(companyInDb.ID, company);
            companyInDb = await _fundamentalsClient.GetCompany(company.Ticker);
            var companyId = companyInDb.ID;

            //Debug.WriteLine(String.Format("Added company {0}. ID = {1}.", company.Name, companyId));
            _logger.LogInformation(String.Format("Updated company {0}. ID = {1}.", company.Name, companyId));
        }

        #endregion
    }
}

