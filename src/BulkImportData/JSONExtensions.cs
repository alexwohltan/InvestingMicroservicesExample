using System;
using DataStructures;

namespace BulkImportData
{
	public static class JSONExtensions
    {
        public static Market WithoutCompanies(this Market market)
        {
            var marketTrimmed = new Market { Name = market.Name };
            marketTrimmed.Sectors = new List<Sector>();
            foreach (var sec in market.Sectors)
            {
                marketTrimmed.Sectors.Add(sec.WithoutCompanies());
            }
            return marketTrimmed;
        }
        public static Sector WithoutCompanies(this Sector sector)
        {
            var sectorTrimmed = new Sector { Name = sector.Name };
            sectorTrimmed.Industries = new List<Industry>();
            foreach (var ind in sector.Industries)
            {
                sectorTrimmed.Industries.Add(ind.WithoutCompanies());
            }
            return sectorTrimmed;
        }
        public static Industry WithoutCompanies(this Industry industry)
        {
            var industyTrimmed = new Industry { Name = industry.Name };
            industyTrimmed.Companies = new List<Company>();
            return industyTrimmed;
        }
    }
}

