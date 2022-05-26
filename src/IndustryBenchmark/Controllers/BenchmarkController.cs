using Microsoft.AspNetCore.Mvc;

namespace Benchmark;

[ApiController]
[Route("api/Benchmark")]
public class BenchmarkController : ControllerBase
{
    private readonly IBenchmarkRepository _repository;

    public BenchmarkController(IBenchmarkRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("markets", Name = "GetMarketBenchmark")]
    public MarketBenchmark GetMarketBenchmark(string marketName)
    {
        return _repository.MarketBenchmarks.FirstOrDefault(e => e.MarketName == marketName);
    }

    [HttpGet("sectors", Name = "GetSectorBenchmark")]
    public SectorBenchmark GetSectorBenchmark(string marketName, string sectorName = "")
    {
        return _repository.SectorBenchmarks.FirstOrDefault(e => e.MarketName == marketName && e.SectorName == sectorName);
    }

    [HttpGet("industries", Name = "GetIndustryBenchmark")]
    public IndustryBenchmark GetIndustryBenchmark(string marketName, string sectorName = "", string industryName = "")
    {
        return _repository.IndustryBenchmarks.FirstOrDefault(e => e.MarketName == marketName && e.SectorName == sectorName && e.IndustryName == industryName);
    }
}

