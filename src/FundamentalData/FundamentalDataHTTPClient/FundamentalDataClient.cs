using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Web;
using DataStructures;

namespace FundamentalDataHTTPClient;
public class FundamentalDataClient
{
    public HttpClient Client { get; set; }

    public string Hostname { get; set; }
    public int Port { get; set; }
    public string ApiUrl { get; set; }

    public FundamentalDataClient(string hostname, int port, string baseUrl)
    {
        Client = new HttpClient();
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        Hostname = hostname;
        Port = port;
        ApiUrl = baseUrl;
    }

    #region Market
    public async Task<string> AddMarket(Market newMarket)
    {
        var result = await SendHttpRequest(HttpMethod.Post, "Markets", bodyContent: newMarket);
        if (result.IsSuccessStatusCode)
            return result.Headers.Location.ToString();
        else
            return result.ReasonPhrase;
    }

    public async Task<List<Market>?> GetMarkets()
    {
        List<Market>? markets = null;

        var result = await SendHttpRequest(HttpMethod.Get, "Markets");

        if (result.IsSuccessStatusCode)
            markets = await result.Content.ReadFromJsonAsync<List<Market>>();

        return markets;
    }
    public async Task<Market?> GetMarket(int id)
    {
        Market? market = null;

        var requestParameters = new Dictionary<string, string> {
            { "id", id.ToString() }
        };

        var result = await SendHttpRequest(HttpMethod.Get, "Markets/ids", requestParameters);

        if (result.IsSuccessStatusCode)
            market = await result.Content.ReadFromJsonAsync<Market>();

        return market;
    }
    public async Task<Market?> GetMarket(string name)
    {
        Market? market = null;

        var requestParameters = new Dictionary<string, string> {
            { "name", name }
        };

        var result = await SendHttpRequest(HttpMethod.Get, "Markets/names", requestParameters);

        if (result.IsSuccessStatusCode)
            market = await result.Content.ReadFromJsonAsync<Market>();

        return market;
    }

    public async Task<string> UpdateMarket(int marketId, Market newMarket)
    {
        var requestParameters = new Dictionary<string, string> {
            { "id", marketId.ToString() }
        };

        var result = await SendHttpRequest(HttpMethod.Put, "Markets", requestParameters, newMarket);
        if (result.IsSuccessStatusCode)
            return result.Headers.Location.ToString();
        else
            return result.ReasonPhrase;
    }


    public async Task DeleteMarket(int marketId)
    {
        var requestParameters = new Dictionary<string, string> {
            { "id", marketId.ToString() }
        };

        var result = await SendHttpRequest(HttpMethod.Delete, "Markets", requestParameters);

        if (!result.IsSuccessStatusCode)
            throw new Exception(String.Format("Error while deleting market with id {0}.", marketId));
    }
    #endregion

    #region Sector
    public async Task<string> AddSector(Sector newSector, int marketId)
    {
        var requestParameters = new Dictionary<string, string> {
            { "marketId", marketId.ToString() }
        };

        var result = await SendHttpRequest(HttpMethod.Post, "Sectors", parameters: requestParameters, bodyContent: newSector);
        if (result.IsSuccessStatusCode)
            return result.Headers.Location.ToString();
        else
            return result.ReasonPhrase;
    }
    public async Task<string> AddSector(Sector newSector, string marketName)
    {
        var requestParameters = new Dictionary<string, string> {
            { "marketName", marketName }
        };

        var result = await SendHttpRequest(HttpMethod.Post, "Sectors/names", parameters: requestParameters, bodyContent: newSector);
        if (result.IsSuccessStatusCode)
            return result.Headers.Location.ToString();
        else
            return result.ReasonPhrase;
    }

    public async Task<List<Sector>?> GetSectors(int marketId)
    {
        List<Sector>? sectors = null;

        var result = await SendHttpRequest(HttpMethod.Get, "Markets/" + marketId + "/Sectors");

        if (result.IsSuccessStatusCode)
            sectors = await result.Content.ReadFromJsonAsync<List<Sector>>();

        return sectors;
    }
    public async Task<Sector?> GetSector(int id)
    {
        Sector? sector = null;

        var requestParameters = new Dictionary<string, string> {
            { "id", id.ToString() }
        };

        var result = await SendHttpRequest(HttpMethod.Get, "Sectors/ids", requestParameters);

        if (result.IsSuccessStatusCode)
            sector = await result.Content.ReadFromJsonAsync<Sector>();

        return sector;
    }
    public async Task<Sector?> GetSector(string sectorName, int marketId)
    {
        Sector? sector = null;

        var requestParameters = new Dictionary<string, string> {
            { "name", sectorName },
            { "marketId", marketId.ToString() }
        };

        var result = await SendHttpRequest(HttpMethod.Get, "Sectors/names", requestParameters);

        if (result.IsSuccessStatusCode)
            sector = await result.Content.ReadFromJsonAsync<Sector>();

        return sector;
    }

