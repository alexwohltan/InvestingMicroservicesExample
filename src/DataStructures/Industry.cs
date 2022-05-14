using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataStructures
{
    public class Industry
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Sector")]
        public int SectorID { get; set; }
        public Sector Sector { get; set; }

        public string Name { get; set; }

        public List<Company> Companies { get; set; }
    }
}
