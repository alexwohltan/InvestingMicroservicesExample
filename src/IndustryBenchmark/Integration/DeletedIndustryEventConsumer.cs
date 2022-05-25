using System;

namespace Benchmark
{
	public class DeletedIndustryEventConsumer : IIntegrationEventHandler<DeletedIndustryEvent>
	{
        private readonly IBenchmarkRepository _Repository;

        public DeletedIndustryEventConsumer(IBenchmarkRepository repository)
		{
            _Repository = repository;
		}

        public async Task Handle(DeletedIndustryEvent @event)
        {
            string? marketName = @event.MarketName;
            string? sectorName = @event.SectorName;
            DataStructures.Industry industry = @event.OldIndustry;

            if (industry == null ||
                marketName == null ||
                sectorName == null)
                throw new ArgumentException("Industry and/or sector/market name cannot be null");

            if (_Repository.IndustryInRepository(marketName, sectorName, industry.Name))
            {
                _Repository.DeleteIndustry(marketName, sectorName, industry.Name);
            }

            return;
        }
    }
}

