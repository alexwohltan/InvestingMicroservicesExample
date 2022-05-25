using System;
namespace Benchmark
{
	public static class CompanyExtensions
	{
		public static CompanyFundamentals ToCompanyFundamentals(this DataStructures.Company company)
        {
			var fundamentals = new CompanyFundamentals();

			fundamentals.Name = company.Name;
			fundamentals.Ticker = company.Ticker;

			var lastFiling = company.Filings.OrderBy(e => e.FilingDate).LastOrDefault();

			if (lastFiling == null)
				throw new ArgumentException("Filings cannot be empty");

			fundamentals.LatestFilingDate = lastFiling.FilingDate;

            #region Profitability & Returns
            fundamentals.ReturnOnEquity = lastFiling.ReturnOnEquity == 0 ? 0 : lastFiling.ReturnOnEquity;
            fundamentals.ReturnOnSales = lastFiling.ReturnOnSales == 0 ? 0 : lastFiling.ReturnOnSales;
            fundamentals.EBITMargin = lastFiling.EBITMargin == 0 ? 0 : lastFiling.EBITMargin;
            fundamentals.EBITDAMargin = lastFiling.EBITDAMargin == 0 ? 0 : lastFiling.EBITDAMargin;
            fundamentals.CapitalTurnover = lastFiling.CapitalTurnover == 0 ? 0 : lastFiling.CapitalTurnover;
            fundamentals.TotalReturnOnInvestment = lastFiling.TotalReturnOnInvestment == 0 ? 0 : lastFiling.TotalReturnOnInvestment;
            fundamentals.ReturnOnCapitalEmployed = lastFiling.ReturnOnCapitalEmployed == 0 ? 0 : lastFiling.ReturnOnCapitalEmployed;
            fundamentals.CashflowTurnoverRatio = lastFiling.CashflowTurnoverRatio == 0 ? 0 : lastFiling.CashflowTurnoverRatio;
            #endregion

            #region Financial Stability
            fundamentals.EquityRatio = lastFiling.EquityRatio == 0 ? 0 : lastFiling.EquityRatio;
            fundamentals.Gearing = lastFiling.Gearing == 0 ? 0 : lastFiling.Gearing;
            fundamentals.DynamicGearing = lastFiling.DynamicGearing == 0 ? 0 : lastFiling.DynamicGearing;
            fundamentals.NetDebtoverEBITDA = lastFiling.NetDebtoverEBITDA == 0 ? 0 : lastFiling.NetDebtoverEBITDA;
            fundamentals.CAPEXoverOperatingCashflow = lastFiling.CAPEXoverOperatingCashflow == 0 ? 0 : lastFiling.CAPEXoverOperatingCashflow;
            fundamentals.CurrentAssetsoverTotalAssets = lastFiling.CurrentAssetsoverTotalAssets == 0 ? 0 : lastFiling.CurrentAssetsoverTotalAssets;
            fundamentals.EquityToAssetRatio = lastFiling.EquityToAssetRatio == 0 ? 0 : lastFiling.EquityToAssetRatio;
            fundamentals.EquityAndNoncurrentLiabilitiesToAssetRatio = lastFiling.EquityAndNoncurrentLiabilitiesToAssetRatio == 0 ? 0 : lastFiling.EquityAndNoncurrentLiabilitiesToAssetRatio;
            #endregion

            #region Working Capital Management
            fundamentals.DebtorTerms = lastFiling.DebtorTerms == 0 ? 0 : lastFiling.DebtorTerms;
            fundamentals.DaysPayableOutstanding = lastFiling.DaysPayableOutstanding == 0 ? 0 : lastFiling.DaysPayableOutstanding;
            fundamentals.Liquidity1Degree = lastFiling.Liquidity1Degree == 0 ? 0 : lastFiling.Liquidity1Degree;
            fundamentals.Liquidity2Degree = lastFiling.Liquidity2Degree == 0 ? 0 : lastFiling.Liquidity2Degree;
            fundamentals.Liquidity3Degree = lastFiling.Liquidity3Degree == 0 ? 0 : lastFiling.Liquidity3Degree;
            fundamentals.InventoryIntensity = lastFiling.InventoryIntensity == 0 ? 0 : lastFiling.InventoryIntensity;
            #endregion

            #region Valuation
            #endregion

            return fundamentals;
        }

        public static CompanyFundamentals ToCompanyFundamentals(this DataStructures.Filing filing, string ticker, string companyName)
        {
            var fundamentals = new CompanyFundamentals();

            fundamentals.Name = companyName;
            fundamentals.Ticker = ticker;

            fundamentals.LatestFilingDate = filing.FilingDate;

            #region Profitability & Returns
            fundamentals.ReturnOnEquity = filing.ReturnOnEquity;
            fundamentals.ReturnOnSales = filing.ReturnOnSales;
            fundamentals.EBITMargin = filing.EBITMargin;
            fundamentals.EBITDAMargin = filing.EBITDAMargin;
            fundamentals.CapitalTurnover = filing.CapitalTurnover;
            fundamentals.TotalReturnOnInvestment = filing.TotalReturnOnInvestment;
            fundamentals.ReturnOnCapitalEmployed = filing.ReturnOnCapitalEmployed;
            fundamentals.CashflowTurnoverRatio = filing.CashflowTurnoverRatio;
            #endregion

            #region Financial Stability
            fundamentals.EquityRatio = filing.EquityRatio;
            fundamentals.Gearing = filing.Gearing;
            fundamentals.DynamicGearing = filing.DynamicGearing;
            fundamentals.NetDebtoverEBITDA = filing.NetDebtoverEBITDA;
            fundamentals.CAPEXoverOperatingCashflow = filing.CAPEXoverOperatingCashflow;
            fundamentals.CurrentAssetsoverTotalAssets = filing.CurrentAssetsoverTotalAssets;
            fundamentals.EquityToAssetRatio = filing.EquityToAssetRatio;
            fundamentals.EquityAndNoncurrentLiabilitiesToAssetRatio = filing.EquityAndNoncurrentLiabilitiesToAssetRatio;
            #endregion

            #region Working Capital Management
            fundamentals.DebtorTerms = filing.DebtorTerms;
            fundamentals.DaysPayableOutstanding = filing.DaysPayableOutstanding;
            fundamentals.Liquidity1Degree = filing.Liquidity1Degree;
            fundamentals.Liquidity2Degree = filing.Liquidity2Degree;
            fundamentals.Liquidity3Degree = filing.Liquidity3Degree;
            fundamentals.InventoryIntensity = filing.InventoryIntensity;
            #endregion

            #region Valuation
            #endregion

            return fundamentals;
        }
    }
}

