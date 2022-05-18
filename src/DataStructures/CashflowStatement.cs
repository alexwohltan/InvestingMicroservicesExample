using System;
using System.ComponentModel.DataAnnotations;

namespace DataStructures
{
    public class CashFlowStatement : FinancialStatement
    {
        /// <summary>
        /// Abschreibungen
        /// </summary>
        public decimal DepreciationAmortization { get; set; }

        public decimal NonCashItems { get; set; }

        public decimal ChangeInWorkingCapital { get; set; }
        public decimal ChangeInAccountsReceivable { get; set; }
        public decimal ChangeInInventories { get; set; }
        public decimal ChangeInAccountsPayable { get; set; }
        public decimal ChangeInOther { get; set; }

        /// <summary>
        /// Aktienbasierte Vergütung
        /// Mitarbeiter werden mit Aktien bezahlt.
        /// </summary>
        public decimal StockBasedCompensation { get; set; }
        /// <summary>
        /// Operating Cash Flow
        /// </summary>
        public decimal OperatingCashFlow { get; set; }

        /// <summary>
        /// Investitionsausgaben (CAPEX)
        /// </summary>
        public decimal CapitalExpenditure { get; set; }
        /// <summary>
        /// Akqusitionen und Desinvestitionen
        /// </summary>
        public decimal AcquisitionsAndDisposals { get; set; }
        /// <summary>
        /// ???
        /// </summary>
        public decimal InvestmentPurchasesAndSales { get; set; }
        /// <summary>
        /// Investing Cash Flow
        /// </summary>
        public decimal InvestingCashFlow { get; set; }

        public decimal RepaymentOfDebt { get; set; } // Issuance 
        public decimal BuybacksOfShares { get; set; } // Issuance
        public decimal DividendPaments { get; set; }
        public decimal FinancingCashFlow { get; set; }
        public decimal EffectOfForexChangesOnCash { get; set; }
        public decimal NetCashFlow { get; set; } // Change in Cash
        /// <summary>
        /// Free Cash Flow
        /// Cashflow that is available to all providers of the company's capital, both debt and equity.
        /// Interest is included already.
        /// = OperatingCashFlow - CapitalExpenditure
        /// </summary>
        public decimal FreeCashFlow { get; set; }
        public decimal NetCash { get; set; } // Marketcap

        public CashFlowStatement()
        {
            
        }

        public override string ToString()
        {
            return base.ToString() + " Operating Cash Flow: " + OperatingCashFlow + ", Investing Cash Flow: " + InvestingCashFlow + ", Financing Cash Flow: " + FinancingCashFlow + ", Free Cash Flow: " + FreeCashFlow;
        }
    }
}
