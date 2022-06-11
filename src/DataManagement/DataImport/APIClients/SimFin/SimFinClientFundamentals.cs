using System;
namespace DataImport.APIClients.SimFin
{
	public partial class SimFinClient
	{
		public HttpClient Client { get; set; }

        public string Hostname { get; set; }
		public int Port { get; set; }
		public string ApiUrl { get; set; }
        public string ApiKey { get; set; }

        public SimFinClient(string hostname, int port, string baseUrl, string apiKey)
        {
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            Hostname = hostname;
            Port = port;
            ApiUrl = baseUrl;
            ApiKey = apiKey;
        }

        #region Company

        #region List of all companies in the database
        public async Task<IEnumerable<string>> ListAllCompaniesInSimFin()
        {
            var result = await SendHttpRequest(HttpMethod.Get, "companies/list");
            var response = new SimFinCompaniesListResponse();

            if (result.IsSuccessStatusCode)
                response = await result.Content.ReadFromJsonAsync<SimFinCompaniesListResponse>();

            return response.Tickers;
        }
        #endregion

        #region General company information
        public async Task<SimFinGeneralCompanyInformationResponse> GetGeneralCompanyInformation(List<string> tickers)
        {
            var requestParameters = new Dictionary<string, string> {
                { "ticker", String.Join(",", tickers) }
            };

            var result = await SendHttpRequest(HttpMethod.Get, "companies/general", requestParameters);
            Debug.WriteLine(await result.Content.ReadAsStringAsync());
            var response = new SimFinGeneralCompanyInformationResponse();

            if (result.IsSuccessStatusCode)
                response = await result.Content.ReadFromJsonAsync<SimFinGeneralCompanyInformationResponse>();

            return response;
        }
        public async Task<SimFinGeneralCompanyInformationResponse> GetGeneralCompanyInformation(List<int> ids)
        {
            var requestParameters = new Dictionary<string, string> {
                { "id", String.Join(",", ids) }
            };

            var result = await SendHttpRequest(HttpMethod.Get, "companies/general", requestParameters);
            Debug.WriteLine(await result.Content.ReadAsStringAsync());
            var response = new SimFinGeneralCompanyInformationResponse();

            if (result.IsSuccessStatusCode)
                response = await result.Content.ReadFromJsonAsync<SimFinGeneralCompanyInformationResponse>();

            return response;
        }
        #endregion

