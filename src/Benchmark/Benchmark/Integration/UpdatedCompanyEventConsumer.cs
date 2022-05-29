using System;

namespace Benchmark
{
	public class UpdatedCompanyEventConsumer : IIntegrationEventHandler<UpdatedCompanyEvent>
	{
        private readonly BenchmarkDbContext _Repository;

        public UpdatedCompanyEventConsumer(BenchmarkDbContext repository)
		{
            _Repository = repository;
		}

        public async Task Handle(UpdatedCompanyEvent @event)
        {
            Debug.WriteLine(String.Format("Received updated Company. (Name = {0})", @event.NewCompany.Name));

            Company newCompany = @event.NewCompany;
            Company oldCompany = @event.OldCompany;
            string? marketName = @event.MarketName;
            string? sectorName = @event.SectorName;
            string? industryName = @event.IndustryName;

            if (newCompany == null ||
                oldCompany == null ||
                marketName == null ||
                sectorName == null ||
                industryName == null)
                throw new ArgumentException("Either company and/or industry/sector/market name cannot be null");

            string? ticker = newCompany.Ticker;
            string? oldTicker = oldCompany.Ticker;

            if (ticker == null)
                throw new ArgumentException("Ticker cannot be null");

            if (newCompany.Filings == null || newCompany.Filings.Count == 0)
                return;

            if (oldTicker != null && oldTicker != ticker && _Repository.CompanyInRepository(oldTicker, marketName, sectorName, industryName))
                _Repository.DeleteCompany(oldTicker, marketName, sectorName, industryName);

            var fundamentals = newCompany.ToCompanyFundamentals();

            if (_Repository.CompanyInRepository(ticker, marketName, sectorName, industryName))
            {
                _Repository.UpdateCompany(fundamentals, marketName, sectorName, industryName);
            }
            else
            {
                _Repository.AddCompany(fundamentals, marketName, sectorName, industryName);
            }

            return;
        }
    }
}

