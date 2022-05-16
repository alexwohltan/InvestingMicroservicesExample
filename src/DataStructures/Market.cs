using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataStructures
{
    public class Market : IIdentifier
    {
        [Key]
        public virtual int ID { get; set; }

        public virtual string Name { get; set; }

        public virtual List<Sector> Sectors { get; set; }

        public override string ToString()
        {
            return Name + " Market";
        }
    }
}
