using System;
namespace DataStructures.FundamentalData
{
	public class StockPrice : IIdentifier
	{
        [Key]
        public virtual int ID { get; set; }

        [ForeignKey("Company")]
        public virtual int CompanyID { get; set; }
        [JsonIgnore]
        public virtual Company Company { get; set; }

        public virtual DateTime Date { get; set; }
        public virtual decimal? Open { get; set; }
        public virtual decimal? High { get; set; }
        public virtual decimal? Low { get; set; }
        public virtual decimal? Close { get; set; }
        public virtual decimal? AdjClose { get; set; }
        public virtual decimal? Volume { get; set; }
        public virtual decimal? Dividend { get; set; }
    }
}

