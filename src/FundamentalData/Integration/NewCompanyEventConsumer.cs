namespace FundamentalData.Integration
{
	public class NewCompanyEventConsumer : IIntegrationEventHandler<NewCompanyEvent>
	{
        private readonly FundamentalDataContext _repository;
        
        public NewCompanyEventConsumer(FundamentalDataContext repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task Handle(NewCompanyEvent @event)
        {
            Debug.WriteLine(String.Format("Received new Company. (Name = {0})", @event.NewCompany.Name));

            if (_repository.Companies.FirstOrDefault(e => e.Name == @event.NewCompany.Name) == null)
            {
                var market = _repository.Markets.First(e => e.Name == @event.MarketName);
                var sector = _repository.Sectors.First(e => e.Name == @event.SectorName && e.MarketID == market.ID);
                var industry = _repository.Industries.Include(i => i.Companies).First(e => e.Name == @event.IndustryName && e.SectorID == sector.ID);
                industry.Companies.Add(@event.NewCompany);
                await _repository.SaveChangesAsync();
            }

            return;
        }
    }
}

