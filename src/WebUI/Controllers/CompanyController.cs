using System.Diagnostics;
using DataStructures.Benchmark;
using DataStructures.FundamentalData;
using InvestingMicroservicesAPIClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebUI.Models;

namespace WebUI.Controllers;

public class CompanyController : Controller
{
    private readonly ILogger<CompanyController> _logger;

    private APIGatewayClient _communicator;

    //public ExternalDataStructures.CompanyView? CompanyView { get; set; }

    //public ExternalDataStructures.IndustryView? IndustryBenchmark { get; set; }
    //public ExternalDataStructures.SectorView? SectorBenchmark { get; set; }
    //public ExternalDataStructures.MarketView? MarketBenchmark { get; set; }

    public CompanyController(ILogger<CompanyController> logger)
    {
        _logger = logger;

        _communicator = new APIGatewayClient("localhost", 7199, "api/");
    }

    public async Task<IActionResult> Index(string ticker)
    {
        var company = await _communicator.GetCompany(ticker);

        var industryBenchmark = await _communicator.GetIndustry(company.MarketName, company.SectorName, company.IndustryName);
        var sectorBenchmark = await _communicator.GetSector(company.MarketName, company.SectorName);
        var marketBenchmark = await _communicator.GetMarket(company.MarketName);

        //CompanyView = company;

        return View("CompanyView", new CompanyBenchmarkModel() { CompanyData = company.CompanyData, IndustryData = industryBenchmark.BenchmarkData, SectorData = sectorBenchmark.BenchmarkData, MarketData = marketBenchmark.BenchmarkData });
    }

    public async Task<IActionResult> DiscountingFactorCalculation(int profitability, int financialStability, int capitalManagement, int businessModel, int marketConditions, int management)
    {
        throw new NotImplementedException();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCompany(string ticker)
    {
        var response = await _communicator.StartUpdateCompanyJob(ticker);

        return Ok();
    }
}

