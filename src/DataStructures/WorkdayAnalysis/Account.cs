using System;
namespace DataStructures.WorkdayAnalysis
{
	public class Account
	{
        public virtual int Id { get; set; }

        public virtual string Ticker { get; set; }
        public virtual string Name { get; set; }
        public virtual int FundamentalDataId { get; set; }

        [NotMapped]
        public bool WorkdayCustomer => (WorkdayCoreCustomer || WorkdayLandCustomer);
        [NotMapped]
        public bool WorkdayCoreCustomer => (WorkdayHCMCustomer || WorkdayFINCustomer);
        [NotMapped]
        public bool WorkdayLandCustomer => (WorkdayPLNCustomer || WorkdaySRCCustomer || WorkdayPKNCustomer);
        [NotMapped]
        public bool WorkdayPlatformCustomer => (WorkdayHCMCustomer && WorkdayFINCustomer);

        [NotMapped]
        public bool WorkdayHCMCustomer => (WorkdayHCMGoLive != null);
        [NotMapped]
        public bool WorkdayFINCustomer => (WorkdayFINGoLive != null);

        [NotMapped]
        public bool WorkdayPLNCustomer => WorkdayPLNGoLive != null;
        [NotMapped]
        public bool WorkdaySRCCustomer => WorkdaySRCGoLive != null;
        [NotMapped]
        public bool WorkdayPKNCustomer => WorkdayPKNGoLive != null;

        public virtual DateTime? WorkdayHCMGoLive { get; set; }
        public virtual DateTime? WorkdayFINGoLive { get; set; }
        public virtual DateTime? WorkdayPLNGoLive { get; set; }
        public virtual DateTime? WorkdaySRCGoLive { get; set; }
        public virtual DateTime? WorkdayPKNGoLive { get; set; }

        public virtual IList<AccountSharePrice> Prices { get; set; }
        public virtual IList<AccountFiling> Filings { get; set; }

        public void UpdatePricesAndFilings()
        {
            UpdatePrices();
            UpdateFilings();
        }
        public void UpdatePrices()
        {
            foreach (var price in Prices)
            {
                price.WorkdayHCMCustomer = price.Date >= WorkdayHCMGoLive;
                price.WorkdayFINCustomer = price.Date >= WorkdayFINGoLive;
                price.WorkdayPLNCustomer = price.Date >= WorkdayPLNGoLive;
                price.WorkdaySRCCustomer = price.Date >= WorkdaySRCGoLive;
                price.WorkdayPKNCustomer = price.Date >= WorkdayPKNGoLive;
            }
        }
        public void UpdateFilings()
        {
            foreach (var filing in Filings)
            {
                filing.WorkdayHCMCustomer = filing.Date >= WorkdayHCMGoLive;
                filing.WorkdayFINCustomer = filing.Date >= WorkdayFINGoLive;
                filing.WorkdayPLNCustomer = filing.Date >= WorkdayPLNGoLive;
                filing.WorkdaySRCCustomer = filing.Date >= WorkdaySRCGoLive;
                filing.WorkdayPKNCustomer = filing.Date >= WorkdayPKNGoLive;
            }
        }
    }
}

