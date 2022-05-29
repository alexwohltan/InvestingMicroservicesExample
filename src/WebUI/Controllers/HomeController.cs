using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebUI.Models;

namespace WebUI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private APICommunication _communicator;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;

        _communicator = new APICommunication();
    }

    public async Task<IActionResult> Index()
    {
        var markets = await _communicator.GetMarketsForIndexView();

        return View(markets);
    }

    public async Task<IActionResult> Market(string marketName)
    {
        var market = await _communicator.GetMarketForMarketView(marketName);

        return View(market);
    }

    public async Task<IActionResult> Sector(string marketName, string sectorName)
    {
        var sector = await _communicator.GetSectorForSectorView(marketName, sectorName);

        return View(sector);
    }

    public async Task<IActionResult> Industry(string marketName, string sectorName, string industryName)
    {
        var industry = await _communicator.GetIndustryForIndustryView(marketName, sectorName, industryName);

        return View(industry);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

