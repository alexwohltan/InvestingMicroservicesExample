using System;
using System.ComponentModel.DataAnnotations;

namespace DataStructures
{
    public abstract class FinancialStatement : IIdentifier
    {
        [Key]
        public virtual int ID { get; set; }

        public virtual string Ticker { get; set; }

        public virtual int FiscalYear => FilingDate.Year;

        public virtual DateTime FilingDate { get; set; }
        public virtual DateTime PeriodStartDate { get; set; }

        public virtual DateTime PublishedDate { get; set; }
        public virtual DateTime RestatedDate { get; set; }

        public virtual string Currency { get; set; }

        public override string ToString()
        {
            return FilingDate.ToShortDateString();
        }
    }
}
