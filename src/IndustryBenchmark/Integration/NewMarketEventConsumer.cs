using System;

namespace Benchmark
{
	public class NewMarketEventConsumer : IIntegrationEventHandler<NewMarketEvent>
	{
        private readonly IBenchmarkRepository _Repository;

        public NewMarketEventConsumer(IBenchmarkRepository repository)
		{
            _Repository = repository;
		}

        public async Task Handle(NewMarketEvent @event)
        {
            Debug.WriteLine(String.Format("Received new Market. (Name = {0})", @event.NewMarket.Name));

            string? marketName = @event.NewMarket.Name;

            if (marketName == null)
                throw new ArgumentException("market name cannot be null");

            if (!_Repository.MarketInRepository(marketName))
            {
                _Repository.AddMarket(marketName);
            }

            return;
        }
    }
}

