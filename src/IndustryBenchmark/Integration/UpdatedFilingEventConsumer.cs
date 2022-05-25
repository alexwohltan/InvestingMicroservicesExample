﻿using System;

namespace Benchmark
{
	public class UpdatedFilingEventConsumer : IIntegrationEventHandler<UpdatedFilingEvent>
	{
        private readonly IBenchmarkRepository _Repository;

        public UpdatedFilingEventConsumer(IBenchmarkRepository repository)
		{
            _Repository = repository;
		}

        public async Task Handle(UpdatedFilingEvent @event)
        {
            Debug.WriteLine(String.Format("Received new Filing. (Ticker = {0})", @event.CompanyTicker));

            DataStructures.Filing newFiling = @event.NewFiling;
            string? marketName = @event.MarketName;
            string? sectorName = @event.SectorName;
            string? industryName = @event.IndustryName;
            string? ticker = @event.CompanyTicker;

            if (newFiling == null ||
                marketName == null ||
                sectorName == null ||
                industryName == null ||
                ticker == null)
                throw new ArgumentException("Either filing and/or ticker/industry/sector/market name cannot be null");

            var fundamentals = newFiling.ToCompanyFundamentals(ticker, "");

            if (_Repository.CompanyInRepository(ticker, marketName, sectorName, industryName))
            {
                var marketBenchmark = _Repository.MarketBenchmarks.First(e => e.MarketName == marketName);
                if (marketBenchmark.Companies[ticker] <= fundamentals.LatestFilingDate)
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

