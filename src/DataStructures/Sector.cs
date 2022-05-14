using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataStructures
{
    public class Sector : IIdentifier
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Market")]
        public int MarketID { get; set; }
        [JsonIgnore]
        public Market Market { get; set; }

        public string Name { get; set; }

        public List<Industry> Industries { get; set; }
    }
}
