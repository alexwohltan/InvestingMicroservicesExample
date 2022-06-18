using System;
namespace DataImport.Controllers
{
	[ApiController]
	[Route("Jobs", Name = "Background Jobs")]
	public class JobController : ControllerBase
	{
		private readonly IImporter Importer;

		public JobController(IImporter importer)
		{
			Importer = importer;
		}

		[HttpPut("companies/import/bulk", Name = "ImportBulk")]
		public async Task<ActionResult> ImportBulk()
		{
			Importer.ImportBulkCompanyFundamentalData();

			return Ok();
		}

		[HttpPut("companies/import/{ticker}", Name = "ImportCompany")]
        public async Task<ActionResult> ImportCompany(string ticker, string marketName, string sectorName, string industryName)
        {
			Importer.ImportCompany(ticker, marketName, sectorName, industryName);

			return Ok();
        }

		[HttpPut("companies/update/{ticker}", Name = "UpdateCompany")]
		public async Task<ActionResult> UpdateCompany(string ticker)
		{
			Importer.UpdateCompany(ticker);

			return Ok();
		}
	}
}

