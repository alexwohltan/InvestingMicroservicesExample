using System;
namespace ExternalDataStructures
{
	public class CompanyView
	{
		public string? MarketName { get; set; }
		public string? SectorName { get; set; }
		public string? IndustryName { get; set; }

		public DataStructures.FundamentalData.Company CompanyData { get; set; }
    }
}

