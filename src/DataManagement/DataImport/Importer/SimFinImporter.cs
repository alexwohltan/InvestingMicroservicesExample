using System;
namespace DataImport
{
    public class SimFinImporter : IImporter
    {
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly ILogger _logger;
        private readonly CancellationToken _cancellationToken;

        private readonly SimFinClient _client;

        public SimFinImporter(IBackgroundTaskQueue taskQueue,
        ILogger<SimFinImporter> logger,
        IHostApplicationLifetime applicationLifetime)
        {
            _taskQueue = taskQueue;
            _logger = logger;
            _cancellationToken = applicationLifetime.ApplicationStopping;

            _client = new SimFinClient("simfin.com", 443, "api/v2/", "e3Vs37gGTuE15kRKL4QJketpT7lOkLZl");
        }

        public async Task<IEnumerable<string>> GetAvialableTickers()
        {
            return await _client.ListAllCompaniesInSimFin();
        }

        public void ImportBulkCompanyFundamentalData()
        {
            throw new NotImplementedException();
        }

        public void ImportCompany(string ticker)
        {
            try
            {
                _logger.LogInformation("Queued " + ticker + " for import.");


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
            throw new NotImplementedException();
        }

        public void UpdateStockPrice(string ticker)
        {
            throw new NotImplementedException();
        }

        #region ImportTasks

        private async ValueTask RunImportCompanyAsync(string ticker, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

