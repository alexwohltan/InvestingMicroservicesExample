namespace FundamentalData.Integration
{
	public class NewMarketEventConsumer : IIntegrationEventHandler<NewMarketEvent>
	{
        private readonly FundamentalDataContext _repository;

		public NewMarketEventConsumer(FundamentalDataContext repository)
		{
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task Handle(NewMarketEvent @event)
        {
            Debug.WriteLine(String.Format("Received new Market. (Name = {0})", @event.NewMarket.Name));

            if (_repository.Markets.FirstOrDefault(e => e.Name == @event.NewMarket.Name) == null)
            {
                _repository.Markets.Add(@event.NewMarket);
                await _repository.SaveChangesAsync();
            }

            return;
        }
    }
}

