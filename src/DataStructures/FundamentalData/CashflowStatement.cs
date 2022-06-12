using System;
using System.ComponentModel.DataAnnotations;

namespace DataStructures.FundamentalData
{
    public class CashFlowStatement : FinancialStatement
    {
        public virtual decimal NetIncomeStartingLine { get; set; }
        public virtual decimal NetIncome { get; set; }
        public virtual decimal NetIncomefromDiscontinuedOperations { get; set; }
        public virtual decimal OtherAdjustments { get; set; }

        public virtual decimal DepreciationAmortization { get; set; }
        public virtual decimal NonCashItems { get; set; }
        public virtual decimal StockBasedCompensation { get; set; }
        public virtual decimal DeferredIncomeTaxes { get; set; }
        public virtual decimal OtherNonCashAdjustments { get; set; }
        public virtual decimal ChangeinWorkingCapital { get; set; }
        public virtual decimal ChangeinAccountsReceivable { get; set; }
        public virtual decimal ChangeinInventories { get; set; }
        public virtual decimal ChangeinAccountsPayable { get; set; }
        public virtual decimal ChangeinOther { get; set; }
        public virtual decimal NetCashfromDiscontinuedOperationsOperating { get; set; }
        public virtual decimal NetCashfromOperatingActivities { get; set; } // Operating Cashflow
        public virtual decimal ChangeinFixedAssetsIntangibles { get; set; }
        public virtual decimal DispositionofFixedAssetsIntangibles { get; set; }
        public virtual decimal DispositionofFixedAssets { get; set; }
        public virtual decimal DispositionofIntangibleAssets { get; set; }
        public virtual decimal AcquisitionofFixedAssetsIntangibles { get; set; }
        public virtual decimal PurchaseofFixedAssets { get; set; }
        public virtual decimal AcquisitionofIntangibleAssets { get; set; }
        public virtual decimal OtherChangeinFixedAssetsIntangibles { get; set; }
        public virtual decimal NetChangeinLongTermInvestment { get; set; }
        public virtual decimal DecreaseinLongTermInvestment { get; set; }
        public virtual decimal IncreaseinLongTermInvestment { get; set; }
        public virtual decimal NetCashfromAcquisitionsDivestitures { get; set; }
        public virtual decimal NetCashfromDivestitures { get; set; }
        public virtual decimal CashforAcquisitionofSubsidiaries { get; set; }
        public virtual decimal CashforJointVentures { get; set; }
        public virtual decimal NetCashfromOtherAcquisitions { get; set; }
        public virtual decimal OtherInvestingActivities { get; set; }
        public virtual decimal NetCashfromDiscontinuedOperationsInvesting { get; set; }
        public virtual decimal NetCashfromInvestingActivities { get; set; } // Investing Cashflow
        public virtual decimal DividendsPaid { get; set; }
        public virtual decimal CashfromRepaymentofDebt { get; set; }
        public virtual decimal CashfromRepaymentofShortTermDebtNet { get; set; }
        public virtual decimal CashfromRepaymentofLongTermDebtNet { get; set; }
        public virtual decimal RepaymentsofLongTermDebt { get; set; }
        public virtual decimal CashfromLongTermDebt { get; set; }
        public virtual decimal CashfromRepurchaseofEquity { get; set; }
        public virtual decimal IncreaseinCapitalStock { get; set; }
        public virtual decimal DecreaseinCapitalStock { get; set; }
        public virtual decimal OtherFinancingActivities { get; set; }
        public virtual decimal NetCashfromDiscontinuedOperationsFinancing { get; set; }
        public virtual decimal NetCashfromFinancingActivities { get; set; } // Financing Cashflow
        public virtual decimal NetCashBeforeDiscOperationsandFX { get; set; }
        public virtual decimal ChangeinCashfromDiscOperationsandOther { get; set; }
        public virtual decimal NetCashBeforeFX { get; set; }
        public virtual decimal EffectofForeignExchangeRates { get; set; }
        public virtual decimal NetChangeinCash { get; set; }

        /// <summary>
        /// Free Cash Flow
        /// Cashflow that is available to all providers of the company's capital, both debt and equity.
        /// Interest is included already.
        /// = OperatingCashFlow - CapitalExpenditure
        /// </summary>
        public decimal FreeCashFlow { get; set; } // not in SimFin -> Operating CF - Capital Expenditure

        public CashFlowStatement()
        {
            
        }

        public override string ToString()
        {
            return base.ToString() + " Operating Cash Flow: " + NetCashfromOperatingActivities + ", Investing Cash Flow: " + NetCashfromInvestingActivities + ", Financing Cash Flow: " + NetCashfromFinancingActivities + ", Free Cash Flow: " + FreeCashFlow;
        }
    }
}
