
using BenchmarkHTTPClient;

namespace APIGateway.Controllers;

[ApiController]
[Route("api/Companies")]
public class CompanyController : Controller
{
    private readonly FundamentalDataClient _FundamentalDataClient;
    private readonly BenchmarkClient _BenchmarkClient;

    public CompanyController(FundamentalDataClient fundamentalDataClient, BenchmarkClient benchmarkClient)
    {
        _FundamentalDataClient = fundamentalDataClient;
        _BenchmarkClient = benchmarkClient;
    }

    [HttpGet("{ticker}")]
    public async Task<ExternalDataStructures.CompanyView> GetCompany(string ticker)
    {
        var companyData = await _FundamentalDataClient.GetCompany(ticker);
        if (companyData == null)
            return null;

        var names = await _FundamentalDataClient.ResolveIndustrySectorMarketName(companyData.IndustryID);
        if (names == null)
            throw new Exception("Server did not provide Names for Industry, Sector & Market. Requested Industry ID was " + companyData.Industry + "!");

        var result = new ExternalDataStructures.CompanyView()
        {
            CompanyData = companyData,
            IndustryName = names[0],
            SectorName = names[1],
            MarketName = names[2],
        };

        return result;
    }
}

