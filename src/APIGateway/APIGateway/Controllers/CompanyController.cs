
namespace APIGateway.Controllers;

[ApiController]
[Route("api/Companies")]
public class CompanyController : Controller
{
    private readonly FundamentalDataClient _FundamentalDataClient;

    public CompanyController(FundamentalDataClient fundamentalDataClient)
    {
        _FundamentalDataClient = fundamentalDataClient;
    }

    [HttpGet("{ticker}")]
    public async Task<ExternalDataStructures.CompanyView> GetCompany(string ticker)
    {
        var companyData = await _FundamentalDataClient.GetCompany(ticker);

        if (companyData == null)
            return null;

        var result = new ExternalDataStructures.CompanyView()
        {
            CompanyData = companyData
        };

        return result;
    }
}

