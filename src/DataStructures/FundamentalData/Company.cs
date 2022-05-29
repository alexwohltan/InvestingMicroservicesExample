using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace DataStructures.FundamentalData
{
    public class Company : IIdentifier
    {
        [Key]
        public virtual int ID { get; set; }

        [ForeignKey("Industry")]
        public virtual int IndustryID { get; set; }
        [JsonIgnore]
        public virtual Industry Industry { get; set; }

        public virtual string Name { get; set; }
        public virtual string Ticker { get; set; }

        public virtual List<Filing> Filings { get; set; }

        public Company()
        {

        }

        public override string ToString()
        {
            return Name;
        }
    }
}