        #region Fundamentals
        /// <summary>
        /// Fundamentals and derived figures can be retrieved here. You have to provide 'ticker' (see below for details).
        /// </summary>
        /// 
        /// <param name="tickers">Ticker of company; you can provide one or more tickers here. Please note: providing multiple tickers is reserved for SimFin+ users. Multiple values have to be separated by commas.</param>
        /// 
        /// <param name="statement">Enum: "pl" "bs" "cf" "derived" "all" Example: 'statement=pl' Statement to be retrieved.
        /// pl: Profit & Loss statement
        /// bs: Balance Sheet
        /// cf: Cash Flow
        /// derived: Derived figures & fundamental ratios
        /// all: Retrieves all 3 statements + ratios. Please note that this option is reserved for SimFin+ users.</param>
        ///
        /// <param name="periods">Filter for periods. As a non-SimFin+ user, you have to provide exactly one period. As SimFin+ user, this filter can be omitted to retrieve all statements available for the company. As SimFin+ user, you can also chain this filter with a comma (e.g. period=quarters,fy to retrieve all quarters and the full financial year figures)
        /// q1: First fiscal quarter.
        /// q2: Second fiscal quarter.
        /// q3: Third fiscal quarter.
        /// q4: Fourth fiscal quarter.
        /// fy: Full fiscal year.
        /// h1: First 6 months of fiscal year.
        /// h2: Last 6 months of fiscal year.
        /// 9m: First nine months of fiscal year.
        /// 6m: Any fiscal 6 month period (first + second half years; reserved for SimFin+ users).
        /// quarters: All quarters(q1 + q2 + q3 + q4; reserved for SimFin+ users).</param>
        /// 
        /// <param name="fiscalYears">Filter for fiscal year. As a non-SimFin+ user, you have to provide exactly one fiscal year. As SimFin+ user, this filter can be omitted to retrieve all statements available for the company. As SimFin+ user, you can also chain this filter with a comma, to retrieve multiple years at once (e.g. fyear=2015,2016,2017 to retrieve the data for 3 years at once).</param>
        /// <param name="startDate">Filter for the report dates (reserved for SimFin+ users). With this filter you can filter the statements by the date on which the reported period ended ('Report Date'). Date format is YYYY-MM-DD. By specifying a value here, only statements will be retrieved with report dates ending AFTER the specified date.</param>
        /// <param name="endDate">Filter for the report dates (reserved for SimFin+ users). With this filter you can filter the statements by the date on which the reported period ended ('Report Date'). Date format is YYYY-MM-DD. By specifying a value here, only statements will be retrieved with report dates ending BEFORE the specified date.</param>
        /// <param name="ttm">By adding '&ttm' to the query, you can return the trailing twelve months statements for all periods, meaning at every available point in time the sum of the last 4 available quarterly figures.</param>
        /// <param name="asreported">By adding '&asreported' to the query, you can return the financial statements that were initially reported by the company, as opposed to the most recently restated ones if available (these are the default). This is a SimFin+ only feature.</param>
        /// <param name="shares">By adding '&shares' to the query, you can display the weighted average basic & diluted shares outstanding for each period along with the fundamentals. Reserved for SimFin+ users (as non-SimFin+ user, you can still use the shares outstanding endpoints).</param>
        /// <returns></returns>
        public async Task<IEnumerable<SimFinFundamentalsCompany>> GetCompanyFundamentals(List<string> tickers, string statement, List<string>? periods = null, List<int>? fiscalYears = null, DateTime? startDate = null, DateTime? endDate = null, bool ttm = false, bool asreported = false, bool shares = false)
        {
            var requestParameters = new Dictionary<string, string> {
                { "ticker", String.Join(",", tickers) },
                { "statement", statement }
            };

            if (periods != null)
                requestParameters.Add("period", String.Join(",", periods));
            if (fiscalYears != null)
                requestParameters.Add("fyear", String.Join(",", fiscalYears));
            if (startDate != null)
                requestParameters.Add("start", ((DateTime)startDate).ToString("yyyy-MM-dd"));
            if (endDate != null)
                requestParameters.Add("end", ((DateTime)endDate).ToString("yyyy-MM-dd"));

            var specialParameters = "";

            if (ttm)
                specialParameters += "&ttm";
            if (asreported)
                specialParameters += "&asreported";
            if (shares)
                specialParameters += "&shares";

            var result = await SendHttpRequest(HttpMethod.Get, "companies/statements", specialParameters, requestParameters);
            var response = new SimFinFundamentalsResponse();

            if (result.IsSuccessStatusCode)
                response = await result.Content.ReadFromJsonAsync<SimFinFundamentalsResponse>();

            return response.FundamentalsCompanies;
        }
        /// <summary>
        /// Fundamentals and derived figures can be retrieved here. You have to provide 'id' (SimFin Ids) (see below for details).
        /// </summary>
        /// 
        /// <param name="ids">SimFin ID; you can provide one or more SimFin IDs here. Please note: providing multiple IDs is reserved for SimFin+ users. Multiple values have to be separated by commas.</param>
        /// 
        /// <param name="statement">Enum: "pl" "bs" "cf" "derived" "all" Example: 'statement=pl' Statement to be retrieved.
        /// pl: Profit & Loss statement
        /// bs: Balance Sheet
        /// cf: Cash Flow
        /// derived: Derived figures & fundamental ratios
        /// all: Retrieves all 3 statements + ratios. Please note that this option is reserved for SimFin+ users.</param>
        ///
        /// <param name="period">Filter for periods. As a non-SimFin+ user, you have to provide exactly one period. As SimFin+ user, this filter can be omitted to retrieve all statements available for the company. As SimFin+ user, you can also chain this filter with a comma (e.g. period=quarters,fy to retrieve all quarters and the full financial year figures)
        /// q1: First fiscal quarter.
        /// q2: Second fiscal quarter.
        /// q3: Third fiscal quarter.
        /// q4: Fourth fiscal quarter.
        /// fy: Full fiscal year.
        /// h1: First 6 months of fiscal year.
        /// h2: Last 6 months of fiscal year.
        /// 9m: First nine months of fiscal year.
        /// 6m: Any fiscal 6 month period (first + second half years; reserved for SimFin+ users).
        /// quarters: All quarters(q1 + q2 + q3 + q4; reserved for SimFin+ users).</param>
        /// 
        /// <param name="fiscalYear">Filter for fiscal year. As a non-SimFin+ user, you have to provide exactly one fiscal year. As SimFin+ user, this filter can be omitted to retrieve all statements available for the company. As SimFin+ user, you can also chain this filter with a comma, to retrieve multiple years at once (e.g. fyear=2015,2016,2017 to retrieve the data for 3 years at once).</param>
        /// <param name="startDate">Filter for the report dates (reserved for SimFin+ users). With this filter you can filter the statements by the date on which the reported period ended ('Report Date'). Date format is YYYY-MM-DD. By specifying a value here, only statements will be retrieved with report dates ending AFTER the specified date.</param>
        /// <param name="endDate">Filter for the report dates (reserved for SimFin+ users). With this filter you can filter the statements by the date on which the reported period ended ('Report Date'). Date format is YYYY-MM-DD. By specifying a value here, only statements will be retrieved with report dates ending BEFORE the specified date.</param>
        /// <param name="ttm">By adding '&ttm' to the query, you can return the trailing twelve months statements for all periods, meaning at every available point in time the sum of the last 4 available quarterly figures.</param>
        /// <param name="asreported">By adding '&asreported' to the query, you can return the financial statements that were initially reported by the company, as opposed to the most recently restated ones if available (these are the default). This is a SimFin+ only feature.</param>
        /// <param name="shares">By adding '&shares' to the query, you can display the weighted average basic & diluted shares outstanding for each period along with the fundamentals. Reserved for SimFin+ users (as non-SimFin+ user, you can still use the shares outstanding endpoints).</param>
        /// <returns></returns>
        public async Task<IEnumerable<SimFinFundamentalsCompany>> GetCompanyFundamentals(List<int> ids, string statement, List<string>? periods = null, List<int>? fiscalYears = null, DateTime? startDate = null, DateTime? endDate = null, bool ttm = false, bool asreported = false, bool shares = false)
        {
            var requestParameters = new Dictionary<string, string> {
                { "id", String.Join(",", ids) },
                { "statement", statement }
            };

            if (periods != null)
                requestParameters.Add("period", String.Join(",", periods));
            if (fiscalYears != null)
                requestParameters.Add("fyear", String.Join(",", fiscalYears));
            if (startDate != null)
                requestParameters.Add("start", ((DateTime)startDate).ToString("yyyy-MM-dd"));
            if (endDate != null)
                requestParameters.Add("end", ((DateTime)endDate).ToString("yyyy-MM-dd"));

            var specialParameters = "";

            if (ttm)
                specialParameters += "&ttm";
            if (asreported)
                specialParameters += "&asreported";
            if (shares)
                specialParameters += "&shares";

            var result = await SendHttpRequest(HttpMethod.Get, "companies/statements", specialParameters, requestParameters);
            var response = new SimFinFundamentalsResponse();

            if (result.IsSuccessStatusCode)
                response = await result.Content.ReadFromJsonAsync<SimFinFundamentalsResponse>();

            return response.FundamentalsCompanies;
        }