    public async Task<string> UpdateSector(int sectorId, Sector newSector)
    {
        var requestParameters = new Dictionary<string, string> {
            { "id", sectorId.ToString() }
        };

        var result = await SendHttpRequest(HttpMethod.Put, "Sectors", requestParameters, newSector);
        if (result.IsSuccessStatusCode)
            return result.Headers.Location.ToString();
        else
            return result.ReasonPhrase;
    }


    public async Task DeleteSector(int sectorId)
    {
        var requestParameters = new Dictionary<string, string> {
            { "id", sectorId.ToString() }
        };

        var result = await SendHttpRequest(HttpMethod.Delete, "Sectors", requestParameters);

        if (!result.IsSuccessStatusCode)
            throw new Exception(String.Format("Error while deleting sector with id {0}.", sectorId));
    }
    #endregion

    #region Industry
    public async Task<string> AddIndustry(Industry newIndustry, int sectorId)
    {
        var requestParameters = new Dictionary<string, string> {
            { "sectorId", sectorId.ToString() }
        };

        var result = await SendHttpRequest(HttpMethod.Post, "Industries", parameters: requestParameters, bodyContent: newIndustry);
        if (result.IsSuccessStatusCode)
            return result.Headers.Location.ToString();
        else
            return result.ReasonPhrase;
    }
    public async Task<string> AddIndustry(Industry newIndustry, string marketName, string sectorName)
    {
        var requestParameters = new Dictionary<string, string> {
            { "marketName", marketName },
            { "sectorName", sectorName }
        };

        var result = await SendHttpRequest(HttpMethod.Post, "Industries/names", parameters: requestParameters, bodyContent: newIndustry);
        if (result.IsSuccessStatusCode)
            return result.Headers.Location.ToString();
        else
            return result.ReasonPhrase;
    }

    public async Task<List<Industry>?> GetIndustries(int sectorId)
    {
        List<Industry>? industries = null;

        var result = await SendHttpRequest(HttpMethod.Get, "Sectors/" + sectorId + "/Industries");

        if (result.IsSuccessStatusCode)
            industries = await result.Content.ReadFromJsonAsync<List<Industry>>();

        return industries;
    }
    public async Task<Industry?> GetIndustry(int id)
    {
        Industry? industry = null;

        var requestParameters = new Dictionary<string, string> {
            { "id", id.ToString() }
        };

        var result = await SendHttpRequest(HttpMethod.Get, "Industries/ids", requestParameters);

        if (result.IsSuccessStatusCode)
            industry = await result.Content.ReadFromJsonAsync<Industry>();

        return industry;
    }
    public async Task<Industry?> GetIndustry(string name, int sectorId)
    {
        Industry? industry = null;

        var requestParameters = new Dictionary<string, string> {
            { "name", name },
            { "sectorId", sectorId.ToString() }
        };

        var result = await SendHttpRequest(HttpMethod.Get, "Industries/names", requestParameters);

        if (result.IsSuccessStatusCode)
            industry = await result.Content.ReadFromJsonAsync<Industry>();

        return industry;
    }

    public async Task<string> UpdateIndustry(int industryId, Industry newIndustry)
    {
        var requestParameters = new Dictionary<string, string> {
            { "id", industryId.ToString() }
        };

        var result = await SendHttpRequest(HttpMethod.Put, "Industries", requestParameters, newIndustry);
        if (result.IsSuccessStatusCode)
            return result.Headers.Location.ToString();
        else
            return result.ReasonPhrase;
    }


    public async Task DeleteIndustry(int industryId)
    {
        var requestParameters = new Dictionary<string, string> {
            { "id", industryId.ToString() }
        };

        var result = await SendHttpRequest(HttpMethod.Delete, "Industries", requestParameters);

        if (!result.IsSuccessStatusCode)
            throw new Exception(String.Format("Error while deleting industry with id {0}.", industryId));
    }
    #endregion

    #region Company
    public async Task<string> AddCompany(Company newCompany, int industryId)
    {
        var requestParameters = new Dictionary<string, string> {
            { "industryId", industryId.ToString() }
        };

        var result = await SendHttpRequest(HttpMethod.Post, "Companies", parameters: requestParameters, bodyContent: newCompany);
        if (result.IsSuccessStatusCode)
            return result.Headers.Location.ToString();
        else
            return result.ReasonPhrase;
    }
    public async Task<string> AddCompany(Company newCompany, string marketName, string sectorName, string industryName)
    {
        var requestParameters = new Dictionary<string, string> {
            { "marketName", marketName },
            { "sectorName", sectorName },
            { "industryName", industryName }
        };

        var result = await SendHttpRequest(HttpMethod.Post, "Companies/names", parameters: requestParameters, bodyContent: newCompany);
        if (result.IsSuccessStatusCode)
            return result.Headers.Location.ToString();
        else
            return result.ReasonPhrase;
    }

