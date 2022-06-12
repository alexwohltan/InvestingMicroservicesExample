using System.Diagnostics;
using InvestingMicroservicesAPIClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebUI.Models;

namespace WebUI.Controllers;

public class CompanyController : Controller
{
    private readonly ILogger<CompanyController> _logger;

    private APIGatewayClient _communicator;

    public CompanyController(ILogger<CompanyController> logger)
    {
        _logger = logger;

        _communicator = new APIGatewayClient("localhost", 7199, "api/");
    }

    public async Task<IActionResult> Index(string ticker)
    {
        var company = await _communicator.GetCompany(ticker);

        return View(company);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

