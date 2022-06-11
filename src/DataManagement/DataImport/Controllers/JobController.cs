using System;
namespace DataImport.Controllers
{
	[ApiController]
	[Route("api/Jobs")]
	public class JobController : ControllerBase
	{
		private readonly IImporter Importer;

		public JobController(IImporter importer)
		{
			Importer = importer;
		}

  //      [HttpPut("companies/import/{ticker}", Name = "ImportCompany")]
		//public async Task<ActionResult> ImportCompany(string ticker)
  //      {
		//	Importer.ImportCompany(ticker);
  //      }
	}
}

