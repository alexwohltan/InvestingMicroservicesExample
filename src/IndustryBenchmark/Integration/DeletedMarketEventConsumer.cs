﻿using System;

namespace Benchmark
{
	public class DeletedMarketEventConsumer : IIntegrationEventHandler<DeletedMarketEvent>
	{
        private readonly IBenchmarkRepository _Repository;

        public DeletedMarketEventConsumer(IBenchmarkRepository repository)
		{
            _Repository = repository;
		}

        public async Task Handle(DeletedMarketEvent @event)
        {
            string? marketName = @event.OldMarket.Name;

            if (marketName == null)
                throw new ArgumentException("market name cannot be null");

            if (_Repository.MarketInRepository(marketName))
            {
                _Repository.DeleteMarket(marketName);
            }

            return;
        }
    }
}

