using System;

namespace DataImport.APIClients.SimFin;

public class SimFinBalanceSheet : SimFinFinancialStatement
{
    public SimFinBalanceSheet(IList<string> columns, IList<object> dataPoints) : base(columns, dataPoints)
    {
    }

    public decimal CashCashEquivalentsShortTermInvestments { get; set; }
    public decimal CashCashEquivalents { get; set; }
    public decimal ShortTermInvestments { get; set; }
    public decimal AccountsNotesReceivable { get; set; }
    public decimal AccountsReceivableNet { get; set; }
    public decimal NotesReceivableNet { get; set; }
    public decimal UnbilledRevenues { get; set; }
    public decimal Inventories { get; set; }
    public decimal RawMaterials { get; set; }
    public decimal WorkInProcess { get; set; }
    public decimal FinishedGoods { get; set; }
    public decimal OtherInventory { get; set; }
    public decimal OtherShortTermAssets { get; set; }
    public decimal PrepaidExpenses { get; set; }
    public decimal DerivativeHedgingAssetsShortTerm { get; set; }
    public decimal AssetsHeldforSale { get; set; }
    public decimal DeferredTaxAssetsShortTerm { get; set; }
    public decimal IncomeTaxesReceivable { get; set; }
    public decimal DiscontinuedOperationsShortTerm { get; set; }
    public decimal MiscShortTermAssets { get; set; }
    public decimal TotalCurrentAssets { get; set; }
    public decimal PropertyPlantEquipmentNet { get; set; }
    public decimal PropertyPlantEquipment { get; set; }
    public decimal AccumulatedDepreciation { get; set; }
    public decimal LongTermInvestmentsReceivables { get; set; }
    public decimal LongTermInvestments { get; set; }
    public decimal LongTermMarketableSecurities { get; set; }
    public decimal LongTermReceivables { get; set; }
    public decimal OtherLongTermAssets { get; set; }
    public decimal IntangibleAssets { get; set; }
    public decimal Goodwill { get; set; }
    public decimal OtherIntangibleAssets { get; set; }
    public decimal PrepaidExpense { get; set; }
    public decimal DeferredTaxAssetsLongTerm { get; set; }
    public decimal DerivativeHedgingAssetsLongTerm { get; set; }
    public decimal PrepaidPensionCosts { get; set; }
    public decimal DiscontinuedOperationsLongTerm { get; set; }
    public decimal InvestmentsinAffiliates { get; set; }
    public decimal MiscLongTermAssets { get; set; }
    public decimal TotalNoncurrentAssets { get; set; }
    public decimal TotalAssets { get; set; }
    public decimal PayablesAccruals { get; set; }
    public decimal AccountsPayable { get; set; }
    public decimal AccruedTaxes { get; set; }
    public decimal InterestDividendsPayable { get; set; }
    public decimal OtherPayablesAccruals { get; set; }
    public decimal ShortTermDebt { get; set; }
    public decimal ShortTermBorrowings { get; set; }
    public decimal ShortTermCapitalLeases { get; set; }
    public decimal CurrentPortionofLongTermDebt { get; set; }
    public decimal OtherShortTermLiabilities { get; set; }
    public decimal DeferredRevenueShortTerm { get; set; }
    public decimal LiabilitiesfromDerivativesHedgingShortTerm { get; set; }
    public decimal DeferredTaxLiabilitiesShortTerm { get; set; }
    public decimal LiabilitiesfromDiscontinuedOperationsShortTerm { get; set; }
    public decimal MiscShortTermLiabilities { get; set; }
    public decimal TotalCurrentLiabilities { get; set; }
    public decimal LongTermDebt { get; set; }
    public decimal LongTermBorrowings { get; set; }
    public decimal LongTermCapitalLeases { get; set; }
    public decimal OtherLongTermLiabilities { get; set; }
    public decimal AccruedLiabilities { get; set; }
    public decimal PensionLiabilities { get; set; }
    public decimal Pensions { get; set; }
    public decimal OtherPostRetirementBenefits { get; set; }
    public decimal DeferredCompensation { get; set; }
    public decimal DeferredRevenueLongTerm { get; set; }
    public decimal DeferredTaxLiabilitiesLongTerm { get; set; }
    public decimal LiabilitiesfromDerivativesHedgingLongTerm { get; set; }
    public decimal LiabilitiesfromDiscontinuedOperationsLongTerm { get; set; }
    public decimal MiscLongTermLiabilities { get; set; }
    public decimal TotalNoncurrentLiabilities { get; set; }
    public decimal TotalLiabilities { get; set; }
    public decimal PreferredEquity { get; set; }
    public decimal ShareCapitalAdditionalPaidInCapital { get; set; }
    public decimal CommonStock { get; set; }
    public decimal AdditionalPaidinCapital { get; set; }
    public decimal OtherShareCapital { get; set; }
    public decimal TreasuryStock { get; set; }
    public decimal RetainedEarnings { get; set; }
    public decimal OtherEquity { get; set; }
    public decimal EquityBeforeMinorityInterest { get; set; }
    public decimal MinorityInterest { get; set; }
    public decimal TotalEquity { get; set; }
    public decimal TotalLiabilitiesEquity { get; set; }
}

