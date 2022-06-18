using System;
namespace DataStructures.WorkdayAnalysis
{
	public class AccountSharePrice
	{
        public virtual int Id { get; set; }
        public virtual DateTime Date { get; set; }

        public virtual decimal? CloseAdj { get; set; }

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

