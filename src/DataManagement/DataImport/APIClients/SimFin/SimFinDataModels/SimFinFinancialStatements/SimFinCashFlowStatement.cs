using System;

namespace DataImport.APIClients.SimFin;

public class SimFinCashFlowStatement : SimFinFinancialStatement
{
    public SimFinCashFlowStatement(IList<string> columns, IList<object> dataPoints) : base(columns, dataPoints)
    {
    }

    public decimal NetIncomeStartingLine { get; set; }
    public decimal NetIncome { get; set; }
    public decimal NetIncomefromDiscontinuedOperations { get; set; }
    public decimal OtherAdjustments { get; set; }
    public decimal DepreciationAmortization { get; set; }
    public decimal NonCashItems { get; set; }
    public decimal StockBasedCompensation { get; set; }
    public decimal DeferredIncomeTaxes { get; set; }
    public decimal OtherNonCashAdjustments { get; set; }
    public decimal ChangeinWorkingCapital { get; set; }
    public decimal ChangeinAccountsReceivable { get; set; }
    public decimal ChangeinInventories { get; set; }
    public decimal ChangeinAccountsPayable { get; set; }
    public decimal ChangeinOther { get; set; }
    public decimal NetCashfromDiscontinuedOperationsOperating { get; set; }
    public decimal NetCashfromOperatingActivities { get; set; }
    public decimal ChangeinFixedAssetsIntangibles { get; set; }
    public decimal DispositionofFixedAssetsIntangibles { get; set; }
    public decimal DispositionofFixedAssets { get; set; }
    public decimal DispositionofIntangibleAssets { get; set; }
    public decimal AcquisitionofFixedAssetsIntangibles { get; set; }
    public decimal PurchaseofFixedAssets { get; set; }
    public decimal AcquisitionofIntangibleAssets { get; set; }
    public decimal OtherChangeinFixedAssetsIntangibles { get; set; }
    public decimal NetChangeinLongTermInvestment { get; set; }
    public decimal DecreaseinLongTermInvestment { get; set; }
    public decimal IncreaseinLongTermInvestment { get; set; }
    public decimal NetCashfromAcquisitionsDivestitures { get; set; }
    public decimal NetCashfromDivestitures { get; set; }
    public decimal CashforAcquisitionofSubsidiaries { get; set; }
    public decimal CashforJointVentures { get; set; }
    public decimal NetCashfromOtherAcquisitions { get; set; }
    public decimal OtherInvestingActivities { get; set; }
    public decimal NetCashfromDiscontinuedOperationsInvesting { get; set; }
    public decimal NetCashfromInvestingActivities { get; set; }
    public decimal DividendsPaid { get; set; }
    public decimal CashfromRepaymentofDebt { get; set; }
    public decimal CashfromRepaymentofShortTermDebtNet { get; set; }
    public decimal CashfromRepaymentofLongTermDebtNet { get; set; }
    public decimal RepaymentsofLongTermDebt { get; set; }
    public decimal CashfromLongTermDebt { get; set; }
    public decimal CashfromRepurchaseofEquity { get; set; }
    public decimal IncreaseinCapitalStock { get; set; }
    public decimal DecreaseinCapitalStock { get; set; }
    public decimal OtherFinancingActivities { get; set; }
    public decimal NetCashfromDiscontinuedOperationsFinancing { get; set; }
    public decimal NetCashfromFinancingActivities { get; set; }
    public decimal NetCashBeforeDiscOperationsandFX { get; set; }
    public decimal ChangeinCashfromDiscOperationsandOther { get; set; }
    public decimal NetCashBeforeFX { get; set; }
    public decimal EffectofForeignExchangeRates { get; set; }
    public decimal NetChangeinCash { get; set; }
}

