using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DataStructures
{
    public class Company
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Industry")]
        public int IndustryID { get; set; }
        public Industry Industry { get; set; }

        public string Name { get; set; }
        public string Ticker { get; set; }

        public virtual List<Filing> Filings { get; set; }

        public Company()
        {

        }
    }
}
