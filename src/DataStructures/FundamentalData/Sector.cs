using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataStructures.FundamentalData
{
    public class Sector : IIdentifier
    {
        [Key]
        public virtual int ID { get; set; }

        [ForeignKey("Market")]
        public virtual int MarketID { get; set; }
        [JsonIgnore]
        public virtual Market Market { get; set; }

        public virtual string Name { get; set; }

        public virtual List<Industry> Industries { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
