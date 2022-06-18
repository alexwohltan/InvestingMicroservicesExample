using System;
namespace DataStructures.WorkdayAnalysis
{
	public class AccountFiling
	{
        public virtual int Id { get; set; }
        public virtual DateTime Date { get; set; }

        public virtual decimal? Revenue { get; set; }
        public virtual decimal? CostOfRevenue { get; set; }
        public virtual decimal? GrossProfit { get; set; }
        public virtual decimal? SellingGeneralAdministrative { get; set; }
        public virtual decimal? SellingMarketing { get; set; }
        public virtual decimal? GeneralAdministrative { get; set; }
        public virtual decimal? OperatingExpenses { get; set; }
        public virtual decimal? OperatingIncome { get; set; }
        public virtual decimal? PretaxIncomeAdj { get; set; }
        public virtual decimal? PretaxIncome { get; set; }
        public virtual decimal? NetIncome { get; set; }

        public virtual decimal? OperatingCashflow { get; set; }
        public virtual decimal? FinancingCashflow { get; set; }
        public virtual decimal? InvestingCashflow { get; set; }
        public virtual decimal? FreeCashflow { get; set; }

        public virtual decimal? CostOfRevenueRatio => Revenue != null && Revenue != 0 ? CostOfRevenue / Revenue : null;
        public virtual decimal? SellingExpenseRatio => Revenue != null && Revenue != 0 ? SellingGeneralAdministrative / Revenue : null;

        [NotMapped]
        public bool WorkdayCustomer => (WorkdayCoreCustomer || WorkdayLandCustomer);
        [NotMapped]
        public bool WorkdayCoreCustomer => (WorkdayHCMCustomer || WorkdayFINCustomer);
        [NotMapped]
        public bool WorkdayLandCustomer => (WorkdayPLNCustomer || WorkdaySRCCustomer || WorkdayPKNCustomer);
        [NotMapped]
        public bool WorkdayPlatformCustomer => (WorkdayHCMCustomer && WorkdayFINCustomer);

        [NotMapped]
        public bool WorkdayHCMCustomer { get; set; }
        [NotMapped]
        public bool WorkdayFINCustomer { get; set; }
        [NotMapped]
        public bool WorkdayPLNCustomer { get; set; }
        [NotMapped]
        public bool WorkdaySRCCustomer { get; set; }
        [NotMapped]
        public bool WorkdayPKNCustomer { get; set; }
    }
}

