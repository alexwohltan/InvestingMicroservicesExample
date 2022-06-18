using System;
namespace DataImport.Controllers
{
	[ApiController]
	[Route("SimFin")]
	public class SimFinController : ControllerBase
	{
		private readonly IImporter _importer;

        public SimFinClient Client { get; set; }

        public SimFinController(IImporter importer)
		{
			_importer = importer;

			Client = new SimFinClient("simfin.com", 443, "api/v2/", "e3Vs37gGTuE15kRKL4QJketpT7lOkLZl");
		}

        [HttpGet("companies/list")]
		public async Task<IEnumerable<string>> GetCompaniesListInSimFinDatabase()
        {
			return await _importer.GetAvialableTickers();
        }

		[HttpGet("companies/general")]
		public async Task<SimFinGeneralCompanyInformationResponse> GetCompaniesListInSimFinDatabase(string ticker)
		{
			return await Client.GetGeneralCompanyInformation(new List<string> { ticker });
		}

		[HttpGet("companies/statements")]
		public async Task<IEnumerable<SimFinFundamentalsCompany>> GetFundamentals(string ticker, string statement, string period, int fyear)
        {
			return await Client.GetCompanyFundamentals(new List<string> { ticker }, statement, new List<string> { period }, new List<int> { fyear });
        }
		[HttpGet("companies/statements/2")]
		public async Task<IEnumerable<SimFinFundamentalsCompany>> GetFundamentals2(string ticker, string period)
		{
			return await Client.GetCompanyFundamentalsBasicLicense(new List<string> { ticker, "AAPL" }, period);
		}

		[HttpGet("companies/prices")]
		public async Task<IEnumerable<SimFinSharePriceCollection>> GetPrices(string ticker, bool ratios = false, bool asreported = false, DateTime? startDate = null, DateTime? endDate = null)
		{
			return await Client.GetSharePriceData(new List<string> { ticker }, ratios, asreported, startDate, endDate);
		}

		[HttpGet("companies")]
		public async Task<Company> GetCompany(string ticker)
		{
			return await Client.GetCompany(ticker);
		}
	}
}

