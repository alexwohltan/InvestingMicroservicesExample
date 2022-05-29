using System;

namespace Benchmark
{
	public class NewIndustryEventConsumer : IIntegrationEventHandler<NewIndustryEvent>
	{
        private readonly BenchmarkDbContext _Repository;

        public NewIndustryEventConsumer(BenchmarkDbContext repository)
		{
            _Repository = repository;
		}

        public async Task Handle(NewIndustryEvent @event)
        {
            Debug.WriteLine(String.Format("Received new Industry. (Name = {0})", @event.NewIndustry.Name));

            string? marketName = @event.MarketName;
            string? sectorName = @event.SectorName;
            Industry industry = @event.NewIndustry;

            if (industry == null ||
                marketName == null ||
                sectorName == null)
                throw new ArgumentException("Industry and/or sector/market name cannot be null");

            if (!_Repository.IndustryInRepository(marketName, sectorName, industry.Name))
            {
                _Repository.AddIndustry(marketName, sectorName, industry.Name);
            }

            return;
        }
    }
}

