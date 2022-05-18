using System;
using System.Collections.Generic;

namespace DataStructures
{
    public static class Extensions
    {
        public static Market WithoutSectors(this Market market)
        {
            var marketTrimmed = new Market { Name = market.Name, ID = market.ID };
            marketTrimmed.Sectors = new List<Sector>();
            return marketTrimmed;
        }

        public static Market WithoutIndustries(this Market market)
        {
            var marketTrimmed = new Market { Name = market.Name, ID = market.ID };
            marketTrimmed.Sectors = new List<Sector>();
            foreach (var sec in market.Sectors)
            {
                marketTrimmed.Sectors.Add(sec.WithoutIndustries());
            }
            return marketTrimmed;
        }
        public static Sector WithoutIndustries(this Sector sector)
        {
            var sectorTrimmed = new Sector { Name = sector.Name, ID = sector.ID };
            sectorTrimmed.Industries = new List<Industry>();
            return sectorTrimmed;
        }

        public static Market WithoutCompanies(this Market market)
        {
            var marketTrimmed = new Market { Name = market.Name, ID = market.ID };
            marketTrimmed.Sectors = new List<Sector>();
            foreach (var sec in market.Sectors)
            {
                marketTrimmed.Sectors.Add(sec.WithoutCompanies());
            }
            return marketTrimmed;
        }
        public static Sector WithoutCompanies(this Sector sector)
        {
            var sectorTrimmed = new Sector { Name = sector.Name, ID = sector.ID };
            sectorTrimmed.Industries = new List<Industry>();
            foreach (var ind in sector.Industries)
            {
                sectorTrimmed.Industries.Add(ind.WithoutCompanies());
            }
            return sectorTrimmed;
        }
        public static Industry WithoutCompanies(this Industry industry)
        {
            var industyTrimmed = new Industry { Name = industry.Name, ID = industry.ID };
            industyTrimmed.Companies = new List<Company>();
            return industyTrimmed;
        }

        public static Market WithoutFilings(this Market market)
        {
            var marketTrimmed = new Market { Name = market.Name, ID = market.ID };
            marketTrimmed.Sectors = new List<Sector>();
            foreach (var sec in market.Sectors)
            {
                marketTrimmed.Sectors.Add(sec.WithoutFilings());
            }
            return marketTrimmed;
        }
        public static Sector WithoutFilings(this Sector sector)
        {
            var sectorTrimmed = new Sector { Name = sector.Name, ID = sector.ID };
            sectorTrimmed.Industries = new List<Industry>();
            foreach (var ind in sector.Industries)
            {
                sectorTrimmed.Industries.Add(ind.WithoutFilings());
            }
            return sectorTrimmed;
        }
        public static Industry WithoutFilings(this Industry industry)
        {
            var industyTrimmed = new Industry { Name = industry.Name, ID = industry.ID };
            industyTrimmed.Companies = new List<Company>();
            foreach (var comp in industry.Companies)
            {
                industyTrimmed.Companies.Add(comp.WithoutFilings());
            }
            return industyTrimmed;
        }
        public static Company WithoutFilings(this Company company)
        {
            var companyTrimmed = new Company { Name = company.Name, Ticker = company.Ticker, ID = company.ID };
            company.Filings = new List<Filing>();
            return companyTrimmed;
        }
    }
}

