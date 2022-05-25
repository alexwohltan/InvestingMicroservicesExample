using System;
namespace Benchmark
{
	public class CompanyFundamentals
	{
        public string? Ticker { get; set; }
        public string? Name { get; set; }

        public DateTime LatestFilingDate { get; set; }

        #region Metrics

        #region Profitability & Returns

        [CompanyFundamentalsMetric("Return on Equity", DisplayType = DisplayType.Percent, Calculation = "Net Income / Equity")]
        public decimal? ReturnOnEquity { get; set; }

        [CompanyFundamentalsMetric("Return on Sales", DisplayType = DisplayType.Percent, Calculation = "Net Income / Revenue")]
        public decimal? ReturnOnSales { get; set; }

        [CompanyFundamentalsMetric("EBIT Margin", DisplayType = DisplayType.Percent, Calculation = "EBIT / Revenue")]
        public decimal? EBITMargin { get; set; }

        [CompanyFundamentalsMetric("EBITDA Margin", DisplayType = DisplayType.Percent, Calculation = "EBITDA / Revenue")]
        public decimal? EBITDAMargin { get; set; }

        [CompanyFundamentalsMetric("Capital Turnover", DisplayType = DisplayType.Percent, Calculation = "Revenue / Total Assets")]
        public decimal? CapitalTurnover { get; set; }

        [CompanyFundamentalsMetric("Total Return on Investment", DisplayType = DisplayType.Percent, Calculation = "(Net Income + Interest Expense) / Average of Capital")]
        public decimal? TotalReturnOnInvestment { get; set; }

        [CompanyFundamentalsMetric("Return on Capital Employed", DisplayType = DisplayType.Percent, Calculation = "Operating Income / Capital Employed")]
        public decimal? ReturnOnCapitalEmployed { get; set; }

        [CompanyFundamentalsMetric("Cashflow Turnover Ratio", DisplayType = DisplayType.Percent, Calculation = "Operating Cashflow / Revenue")]
        public decimal? CashflowTurnoverRatio { get; set; }

        #endregion

        #region Financial Stability

        [CompanyFundamentalsMetric("Equity Ratio", DisplayType = DisplayType.Percent, Calculation = "Equity / Sum of Capital")]
        public decimal? EquityRatio { get; set; }

        [CompanyFundamentalsMetric("Gearing", DisplayType = DisplayType.Percent, Calculation = "(Debt - Cash) / Equity")]
        public decimal? Gearing { get; set; }

        [CompanyFundamentalsMetric("Dynamic Gearing", DisplayType = DisplayType.Percent, Calculation = "(Debt - Cash) / Free Cashflow")]
        public decimal? DynamicGearing { get; set; }

        [CompanyFundamentalsMetric("Net Debt / EBITDA", DisplayType = DisplayType.Percent, Calculation = "(Debt - Cash) / EBITDA")]
        public decimal? NetDebtoverEBITDA { get; set; }

        [CompanyFundamentalsMetric("CAPEX / Operating Cashflow", DisplayType = DisplayType.Percent, Calculation = "-CAPEX / Operating Cashflow")]
        public decimal? CAPEXoverOperatingCashflow { get; set; }

        [CompanyFundamentalsMetric("Current Assets / Assets", DisplayType = DisplayType.Percent, Calculation = "Current Assets / Total Assets")]
        public decimal? CurrentAssetsoverTotalAssets { get; set; }

        [CompanyFundamentalsMetric("Asset Coverage Ratio 1", DisplayType = DisplayType.Percent, Calculation = "Equity / Total Fixed Assets")]
        public decimal? EquityToAssetRatio { get; set; }

        [CompanyFundamentalsMetric("Asset Coverage Ratio 2", DisplayType = DisplayType.Percent, Calculation = "(Equity + Noncurrent Liablities) / Total Fixed Assets")]
        public decimal? EquityAndNoncurrentLiabilitiesToAssetRatio { get; set; }

        #endregion

        #region Working Capital Management

        [CompanyFundamentalsMetric("Debtor Terms", DisplayType = DisplayType.Percent, Calculation = "Avg. Receivables / Revenue * 360")]
        public decimal? DebtorTerms { get; set; }

        [CompanyFundamentalsMetric("Days Payable Outstanding", DisplayType = DisplayType.Percent, Calculation = "Payables / Cost of Revenue * 360")]
        public decimal? DaysPayableOutstanding { get; set; }

        [CompanyFundamentalsMetric("Liquidity 1 Degree", DisplayType = DisplayType.Percent, Calculation = "(Cash + Short Term Investments) / Current Liabilities")]
        public decimal? Liquidity1Degree { get; set; }

        [CompanyFundamentalsMetric("Liquidity 2 Degree", DisplayType = DisplayType.Percent, Calculation = "(Cash + Short Term Investments + Receivables) / Current Liabilities")]
        public decimal? Liquidity2Degree { get; set; }

        [CompanyFundamentalsMetric("Liquidity 3 Degree", DisplayType = DisplayType.Percent, Calculation = "Total Current Assets / Current Liabilities")]
        public decimal? Liquidity3Degree { get; set; }

        [CompanyFundamentalsMetric("Inventory Intensity", DisplayType = DisplayType.Percent, Calculation = "Inventories / Assets")]
        public decimal? InventoryIntensity { get; set; }

        #endregion

        #region Valuation
        // need to implement stock prices first
        #endregion

        #endregion
    }
}

