using DataManagementHTTPClient;

namespace APIGateway.Controllers;

[ApiController]
[Route("api/Data")]
public class DataManagementController : Controller
{
    private readonly DataManagementClient _DataManagementClient;

    // GET: /<controller>/
    public DataManagementController(DataManagementClient dataManagementClient)
    {
        _DataManagementClient = dataManagementClient;
    }

    [HttpGet("import/{market}/{sector}/{industry}/{ticker}")]
    public async Task<string> StartCompanyImportJob(string market, string sector, string industry, string ticker)
    {
        var result = await _DataManagementClient.StartImportCompanyJob(ticker, market, sector, industry);

        return result;
    }

    [HttpGet("update/{ticker}")]
    public async Task<string> StartCompanyUpdateJob(string ticker)
    {
        var result = await _DataManagementClient.StartUpdateCompanyJob(ticker);

        return result;
    }
}


