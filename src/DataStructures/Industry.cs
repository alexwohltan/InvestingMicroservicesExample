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
        public int ID { get; set; }

        [ForeignKey("Sector")]
        public int SectorID { get; set; }
        [JsonIgnore]
        public Sector Sector { get; set; }

        public string Name { get; set; }

        public List<Company> Companies { get; set; }
    }
}
