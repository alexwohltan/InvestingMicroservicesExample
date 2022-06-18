using System.Text;
using System.Text.Json;
using System.Web;

namespace DataManagementHTTPClient;

public class DataManagementClient
{
    public HttpClient Client { get; set; }

    public string Hostname { get; set; }
    public int Port { get; set; }
    public string ApiUrl { get; set; }

    public DataManagementClient(string hostname, int port, string baseUrl)
    {
        Client = new HttpClient();
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        Hostname = hostname;
        Port = port;
        ApiUrl = baseUrl;
    }

    public async Task<string> StartImportCompanyJob(string ticker, string marketName, string sectorName, string industryName)
    {
        var requestParameters = new Dictionary<string, string> {
            { "marketName", marketName },
            { "sectorName", sectorName },
            { "industryName", industryName }
        };

        var result = await SendHttpRequest(HttpMethod.Get, "Jobs/companies/import/" + ticker, requestParameters);

        var resultText = await result.Content.ReadAsStringAsync();

        if (result.IsSuccessStatusCode)
            return resultText;

        return "ERROR";
    }
    public async Task<string> StartUpdateCompanyJob(string ticker)
    {
        var result = await SendHttpRequest(HttpMethod.Get, "Jobs/companies/update/" + ticker);

        var resultText = await result.Content.ReadAsStringAsync();

        if (result.IsSuccessStatusCode)
            return resultText;

        return "ERROR";
    }

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


