namespace WorkdayAnalysis;

public class UpdatedCompanyEventConsumer : IIntegrationEventHandler<UpdatedCompanyEvent>
{
    private readonly WorkdayAnalysisDbContext _Repository;

    public UpdatedCompanyEventConsumer(WorkdayAnalysisDbContext repository)
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

        var account = new Account()
        {
            FundamentalDataId = newCompany.ID,
            Ticker = newCompany.Ticker,
            Name = newCompany.Name
        };

        account.Filings = new List<AccountFiling>();
        foreach (var filing in newCompany.Filings)
        {
            account.Filings.Add(new AccountFiling()
            {
                Date = filing.FilingDate,

                Revenue = filing.IncomeStatement.Revenue,
                CostOfRevenue = filing.IncomeStatement.CostofRevenue,
                GrossProfit = filing.IncomeStatement.GrossProfit,
                SellingGeneralAdministrative = filing.IncomeStatement.SellingGeneralAdministrative,
                SellingMarketing = filing.IncomeStatement.SellingMarketing,
                GeneralAdministrative = filing.IncomeStatement.GeneralAdministrative,
                OperatingExpenses = filing.IncomeStatement.OperatingExpenses,
                OperatingIncome = filing.IncomeStatement.OperatingIncomeLoss,
                PretaxIncomeAdj = filing.IncomeStatement.PretaxIncomeLossAdj,
                PretaxIncome = filing.IncomeStatement.PretaxIncomeLoss,
                NetIncome = filing.IncomeStatement.NetIncome,

                OperatingCashflow = filing.CashflowStatement.NetCashfromOperatingActivities,
                FinancingCashflow = filing.CashflowStatement.NetCashfromFinancingActivities,
                InvestingCashflow = filing.CashflowStatement.NetCashfromInvestingActivities,
                FreeCashflow = filing.CashflowStatement.FreeCashFlow
            });
        }

        account.Prices = new List<AccountSharePrice>();
        foreach (var price in newCompany.Prices)
        {
            account.Prices.Add(new AccountSharePrice()
            {
                Date = price.Date,
                CloseAdj = price.AdjClose
            });
        }

        _Repository.UpdateAccount(account);

        return;
    }
}



