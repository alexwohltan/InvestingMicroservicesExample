using System;

namespace Benchmark
{
	public class DeletedCompanyEventConsumer : IIntegrationEventHandler<DeletedCompanyEvent>
	{
        private readonly IBenchmarkRepository _Repository;

        public DeletedCompanyEventConsumer(IBenchmarkRepository repository)
		{
            _Repository = repository;
		}

        public async Task Handle(DeletedCompanyEvent @event)
        {
            DataStructures.Company oldCompany = @event.OldCompany;
            string? marketName = @event.MarketName;
            string? sectorName = @event.SectorName;
            string? industryName = @event.IndustryName;

            if (oldCompany == null ||
                marketName == null ||
                sectorName == null ||
                industryName == null)
                throw new ArgumentException("Either company and/or industry/sector/market name cannot be null");

            string? ticker = oldCompany.Ticker;

            if (ticker == null)
                throw new ArgumentException("Ticker cannot be null");

            if (_Repository.CompanyInRepository(ticker, marketName, sectorName, industryName))
            {
                _Repository.DeleteCompany(ticker, marketName, sectorName, industryName);
            }

            return;
        }
    }
}

