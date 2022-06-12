using System;
namespace DataImport.APIClients.SimFin
{
	public partial class SimFinClient
	{
        #region Share Price Data
        // Share price data and ratios can be retrieved here. All share prices are adjusted for stock splits. The column 'Adj. Close' is also adjusted for dividends.


        public async Task<IEnumerable<SimFinSharePriceCollection>> GetSharePriceData(List<string> tickers, bool ratios = false, bool asreported = false, DateTime? startDate = null, DateTime? endDate = null)
        {
            var requestParameters = new Dictionary<string, string> {
                { "ticker", String.Join(",", tickers) }
            };

            if (startDate != null)
                requestParameters.Add("start", ((DateTime)startDate).ToString("yyyy-MM-dd"));
            if (endDate != null)
                requestParameters.Add("end", ((DateTime)endDate).ToString("yyyy-MM-dd"));

            var specialParameters = "";
            if (ratios)
                if (asreported)
                    specialParameters = "&ratios&asreported";
                else
                    specialParameters = "&ratios";

            var result = await SendHttpRequest(HttpMethod.Get, "companies/prices", specialParameters, requestParameters);
            Debug.WriteLine(await result.Content.ReadAsStringAsync());
            var response = new SimFinSharePriceResponse();

            if (result.IsSuccessStatusCode)
                response = await result.Content.ReadFromJsonAsync<SimFinSharePriceResponse>();

            response.CalculateProperties();

            return response.Companies;
        }
        public async Task<IEnumerable<SimFinSharePriceCollection>> GetSharePriceData(List<int> ids, bool ratios = false, bool asreported = false, DateTime? startDate = null, DateTime? endDate = null)
        {
            var requestParameters = new Dictionary<string, string> {
                { "id", String.Join(",", ids) }
            };

            if (startDate != null)
                requestParameters.Add("start", ((DateTime)startDate).ToString("yyyy-MM-dd"));
            if (endDate != null)
                requestParameters.Add("end", ((DateTime)endDate).ToString("yyyy-MM-dd"));

            var specialParameters = "";
            if (ratios)
                if (asreported)
                    specialParameters = "&ratios&asreported";
                else
                    specialParameters = "&ratios";

            var result = await SendHttpRequest(HttpMethod.Get, "companies/prices", specialParameters, requestParameters);
            Debug.WriteLine(await result.Content.ReadAsStringAsync());
            var response = new SimFinSharePriceResponse();

            if (result.IsSuccessStatusCode)
                response = await result.Content.ReadFromJsonAsync<SimFinSharePriceResponse>();

            return response.Companies;
        }
        #endregion
    }
}

