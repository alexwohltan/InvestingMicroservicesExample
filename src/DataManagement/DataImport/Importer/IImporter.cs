using System;
namespace DataImport
{
	public interface IImporter
	{
		public Task<IEnumerable<string>> GetAvialableTickers();

		public void ImportCompany(string ticker, string marketName, string sectorName, string industryName);
		public void UpdateCompany(string ticker);

		public void ImportStockPrice(string ticker);
		public void UpdateStockPrice(string ticker);

		public void ImportBulkCompanyFundamentalData();
	}

	public enum ImportFunction
    {
		ImportCompany,
		UpdateCompany,
		ImportStockPrice,
		UpdateStockPrice,
		ImportBulkCompanyFundamentalData
    }
}

