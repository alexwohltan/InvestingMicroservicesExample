namespace WorkdayAnalysis;

public class DeletedCompanyEventConsumer : IIntegrationEventHandler<DeletedCompanyEvent>
{
    private readonly WorkdayAnalysisDbContext _Repository;

    public DeletedCompanyEventConsumer(WorkdayAnalysisDbContext repository)
    {
        _Repository = repository;
    }

    public async Task Handle(DeletedCompanyEvent @event)
    {
        Debug.WriteLine(String.Format("Received new Delete-Event. (Ticker = {0}, Name = {1})", @event.OldCompany.Ticker, @event.OldCompany.Name));

        Company? oldCompany = @event.OldCompany;
        string? marketName = @event.MarketName;
        string? sectorName = @event.SectorName;
        string? industryName = @event.IndustryName;

        if (marketName == null ||
            sectorName == null ||
            industryName == null)
            throw new ArgumentException("industry/sector/market name cannot be null");

        string? ticker = oldCompany.Ticker;

        if (ticker == null)
            throw new ArgumentException("Ticker cannot be null");

        // TODO: ADD business logic here.

        return;
    }
}



