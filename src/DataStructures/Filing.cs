using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace DataStructures
{
    public class Filing : FinancialStatement
    {
        [ForeignKey("Company")]
        public virtual int CompanyID { get; set; }
        [JsonIgnore]
        public virtual Company Company { get; set; }

        public virtual IncomeStatement IncomeStatement { get; set; }
        public virtual BalanceSheet BalanceSheet { get; set; }
        public virtual CashFlowStatement CashflowStatement { get; set; }

        #region Metrics for Profitability and Returns

        // An investment needs to make money. These metrics help to understand if the investment is profitable.

        /// <summary>
        /// Eigenkapitalrendite
        /// = Net Income for common shareholders / average of shareholders equity
        ///
        /// Can be increased by increase in income but also by decreasing equity (-> buyback shares or give out dividends)
        /// </summary>
        [JsonIgnore]
        public decimal ReturnOnEquity => IncomeStatement.NetIncomeCom / BalanceSheet.TotalEquity;

        /// <summary>
        /// Umsatzrendite
        /// = Net Income / Revenue
        /// </summary>
        [JsonIgnore]
        public decimal ReturnOnSales => IncomeStatement.NetIncome / IncomeStatement.Revenue;

        /// <summary>
        /// EBIT / Revenue
        /// </summary>
        [JsonIgnore]
        public decimal EBITMargin => IncomeStatement.EBITMargin;
        /// <summary>
        /// EBITDA / Revenue
        /// </summary>
        [JsonIgnore]
        public decimal EBITDAMargin => IncomeStatement.EBITDAMargin;

        /// <summary>
        /// Kapitalumschlag
        /// = Revenue / average of capital
        /// </summary>
        [JsonIgnore]
        public decimal CapitalTurnover => IncomeStatement.Revenue / BalanceSheet.TotalAssets;

        /// <summary>
        /// Gesamtkapitalrendite
        /// = (Net Income + Interest Expense) / average of capital
        /// </summary>
        [JsonIgnore]
        public decimal TotalReturnOnInvestment => (IncomeStatement.NetIncome + IncomeStatement.InterestExpense) / BalanceSheet.TotalAssets;

        /// <summary>
        /// Return on Capital Employed
        /// = EBIT / Capital Employed
        /// </summary>
        [JsonIgnore]
        public decimal ReturnOnCapitalEmployed => IncomeStatement.OperatingIncome / BalanceSheet.CapitalEmployed;

        /// <summary>
        /// Umsatzverdienstrate
        /// = Operating Cashflow / Revenue
        /// </summary>
        [JsonIgnore]
        public decimal CashflowTurnoverRatio => CashflowStatement.OperatingCashFlow / IncomeStatement.Revenue;

        #endregion

        #region Metrics for Financial Stability

        // An investment also needs stable financing and capital structure.

        /// <summary>
        /// Eigenkapitalquote
        /// = Equity / sum of capital
        /// </summary>
        [JsonIgnore]
        public decimal EquityRatio => BalanceSheet.TotalShareholdersEquity / BalanceSheet.TotalAssets;

        /// <summary>
        /// Verschuldungsgrad
        /// = (Debt - Cash) / Equity
        /// </summary>
        [JsonIgnore]
        public decimal Gearing => (BalanceSheet.TotalDebt - BalanceSheet.CashAndCashEquivalents) / BalanceSheet.TotalEquity;

        /// <summary>
        /// Dynamischer Verschuldungsgrad
        /// = (Debt - Cash) / Free Cashflow
        /// </summary>
        [JsonIgnore]
        public decimal DynamicGearing => (BalanceSheet.TotalDebt - BalanceSheet.CashAndCashEquivalents) / CashflowStatement.FreeCashFlow;

        /// <summary>
        /// Nettoverschuldung / EBITDA
        /// = (Debt - Cash) / EBITDA
        /// </summary>
        [JsonIgnore]
        public decimal NetDebtoverEBITDA => (BalanceSheet.TotalDebt - BalanceSheet.CashAndCashEquivalents) / IncomeStatement.EBITDA;

        /// <summary>
        /// Sachinvestitionsquote
        /// = CAPEX / Operating Cashflow
        /// </summary>
        [JsonIgnore]
        public decimal CAPEXoverOperatingCashflow => (CashflowStatement.CapitalExpenditure * (decimal)-1) / CashflowStatement.OperatingCashFlow;

        /// <summary>
        /// Umlaufintensität
        /// = Current Assets / Total Assets
        /// </summary>
        [JsonIgnore]
        public decimal CurrentAssetsoverTotalAssets => BalanceSheet.TotalCurrentAssets / BalanceSheet.TotalAssets;

        /// <summary>
        /// Anlagendeckungsgrad 1
        /// = Equity / Total Noncurrent Assets
        /// </summary>
        [JsonIgnore]
        public decimal EquityToAssetRatio => (BalanceSheet.TotalAssets - BalanceSheet.TotalLiabilities) / BalanceSheet.TotalNonCurrentAssets;
        /// <summary>
        /// Anlagendeckungsgrad 2
        /// = (Equity + Noncurrent Liabilities) / Total Noncurrent Assets
        /// </summary>
        [JsonIgnore]
        public decimal EquityAndNoncurrentLiabilitiesToAssetRatio => (BalanceSheet.TotalAssets - BalanceSheet.TotalCurrentLiabilities) / BalanceSheet.TotalNonCurrentAssets;

        #endregion

        #region Metrics for Working Capital Management

        /// <summary>
        /// Debitorenlaufzeit
        /// = average of Receivables / Revenue * 360
        /// </summary>
        [JsonIgnore]
        public decimal DebtorTerms => BalanceSheet.Receivables / IncomeStatement.Revenue * 360;

        /// <summary>
        /// Kreditorenlaufzeit
        /// = average of Payables / Cost of Revenue * 360
        /// </summary>
        [JsonIgnore]
        public decimal DaysPayableOutstanding => BalanceSheet.Payables / IncomeStatement.CostOfRevenue * 360;

        /// <summary>
        /// Liquidität 1. Grades
        /// = (Cash + short term investments) / current Liabilities
        /// </summary>
        [JsonIgnore]
        public decimal Liquidity1Degree => (BalanceSheet.CashAndCashEquivalents) / BalanceSheet.TotalCurrentLiabilities;

        /// <summary>
        /// Liquidität 2. Grades
        /// = (Cash + short term investments + Receivables) / current Liabilities
        /// </summary>
        [JsonIgnore]
        public decimal Liquidity2Degree => (BalanceSheet.CashAndCashEquivalents + BalanceSheet.Receivables) / BalanceSheet.TotalCurrentLiabilities;

        /// <summary>
        /// Liquidität 3. Grades
        /// = Total Current Assets / current Liabilities
        /// </summary>
        [JsonIgnore]
        public decimal Liquidity3Degree => BalanceSheet.TotalCurrentAssets / BalanceSheet.TotalCurrentLiabilities;

        /// <summary>
        /// Vorratsintensität
        /// = Inventories / Total Assets
        /// </summary>
        [JsonIgnore]
        public decimal InventoryIntensity => BalanceSheet.Inventories / BalanceSheet.TotalAssets;

        #endregion

        #region Metrics for Current Market Evaluation

        #region Equity Metrics

        /// <summary>
        /// Kurs Gewinn Verhältnis
        /// = Stock Price / (Income for Shareholders / Shares outstanding (diluted))
        /// </summary>
        //public decimal PriceToEarningsRatio { get { return StockPrice.CloseAdjusted / IncomeStatement.EPSDiluted; } }
        /// <summary>
        /// Einstandsrendite
        /// = 1 / Price to earnings ratio
        /// </summary>
        //public decimal PriceToEarningsYield { get { return 1 / PriceToEarningsRatio; } }

        /// <summary>
        /// Kurs Buchwert Verhältnis
        /// = Stock Price / (Equity / Shares outstanding (diluted))
        /// </summary>
        //public decimal PriceToBookRatio { get { return StockPrice.CloseAdjusted / (BalanceSheet.TotalShareholdersEquity / IncomeStatement.WeightedAverageShsOutDiluted); } }

        /// <summary>
        /// Kurs Cashflow Verhältnis
        /// = Stock Price / (Operating Cashflow / Shares outstanding (diluted))
        /// </summary>
        //public decimal PriceToCashflowRatio { get { return StockPrice.CloseAdjusted / (CashflowStatement.OperatingCashFlow / IncomeStatement.WeightedAverageShsOutDiluted); } }
        /// <summary>
        /// Kurs Free-Cashflow Verhältnis
        /// = Stock Price / (Free Cashflow / Shares outstanding (diluted))
        /// </summary>
        //public decimal PriceToFreeCashflowRatio { get { return StockPrice.CloseAdjusted / (CashflowStatement.FreeCashFlow / IncomeStatement.WeightedAverageShsOutDiluted); } }

        /// <summary>
        /// Kurs Umsatz Verhältnis
        /// = Stock Price / (Revenue / Shares outstanding (diluted))
        /// </summary>
        //public decimal PriceToRevenueRatio { get { return StockPrice.CloseAdjusted / (IncomeStatement.Revenue / IncomeStatement.WeightedAverageShsOutDiluted); } }

        #endregion

        #region Entity Metrics
        #endregion

        #endregion

        public Filing(IncomeStatement incomeStatement, BalanceSheet balanceSheet, CashFlowStatement cashFlowStatement)
        {
            IncomeStatement = incomeStatement;
            BalanceSheet = balanceSheet;
            CashflowStatement = cashFlowStatement;

            FilingDate = incomeStatement.FilingDate;
            PublishedDate = incomeStatement.PublishedDate;
            RestatedDate = incomeStatement.RestatedDate;

            Currency = incomeStatement.Currency;
        }
        public Filing()
        {
            IncomeStatement = new IncomeStatement();
            BalanceSheet = new BalanceSheet();
            CashflowStatement = new CashFlowStatement();
        }
    }
}