    public async Task<List<Company>?> GetCompanies(int industryId)
    {
        List<Company>? companies = null;

        var result = await SendHttpRequest(HttpMethod.Get, "Industries/" + industryId + "/Companies");

        if (result.IsSuccessStatusCode)
            companies = await result.Content.ReadFromJsonAsync<List<Company>>();

        return companies;
    }
    public async Task<Company?> GetCompany(int id)
    {
        Company? company = null;

        var requestParameters = new Dictionary<string, string> {
            { "id", id.ToString() }
        };

        var result = await SendHttpRequest(HttpMethod.Get, "Companies/ids", requestParameters);

        if (result.IsSuccessStatusCode)
            company = await result.Content.ReadFromJsonAsync<Company>();

        return company;
    }
    public async Task<Company?> GetCompany(string ticker)
    {
        Company? company = null;

        var requestParameters = new Dictionary<string, string> {
            { "ticker", ticker }
        };

        var result = await SendHttpRequest(HttpMethod.Get, "Companies/tickers", requestParameters);

        if (result.IsSuccessStatusCode)
            company = await result.Content.ReadFromJsonAsync<Company>();

        return company;
    }

    public async Task<string> UpdateCompany(int companyId, Company newCompany)
    {
        var requestParameters = new Dictionary<string, string> {
            { "id", companyId.ToString() }
        };

        var result = await SendHttpRequest(HttpMethod.Put, "Companies", requestParameters, newCompany);
        if (result.IsSuccessStatusCode)
            return result.Headers.Location.ToString();
        else
            return result.ReasonPhrase;
    }


    public async Task DeleteCompany(int companyId)
    {
        var requestParameters = new Dictionary<string, string> {
            { "id", companyId.ToString() }
        };

        var result = await SendHttpRequest(HttpMethod.Delete, "Companies", requestParameters);

        if (!result.IsSuccessStatusCode)
            throw new Exception(String.Format("Error while deleting company with id {0}.", companyId));
    }
    #endregion

    #region Filing
    public async Task<string> AddFiling(Filing newFiling, int companyId)
    {
        var requestParameters = new Dictionary<string, string> {
            { "companyId", companyId.ToString() }
        };

        var result = await SendHttpRequest(HttpMethod.Post, "Filings", parameters: requestParameters, bodyContent: newFiling);
        if (result.IsSuccessStatusCode)
            return result.Headers.Location.ToString();
        else
            return result.ReasonPhrase;
    }

    public async Task<List<Filing>?> GetFilings(int companyId)
    {
        List<Filing>? filings = null;

        var result = await SendHttpRequest(HttpMethod.Get, "Companies/" + companyId + "/Filings");

        if (result.IsSuccessStatusCode)
            filings = await result.Content.ReadFromJsonAsync<List<Filing>>();

        return filings;
    }
    public async Task<Filing?> GetFiling(int id)
    {
        Filing? filing = null;

        var requestParameters = new Dictionary<string, string> {
            { "id", id.ToString() }
        };

        var result = await SendHttpRequest(HttpMethod.Get, "Filings", requestParameters);

        if (result.IsSuccessStatusCode)
            filing = await result.Content.ReadFromJsonAsync<Filing>();

        return filing;
    }

    public async Task<string> UpdateFiling(int filingId, Filing newFiling)
    {
        var requestParameters = new Dictionary<string, string> {
            { "id", filingId.ToString() }
        };

        var result = await SendHttpRequest(HttpMethod.Put, "Filings", requestParameters, newFiling);
        if (result.IsSuccessStatusCode)
            return result.Headers.Location.ToString();
        else
            return result.ReasonPhrase;
    }

    public async Task DeleteFiling(int filingId)
    {
        var requestParameters = new Dictionary<string, string> {
            { "id", filingId.ToString() }
        };

        var result = await SendHttpRequest(HttpMethod.Delete, "Filing", requestParameters);

        if (!result.IsSuccessStatusCode)
            throw new Exception(String.Format("Error while deleting filing with id {0}.", filingId));
    }
    #endregion

    private async Task<HttpResponseMessage> SendHttpRequest(HttpMethod method, string location, Dictionary<string, string> parameters = null, object bodyContent = null)
    {
        var builder = new UriBuilder(Hostname);
        builder.Scheme = "https";
        builder.Port = Port;
        builder.Path = ApiUrl + location;

        if(parameters != null)
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

