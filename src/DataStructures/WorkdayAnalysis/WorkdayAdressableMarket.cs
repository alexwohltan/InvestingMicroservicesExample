using System;
namespace DataStructures.WorkdayAnalysis
{
	public class WorkdayAdressableMarket
	{
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        [NotMapped]
        public virtual IEnumerable<Account> WorkdayCustomers => Accounts.Where(e => e.WorkdayCustomer);
        [NotMapped]
        public virtual IEnumerable<Account> WorkdayProspects => Accounts.Where(e => !e.WorkdayCustomer);

        [NotMapped]
        public virtual IEnumerable<Account> WorkdayCoreCustomers => Accounts.Where(e => e.WorkdayCoreCustomer);
        [NotMapped]
        public virtual IEnumerable<Account> WorkdayLandCustomers => Accounts.Where(e => e.WorkdayLandCustomer);
        [NotMapped]
        public virtual IEnumerable<Account> WorkdayPlatformCustomers => Accounts.Where(e => e.WorkdayPlatformCustomer);
        [NotMapped]
        public virtual IEnumerable<Account> WorkdayPlatformProspects => Accounts.Where(e => !e.WorkdayPlatformCustomer);
        [NotMapped]
        public virtual IEnumerable<Account> WorkdayCoreProspects => Accounts.Where(e => !e.WorkdayCoreCustomer);
        [NotMapped]
        public virtual IEnumerable<Account> WorkdayLandProspects => Accounts.Where(e => !e.WorkdayLandCustomer);

        [NotMapped]
        public virtual IEnumerable<Account> WorkdayHCMCustomers => Accounts.Where(e => e.WorkdayHCMCustomer);
        [NotMapped]
        public virtual IEnumerable<Account> WorkdayHCMProspects => Accounts.Where(e => !e.WorkdayHCMCustomer);
        [NotMapped]
        public virtual IEnumerable<Account> WorkdayFINCustomers => Accounts.Where(e => e.WorkdayFINCustomer);
        [NotMapped]
        public virtual IEnumerable<Account> WorkdayFINProspects => Accounts.Where(e => !e.WorkdayFINCustomer);

        [NotMapped]
        public virtual IList<Account> Accounts { get; set; }

        public virtual IList<int> AccountIds { get; set; }
    }
}

