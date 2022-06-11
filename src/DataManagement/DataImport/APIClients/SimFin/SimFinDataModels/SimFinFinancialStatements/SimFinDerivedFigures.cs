using System;

namespace DataImport.APIClients.SimFin;

public class SimFinDerivedFigures : SimFinFinancialStatement
{
    public SimFinDerivedFigures(IList<string> columns, IList<object> dataPoints) : base(columns, dataPoints)
    {
    }

    public decimal EBITDA { get; set; }
    public decimal TotalDebt { get; set; }
    public decimal FreeCashFlow { get; set; }
    public decimal GrossProfitMargin { get; set; }
    public decimal OperatingMargin { get; set; }
    public decimal NetProfitMargin { get; set; }
    public decimal ReturnonEquity { get; set; }
    public decimal ReturnonAssets { get; set; }
    public decimal FreeCashFlowtoNetIncome { get; set; }
    public decimal CurrentRatio { get; set; }
    public decimal LiabilitiestoEquityRatio { get; set; }
    public decimal DebtRatio { get; set; }
    public decimal EarningsPerShareBasic { get; set; }
    public decimal EarningsPerShareDiluted { get; set; }
    public decimal SalesPerShare { get; set; }
    public decimal EquityPerShare { get; set; }
    public decimal FreeCashFlowPerShare { get; set; }
    public decimal DividendsPerShare { get; set; }
    public decimal PiotroskiFScore { get; set; }
    public decimal ReturnOnInvestedCapital { get; set; }
    public decimal CashReturnOnInvestedCapital { get; set; }
    public decimal DividendPayoutRatio { get; set; }
    public decimal NetDebtEBITDA { get; set; }
    public decimal NetDebtEBIT { get; set; }
}

