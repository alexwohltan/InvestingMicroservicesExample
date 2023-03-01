using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Web;
using ExternalDataStructures;

namespace InvestingMicroservicesAPIClient;
public class APIGatewayClient
{
    public HttpClient Client { get; set; }

    public string Hostname { get; set; }
    public int Port { get; set; }
    public string ApiUrl { get; set; }

    public APIGatewayClient(string hostname, int port, string baseUrl)
    {
        Client = new HttpClient();
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        Hostname = hostname;
        Port = port;
        ApiUrl = baseUrl;
    }

    #region Market
    public async Task<List<MarketView>?> GetMarkets()
    {
        List<MarketView>? markets = null;

        var result = await SendHttpRequest(HttpMethod.Get, "Markets");

        if (result.IsSuccessStatusCode)
            markets = await result.Content.ReadFromJsonAsync<List<MarketView>>();

        return markets;
    }
    public async Task<MarketView?> GetMarket(string marketName)
    {
        MarketView? market = null;

        var result = await SendHttpRequest(HttpMethod.Get, String.Format("Markets/{0}", marketName));

        if (result.IsSuccessStatusCode)
            market = await result.Content.ReadFromJsonAsync<MarketView>();

        return market;
    }
    #endregion

    #region Sector
    public async Task<SectorView?> GetSector(string marketName, string sectorName)
    {
        SectorView? sector = null;

        var result = await SendHttpRequest(HttpMethod.Get, String.Format("Markets/{0}/{1}", marketName, sectorName));

        if (result.IsSuccessStatusCode)
            sector = await result.Content.ReadFromJsonAsync<SectorView>();

        return sector;
    }
    #endregion

    #region Industry
    public async Task<IndustryView?> GetIndustry(string marketName, string sectorName, string industryName)
    {
        IndustryView? industry = null;

        var result = await SendHttpRequest(HttpMethod.Get, String.Format("Markets/{0}/{1}/{2}", marketName, sectorName, industryName));

        if (result.IsSuccessStatusCode)
            industry = await result.Content.ReadFromJsonAsync<IndustryView>();

        return industry;
    }
    #endregion

    #region Company

    public async Task<CompanyView?> GetCompany(string ticker)
    {
        CompanyView? company = null;

        var result = await SendHttpRequest(HttpMethod.Get, String.Format("Companies/{0}", ticker));

        if (result.IsSuccessStatusCode)
            company = await result.Content.ReadFromJsonAsync<CompanyView>();

        return company;
    }
    //// Depreciated
    //public async Task<CompanyView?> GetCompanyWithBenchmarks(string ticker)
    //{
    //    CompanyView? company = null;

    //    var result = await SendHttpRequest(HttpMethod.Get, String.Format("Companies/{0}/full", ticker));

    //    if (result.IsSuccessStatusCode)
    //        company = await result.Content.ReadFromJsonAsync<CompanyView>();

    //    return company;
    //}

    #endregion

    private async Task<HttpResponseMessage> SendHttpRequest(HttpMethod method, string location, Dictionary<string, string> parameters = null, object bodyContent = null)
    {
        var builder = new UriBuilder(Hostname);
        builder.Scheme = "https";
        builder.Port = Port;
        builder.Path = ApiUrl + location;

        if (parameters != null)
            builder.Query = QueryParametersEncoded(parameters);

        var uri = builder.Uri;

        var request = new HttpRequestMessage(method, uri);

        if (bodyContent != null)
        {
            var json = JsonSerializer.Serialize(bodyContent);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        return await Client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
    }
    private string QueryParametersEncoded(Dictionary<string, string> parameters)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        foreach (var parameter in parameters)
        {
            query[parameter.Key] = parameter.Value;
        }
        return query.ToString();
    }
}

