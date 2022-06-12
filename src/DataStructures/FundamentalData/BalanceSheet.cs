using System;
namespace DataStructures.FundamentalData
{
    public class BalanceSheet : FinancialStatement
    {
        public virtual decimal CashCashEquivalentsShortTermInvestments { get; set; }
        public virtual decimal CashCashEquivalents { get; set; }
        public virtual decimal ShortTermInvestments { get; set; }
        public virtual decimal AccountsNotesReceivable { get; set; }
        public virtual decimal AccountsReceivableNet { get; set; }
        public virtual decimal NotesReceivableNet { get; set; }
        public virtual decimal UnbilledRevenues { get; set; }
        public virtual decimal Inventories { get; set; }
        public virtual decimal RawMaterials { get; set; }
        public virtual decimal WorkInProcess { get; set; }
        public virtual decimal FinishedGoods { get; set; }
        public virtual decimal OtherInventory { get; set; }
        public virtual decimal OtherShortTermAssets { get; set; }
        public virtual decimal PrepaidExpenses { get; set; }
        public virtual decimal DerivativeHedgingAssetsShortTerm { get; set; }
        public virtual decimal AssetsHeldforSale { get; set; }
        public virtual decimal DeferredTaxAssetsShortTerm { get; set; }
        public virtual decimal IncomeTaxesReceivable { get; set; }
        public virtual decimal DiscontinuedOperationsShortTerm { get; set; }
        public virtual decimal MiscShortTermAssets { get; set; }
        public virtual decimal TotalCurrentAssets { get; set; }
        public virtual decimal PropertyPlantEquipmentNet { get; set; }
        public virtual decimal PropertyPlantEquipment { get; set; }
        public virtual decimal AccumulatedDepreciation { get; set; }
        public virtual decimal LongTermInvestmentsReceivables { get; set; }
        public virtual decimal LongTermInvestments { get; set; }
        public virtual decimal LongTermMarketableSecurities { get; set; }
        public virtual decimal LongTermReceivables { get; set; }
        public virtual decimal OtherLongTermAssets { get; set; }
        public virtual decimal IntangibleAssets { get; set; }
        public virtual decimal Goodwill { get; set; }
        public virtual decimal OtherIntangibleAssets { get; set; }
        public virtual decimal PrepaidExpense { get; set; }
        public virtual decimal DeferredTaxAssetsLongTerm { get; set; }
        public virtual decimal DerivativeHedgingAssetsLongTerm { get; set; }
        public virtual decimal PrepaidPensionCosts { get; set; }
        public virtual decimal DiscontinuedOperationsLongTerm { get; set; }
        public virtual decimal InvestmentsinAffiliates { get; set; }
        public virtual decimal MiscLongTermAssets { get; set; }
        public virtual decimal TotalNoncurrentAssets { get; set; }
        public virtual decimal TotalAssets { get; set; }
        public virtual decimal PayablesAccruals { get; set; }
        public virtual decimal AccountsPayable { get; set; }
        public virtual decimal AccruedTaxes { get; set; }
        public virtual decimal InterestDividendsPayable { get; set; }
        public virtual decimal OtherPayablesAccruals { get; set; }
        public virtual decimal ShortTermDebt { get; set; }
        public virtual decimal ShortTermBorrowings { get; set; }
        public virtual decimal ShortTermCapitalLeases { get; set; }
        public virtual decimal CurrentPortionofLongTermDebt { get; set; }
        public virtual decimal OtherShortTermLiabilities { get; set; }
        public virtual decimal DeferredRevenueShortTerm { get; set; }
        public virtual decimal LiabilitiesfromDerivativesHedgingShortTerm { get; set; }
        public virtual decimal DeferredTaxLiabilitiesShortTerm { get; set; }
        public virtual decimal LiabilitiesfromDiscontinuedOperationsShortTerm { get; set; }
        public virtual decimal MiscShortTermLiabilities { get; set; }
        public virtual decimal TotalCurrentLiabilities { get; set; }
        public virtual decimal LongTermDebt { get; set; }
        public virtual decimal LongTermBorrowings { get; set; }
        public virtual decimal LongTermCapitalLeases { get; set; }
        public virtual decimal OtherLongTermLiabilities { get; set; }
        public virtual decimal AccruedLiabilities { get; set; }
        public virtual decimal PensionLiabilities { get; set; }
        public virtual decimal Pensions { get; set; }
        public virtual decimal OtherPostRetirementBenefits { get; set; }
        public virtual decimal DeferredCompensation { get; set; }
        public virtual decimal DeferredRevenueLongTerm { get; set; }
        public virtual decimal DeferredTaxLiabilitiesLongTerm { get; set; }
        public virtual decimal LiabilitiesfromDerivativesHedgingLongTerm { get; set; }
        public virtual decimal LiabilitiesfromDiscontinuedOperationsLongTerm { get; set; }
        public virtual decimal MiscLongTermLiabilities { get; set; }
        public virtual decimal TotalNoncurrentLiabilities { get; set; }
        public virtual decimal TotalLiabilities { get; set; }
        public virtual decimal PreferredEquity { get; set; }
        public virtual decimal ShareCapitalAdditionalPaidInCapital { get; set; }
        public virtual decimal CommonStock { get; set; }
        public virtual decimal AdditionalPaidinCapital { get; set; }
        public virtual decimal OtherShareCapital { get; set; }
        public virtual decimal TreasuryStock { get; set; }
        public virtual decimal RetainedEarnings { get; set; }
        public virtual decimal OtherEquity { get; set; }
        public virtual decimal EquityBeforeMinorityInterest { get; set; }
        public virtual decimal MinorityInterest { get; set; }
        public virtual decimal TotalEquity { get; set; }
        public virtual decimal TotalLiabilitiesEquity { get; set; }

        /// <summary>
        /// Nettoumlaufvermögen
        /// NWC
        /// = Receivables + Inventories - Payables
        /// </summary>
        public decimal NetWorkingCapital { get { return AccountsNotesReceivable + Inventories - AccountsPayable; } }
        /// <summary>
        /// Capital Employed
        /// = Total noncurrent Assets + Receivables + Inventories - Payables
        /// </summary>
        public decimal CapitalEmployed { get { return TotalNoncurrentAssets + NetWorkingCapital; } }

        public BalanceSheet()
        {
            
        }

        public override string ToString()
        {
            return base.ToString() + "Assets: " + TotalAssets + ", Liabilities: " + TotalLiabilities;
        }
    }
}
