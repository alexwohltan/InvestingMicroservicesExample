using System;

namespace DataImport.APIClients.SimFin;

public class SimFinProfitLossStatement : SimFinFinancialStatement
{
    public SimFinProfitLossStatement(IList<string> columns, IList<object> dataPoints) : base(columns, dataPoints)
    {
    }

    public decimal Revenue { get; set; }
    public decimal SalesServicesRevenue { get; set; }
    public decimal FinancingRevenue { get; set; }
    public decimal OtherRevenue { get; set; }
    public decimal CostofRevenue { get; set; }
    public decimal CostofGoodsServices { get; set; }
    public decimal CostofFinancingRevenue { get; set; }
    public decimal CostofOtherRevenue { get; set; }
    public decimal GrossProfit { get; set; }
    public decimal OtherOperatingIncome { get; set; }
    public decimal OperatingExpenses { get; set; }
    public decimal SellingGeneralAdministrative { get; set; }
    public decimal SellingMarketing { get; set; }
    public decimal GeneralAdministrative { get; set; }
    public decimal ResearchDevelopment { get; set; }
    public decimal DepreciationAmortization { get; set; }
    public decimal ProvisionforDoubtfulAccounts { get; set; }
    public decimal OtherOperatingExpenses { get; set; }
    public decimal OperatingIncomeLoss { get; set; }
    public decimal NonOperatingIncomeLoss { get; set; }
    public decimal InterestExpenseNet { get; set; }
    public decimal InterestExpense { get; set; }
    public decimal InterestIncome { get; set; }
    public decimal OtherInvestmentIncomeLoss { get; set; }
    public decimal ForeignExchangeGainLoss { get; set; }
    public decimal IncomeLossfromAffiliates { get; set; }
    public decimal OtherNonOperatingIncomeLoss { get; set; }
    public decimal PretaxIncomeLossAdj { get; set; }
    public decimal AbnormalGainsLosses { get; set; }
    public decimal AcquiredInProcessRD { get; set; }
    public decimal MergerAcquisitionExpense { get; set; }
    public decimal AbnormalDerivatives { get; set; }
    public decimal DisposalofAssets { get; set; }
    public decimal EarlyExtinguishmentofDebt { get; set; }
    public decimal AssetWriteDown { get; set; }
    public decimal ImpairmentofGoodwillIntangibles { get; set; }
    public decimal SaleofBusiness { get; set; }
    public decimal LegalSettlement { get; set; }
    public decimal RestructuringCharges { get; set; }
    public decimal SaleofInvestmentsUnrealizedInvestments { get; set; }
    public decimal InsuranceSettlement { get; set; }
    public decimal OtherAbnormalItems { get; set; }
    public decimal PretaxIncomeLoss { get; set; }
    public decimal IncomeTaxExpenseBenefitNet { get; set; }
    public decimal CurrentIncomeTax { get; set; }
    public decimal DeferredIncomeTax { get; set; }
    public decimal TaxAllowanceCredit { get; set; }
    public decimal IncomeLossfromAffiliatesNetofTaxes { get; set; }
    public decimal IncomeLossfromContinuingOperations { get; set; }
    public decimal NetExtraordinaryGainsLosses { get; set; }
    public decimal DiscontinuedOperations { get; set; }
    public decimal AccountingChargesOther { get; set; }
    public decimal IncomeLossInclMinorityInterest { get; set; }
    public decimal MinorityInterest { get; set; }
    public decimal NetIncome { get; set; }
    public decimal PreferredDividends { get; set; }
    public decimal OtherAdjustments { get; set; }
    public decimal NetIncomeCommon { get; set; }
}

