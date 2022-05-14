using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataStructures
{
    public class Sector
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Market")]
        public int MarketID { get; set; }
        public Market Market { get; set; }

        public string Name { get; set; }

        public List<Industry> Industries { get; set; }
    }
}
