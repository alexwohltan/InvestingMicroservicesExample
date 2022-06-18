namespace APITemplate;

public class UpdatedCompanyEventConsumer : IIntegrationEventHandler<UpdatedCompanyEvent>
{
    private readonly MyDbContext _Repository;

    public UpdatedCompanyEventConsumer(MyDbContext repository)
    {
        _Repository = repository;
    }

    public async Task Handle(UpdatedCompanyEvent @event)
    {
        Debug.WriteLine(String.Format("Received new Company-Update-Event. (Ticker = {0}, Name = {1})", @event.NewCompany.Ticker, @event.NewCompany.Name));

        Company? oldCompany = @event.OldCompany;
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


