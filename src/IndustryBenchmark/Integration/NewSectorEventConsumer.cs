using System;

namespace Benchmark
{
	public class NewSectorEventConsumer : IIntegrationEventHandler<NewSectorEvent>
	{
        private readonly IBenchmarkRepository _Repository;

        public NewSectorEventConsumer(IBenchmarkRepository repository)
		{
            _Repository = repository;
		}

        public async Task Handle(NewSectorEvent @event)
        {
            Debug.WriteLine(String.Format("Received new Sector. (Name = {0})", @event.NewSector.Name));

            string? marketName = @event.MarketName;
            string? sectorName = @event.NewSector.Name;

            if (marketName == null ||
                sectorName == null)
                throw new ArgumentException("Sector and/or market name cannot be null");

            if (!_Repository.SectorInRepository(marketName, sectorName))
            {
                _Repository.AddSector(marketName, sectorName);
            }

            return;
        }
    }
}

