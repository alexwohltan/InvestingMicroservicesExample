using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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

