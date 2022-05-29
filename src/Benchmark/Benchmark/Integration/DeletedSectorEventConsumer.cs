using System;

namespace Benchmark
{
	public class DeletedSectorEventConsumer : IIntegrationEventHandler<DeletedSectorEvent>
	{
        private readonly BenchmarkDbContext _Repository;

        public DeletedSectorEventConsumer(BenchmarkDbContext repository)
		{
            _Repository = repository;
		}

        public async Task Handle(DeletedSectorEvent @event)
        {
            string? marketName = @event.MarketName;
            Sector sector = @event.OldSector;

            if (marketName == null ||
                sector == null)
                throw new ArgumentException("Sector or market name cannot be null");

            if (_Repository.SectorInRepository(marketName, sector.Name))
            {
                _Repository.DeleteSector(marketName, sector.Name);
            }

            return;
        }
    }
}

