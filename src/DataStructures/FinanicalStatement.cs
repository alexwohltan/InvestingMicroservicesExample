using System;
using System.ComponentModel.DataAnnotations;

namespace DataStructures
{
    public abstract class FinancialStatement
    {
        [Key]
        public int ID { get; set; }

        public string Ticker { get; set; }

        public int FiscalYear => FilingDate.Year;

        public virtual DateTime FilingDate { get; set; }
        public virtual DateTime PeriodStartDate { get; set; }

        public virtual DateTime PublishedDate { get; set; }
        public virtual DateTime RestatedDate { get; set; }

        public string Currency { get; set; }

        public override string ToString()
        {
            return FilingDate.ToShortDateString();
        }
    }
}
