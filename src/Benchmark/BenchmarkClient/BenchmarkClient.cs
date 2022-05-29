using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using DataStructures.Benchmark;

namespace BenchmarkClient;
public class BenchmarkClient
{
    public HttpClient Client { get; set; }

    public string Hostname { get; set; }
    public int Port { get; set; }
    public string ApiUrl { get; set; }

    public BenchmarkClient(string hostname, int port, string baseUrl)
    {
        Client = new HttpClient();
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        Hostname = hostname;
        Port = port;
        ApiUrl = baseUrl;
    }

    public async Task<MarketBenchmark> GetMarketBenchmark(string marketName)
    {
        MarketBenchmark? benchmark = null;

        var requestParameters = new Dictionary<string, string> {
            { "marketName", marketName }
        };

        var result = await SendHttpRequest(HttpMethod.Get, "Benchmark/markets", requestParameters);

        var resultText = await result.Content.ReadAsStringAsync();

        if (result.IsSuccessStatusCode)
            benchmark = await result.Content.ReadFromJsonAsync<MarketBenchmark>();

        return benchmark;
    }

    public async Task<SectorBenchmark> GetSectorBenchmark(string marketName, string sectorName)
    {
        SectorBenchmark? benchmark = null;

        var requestParameters = new Dictionary<string, string> {
            { "marketName", marketName },
            { "sectorName", sectorName }
        };

        var result = await SendHttpRequest(HttpMethod.Get, "Benchmark/sectors", requestParameters);

        var resultText = await result.Content.ReadAsStringAsync();

        if (result.IsSuccessStatusCode)
            benchmark = await result.Content.ReadFromJsonAsync<SectorBenchmark>();

        return benchmark;
    }

    public async Task<IndustryBenchmark> GetIndustryBenchmark(string marketName, string sectorName, string industryName)
    {
        IndustryBenchmark? benchmark = null;

        var requestParameters = new Dictionary<string, string> {
            { "marketName", marketName },
            { "sectorName", sectorName },
            { "industryName", industryName }
        };

        var result = await SendHttpRequest(HttpMethod.Get, "Benchmark/industries", requestParameters);

        var resultText = await result.Content.ReadAsStringAsync();

        if (result.IsSuccessStatusCode)
            benchmark = await result.Content.ReadFromJsonAsync<IndustryBenchmark>();

        return benchmark;
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

