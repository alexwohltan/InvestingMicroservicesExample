using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DataImport;

[ApiController]
[Route("api/Bulk")]
public class BulkController : ControllerBase
{
    private readonly ILogger<BulkController> _logger;
    private readonly IEventBus _eventBus;

    public BulkController(ILogger<BulkController> logger, IEventBus eventBus)
    {
        _logger = logger;
        _eventBus = eventBus;
    }

    [HttpGet(Name = "SimFinBulkImport")]
    public async Task<ActionResult> AddSimFinBulk()
    {
        SimFinBulkClient handler = new SimFinBulkClient();
        var markets = handler.RetrieveMarkets();

        FundamentalDataHTTPClient.FundamentalDataClient client = new FundamentalDataHTTPClient.FundamentalDataClient("localhost", 60958, "api/");

        var marketsInDb = await client.GetMarkets();

        foreach (var market in markets)
        {
            //var marketContent = System.Text.Json.JsonSerializer.Serialize(market.WithoutCompanies());
            //var marketBuffer = System.Text.Encoding.UTF8.GetBytes(marketContent);
            //var marketByteContent = new ByteArrayContent(marketBuffer);
            //marketByteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            await client.AddMarket(market.WithoutSectors());

            var marketInDatabase = await client.GetMarket(market.Name);
            var marketId = marketInDatabase.ID;

            //var marketId = System.Text.Json.JsonSerializer.Deserialize<Market>((await client.GetAsync("https://localhost:60958/api/Markets/names/name=" + market.Name)).Content.ToString()).ID;

            foreach (var sector in market.Sectors)
            {
                await client.AddSector(sector.WithoutIndustries(), marketId);

                var sectorInDatabase = await client.GetSector(sector.Name, marketId);
                var sectorId = sectorInDatabase.ID;

                foreach (var industry in sector.Industries)
                {
                    await client.AddIndustry(industry.WithoutCompanies(), sectorId);

                    var industryInDatabase = await client.GetIndustry(industry.Name, sectorId);
                    var industryId = industryInDatabase.ID;

                    foreach (var company in industry.Companies)
                    {
                        await client.AddCompany(company, industryId);

                        var companyInDatabase = await client.GetCompany(company.Ticker);
                        var companyId = companyInDatabase.ID;

                        Debug.WriteLine(String.Format("Added company {0}. ID = {1}.", company.Name, companyId));

                        //Thread.Sleep(5000);

                        //_eventBus.Publish(new NewCompanyEvent { NewCompany = company, MarketName = market.Name, SectorName = sector.Name, IndustryName = industry.Name });

                        //var companyContent = System.Text.Json.JsonSerializer.Serialize(company);
                        //var companyBuffer = System.Text.Encoding.UTF8.GetBytes(companyContent);
                        //var companyByteContent = new ByteArrayContent(companyBuffer);
                        //companyByteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                        //string url = String.Format("https://localhost:60958/api/Companies/names?marketName={0}&sectorName={1}&industryName={2}", market.Name, sector.Name, industry.Name);

                        //await client.PostAsync(url, companyByteContent);
                    }
                }
            }

        }

        return Ok();
    }
}

