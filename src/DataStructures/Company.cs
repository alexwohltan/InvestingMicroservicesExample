using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace DataStructures
{
    public class Company : IIdentifier
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Industry")]
        public int IndustryID { get; set; }
        [JsonIgnore]
        public Industry Industry { get; set; }

        public string Name { get; set; }
        public string Ticker { get; set; }

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
