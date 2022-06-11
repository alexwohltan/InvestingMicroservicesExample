
namespace APIGateway.Controllers;

[ApiController]
[Route("api/Markets")]
public class MarketController : Controller
{
    private readonly FundamentalDataClient _FundamentalDataClient;
    private readonly BenchmarkClient _BenchmarkClient;

    public MarketController(FundamentalDataClient fundamentalDataClient, BenchmarkClient benchmarkClient)
    {
        _FundamentalDataClient = fundamentalDataClient;
        _BenchmarkClient = benchmarkClient;
    }

    [HttpGet]
    public async Task<IEnumerable<ExternalDataStructures.MarketView>> GetMarkets()
    {
        var marketsData = await _FundamentalDataClient.GetMarkets();

        if (marketsData == null)
            return null;

        return marketsData.Select(e => new ExternalDataStructures.MarketView() { MarketData = e });
    }

    [HttpGet("{marketName}")]
    public async Task<ExternalDataStructures.MarketView> GetMarket(string marketName)
    {
        var marketData = await _FundamentalDataClient.GetMarket(marketName);

        if (marketData == null)
            return null;

        var benchmarkData = await _BenchmarkClient.GetMarketBenchmark(marketName);

        var result = new ExternalDataStructures.MarketView()
        {
            MarketData = marketData,
            BenchmarkData = benchmarkData
        };

        return result;
    }

    [HttpGet("{marketName}/{sectorName}")]
    public async Task<ExternalDataStructures.SectorView> GetSector(string marketName, string sectorName)
    {
        var sectorData = await _FundamentalDataClient.GetSector(marketName, sectorName);

        if (sectorData == null)
            return null;

        var benchmarkData = await _BenchmarkClient.GetSectorBenchmark(marketName, sectorName);

        var result = new ExternalDataStructures.SectorView()
        {
            SectorData = sectorData,
            BenchmarkData = benchmarkData
        };

        return result;
    }

    [HttpGet("{marketName}/{sectorName}/{industryName}")]
    public async Task<ExternalDataStructures.IndustryView> GetSector(string marketName, string sectorName, string industryName)
    {
        var industryData = await _FundamentalDataClient.GetIndustry(marketName, sectorName, industryName);

        if (industryData == null)
            return null;

        var benchmarkData = await _BenchmarkClient.GetIndustryBenchmark(marketName, sectorName, industryName);

        var result = new ExternalDataStructures.IndustryView()
        {
            IndustryData = industryData,
            BenchmarkData = benchmarkData
        };

        return result;
    }
}

