using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataStructures
{
    public class Industry : IIdentifier
    {
        [Key]
        public virtual int ID { get; set; }

        [ForeignKey("Sector")]
        public virtual int SectorID { get; set; }
        [JsonIgnore]
        public virtual Sector Sector { get; set; }

        public virtual string Name { get; set; }

        public virtual List<Company> Companies { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