        public async Task<IEnumerable<SimFinFundamentalsCompany>> GetCompanyFundamentalsBasicLicense(List<string> tickers, string period, int? fiscalYear = null)
        {
            var result = new List<SimFinFundamentalsCompany>();

            foreach (var ticker in tickers)
            {
                SimFinFundamentalsCompany company = null;

                var requestingYears = new List<int>();
                if (fiscalYear == null)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        requestingYears.Add(DateTime.Now.Year - i - 1);
                    }
                }
                else
                    requestingYears.Add((int)fiscalYear);

                foreach (var fyear in requestingYears)
                {
                    if (company == null)
                        company = await GetCompanyFundamentalsBasicLicense(ticker, period, fyear);
                    else
                        company.Merge(await GetCompanyFundamentalsBasicLicense(ticker, period, fyear));

                }

                result.Add(company);
            }

            return result;
        }
        public async Task<SimFinFundamentalsCompany> GetCompanyFundamentalsBasicLicense(string ticker, string period, int fiscalYear)
        {
            var company = await GetCompanyFundamentalsBasicLicense(ticker, "pl", period, fiscalYear);
            company.Merge(await GetCompanyFundamentalsBasicLicense(ticker, "bs", period, fiscalYear));
            company.Merge(await GetCompanyFundamentalsBasicLicense(ticker, "cf", period, fiscalYear));
            company.Merge(await GetCompanyFundamentalsBasicLicense(ticker, "derived", period, fiscalYear));
            return company;
        }
        public async Task<SimFinFundamentalsCompany> GetCompanyFundamentalsBasicLicense(string ticker, string statement, string period, int fiscalYear)
        {
            return (await GetCompanyFundamentals(new List<string> { ticker }, statement, new List<string> { period }, new List<int> { fiscalYear })).First();
        }
        #endregion

        #endregion

        private async Task<HttpResponseMessage> SendHttpRequest(HttpMethod method, string location, Dictionary<string, string> parameters = null, object bodyContent = null)
        {
            return await SendHttpRequest(method, location, null, parameters, bodyContent);
        }
        private async Task<HttpResponseMessage> SendHttpRequest(HttpMethod method, string location, string? specialParameters, Dictionary<string, string>? parameters = null, object bodyContent = null)
        {
            var builder = new UriBuilder(Hostname);
            builder.Scheme = "https";
            builder.Port = Port;
            builder.Path = ApiUrl + location;

            builder.Query = specialParameters == null ? QueryParametersEncoded(parameters) : QueryParametersEncoded(parameters) + specialParameters;

            var uri = builder.Uri;

            var request = new HttpRequestMessage(method, uri);

            if (bodyContent != null)
            {
                var json = JsonSerializer.Serialize(bodyContent);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            return await Client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
        }
        private string QueryParametersEncoded(Dictionary<string, string>? parameters)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["api-key"] = ApiKey;
            if (parameters != null)
                foreach (var parameter in parameters)
                {
                    query[parameter.Key] = parameter.Value;
                }
            return query.ToString();
        }
    }
}

