using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataStructures
{
    public class Market : IIdentifier
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public List<Sector> Sectors { get; set; }
    }
}
