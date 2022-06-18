namespace APITemplate;

public class NewCompanyEventConsumer : IIntegrationEventHandler<NewCompanyEvent>
{
    private readonly MyDbContext _Repository;

    public NewCompanyEventConsumer(MyDbContext repository)
    {
        _Repository = repository;
    }

    public async Task Handle(NewCompanyEvent @event)
    {
        Debug.WriteLine(String.Format("Received new Company. (Ticker = {0}, Name = {1})", @event.NewCompany.Ticker, @event.NewCompany.Name));

        Company newCompany = @event.NewCompany;
        string? marketName = @event.MarketName;
        string? sectorName = @event.SectorName;
        string? industryName = @event.IndustryName;

        if (newCompany == null ||
            marketName == null ||
            sectorName == null ||
            industryName == null)
            throw new ArgumentException("Either company and/or industry/sector/market name cannot be null");

        string? ticker = newCompany.Ticker;

        if (ticker == null)
            throw new ArgumentException("Ticker cannot be null");

        // TODO: ADD business logic here.

        return;
    }
}


