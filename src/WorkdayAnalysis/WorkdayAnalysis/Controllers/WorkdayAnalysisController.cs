using Microsoft.AspNetCore.Mvc;

namespace WorkdayAnalysis;

[ApiController]
[Route("Workday")]
public class WorkdayAnalysisController : ControllerBase
{
    private readonly ILogger<WorkdayAnalysisController> _logger;
    private readonly WorkdayAnalysisDbContext _repository;

    private readonly DataManagementHTTPClient.DataManagementClient _dataManagementClient;

    public WorkdayAnalysisController(ILogger<WorkdayAnalysisController> logger, WorkdayAnalysisDbContext repository)
    {
        _logger = logger;
        _repository = repository;

        _dataManagementClient = new DataManagementHTTPClient.DataManagementClient("localhost", 7299, "");
    }

    [HttpPost("AddAccount", Name = "ActionName")]
    public async Task<ActionResult> AddAccount(string ticker, DateTime? HCMGoLive = null, DateTime? FINGoLive = null, DateTime? PLNGoLive = null, DateTime? SRCGoLive = null, DateTime? PKNGoLive = null, string marketIdsString = "")
    {
        var account = new Account()
        {
            Ticker = ticker,
            WorkdayHCMGoLive = HCMGoLive,
            WorkdayFINGoLive = FINGoLive,
            WorkdayPLNGoLive = PLNGoLive,
            WorkdaySRCGoLive = SRCGoLive,
            WorkdayPKNGoLive = PKNGoLive
        };

        var marketIds = new List<int>();

        if(marketIdsString.Trim() != "" && marketIdsString.Trim() != "null")
        {
            var idStrings = marketIdsString.Split(',');

            foreach (var idString in idStrings)
            {
                if (int.TryParse(idString, out int marketId))
                    marketIds.Add(marketId);
            }
        }

        if (marketIds.Count() > 0)
            _repository.AddAccount(account, marketIds);
        else
            _repository.AddAccount(account);

        await _dataManagementClient.StartUpdateCompanyJob(ticker);

        return Ok();
    }
}


