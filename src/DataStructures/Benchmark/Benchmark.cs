
namespace DataStructures.Benchmark
{
	public class Benchmark
	{
        [Key]
        public int Id { get; set; }

        public virtual List<Metric> Metrics { get; set; }
        [JsonIgnore]
        public virtual List<CompanyLatestFiling> CompanyDates { get; private set; }

        public void Initialize()
        {
            GenerateMetrics();
            CompanyDates = new List<CompanyLatestFiling>();
        }

        public void GenerateMetrics()
        {
            // get type of companyfundamentals -> generate Metric for every property that does not have a specific attribute -> not in constructor
            var metrics = new List<Metric>();
            var properties = typeof(CompanyFundamentals).GetProperties();

            foreach (var property in properties)
            {
                if (property.CustomAttributes.FirstOrDefault(e => e.AttributeType == typeof(CompanyFundamentalsMetric)) == null)
                    continue;

                if (property.GetGetMethod() == null)
                    continue;

                var propGetMethod = property.GetGetMethod();

                if (property.GetGetMethod().ReturnType == typeof(decimal?) || property.GetGetMethod().ReturnType == typeof(decimal))
                    metrics.Add(new Metric() { MetricName = property.Name });
            }

            Metrics = metrics;
        }

        public void AddCompany(CompanyFundamentals company)
        {
            foreach (var metric in Metrics)
            {
                metric.AddCompany(company);
            }
            CompanyDates.Add(new CompanyLatestFiling() { LatestFilingDate = company.LatestFilingDate, Ticker = company.Ticker });
        }
        public void UpdateCompany(CompanyFundamentals company)
        {
            var compDate = CompanyDates.FirstOrDefault(e => e.Ticker == company.Ticker);
            if (compDate == null)
                throw new ArgumentException("Company " + company.Ticker + " does not exist in Benchmark.");

            compDate.LatestFilingDate = company.LatestFilingDate;

            foreach (var metric in Metrics)
            {
                metric.UpdateCompany(company);
            }
        }
        public void DeleteCompany(CompanyFundamentals company)
        {
            var compDate = CompanyDates.FirstOrDefault(e => e.Ticker == company.Ticker);
            if (compDate == null)
                throw new ArgumentException("Company " + company.Ticker + " does not exist in Benchmark.");

            foreach (var metric in Metrics)
            {
                metric.DeleteCompany(company);
            }

            CompanyDates.Remove(compDate);
        }
        public void DeleteCompany(string ticker)
        {
            var compDate = CompanyDates.FirstOrDefault(e => e.Ticker == ticker);
            if (compDate == null)
                throw new ArgumentException("Company " + ticker + " does not exist in Benchmark.");

            foreach (var metric in Metrics)
            {
                metric.DeleteCompany(ticker);
            }

            CompanyDates.Remove(compDate);
        }
    }

    public class CompanyLatestFiling
    {
        [Key]
        public int Id { get; set; }
        public string? Ticker { get; set; }
        public DateTime LatestFilingDate { get; set; }
    }

    //public class BenchmarkDictionary : List<CompanyLatestFiling>
    //{
    //    public IEnumerable<string> Keys => Tickers;

    //    public IEnumerable<string> Tickers => this.Select(e => e.Ticker);

    //    public CompanyLatestFiling this[string ticker]
    //    {
    //        get { return this.FirstOrDefault(e => e.Ticker == ticker); }
    //    }

    //    public void Add(string ticker, DateTime latestFilingDate)
    //    {
    //        Add(new CompanyLatestFiling() { LatestFilingDate = latestFilingDate, Ticker = ticker });
    //    }

    //    public void SetLatestFilingDate(string ticker, DateTime latestFilingDate)
    //    {
    //        foreach (var comp in this.Where(e => e.Ticker == ticker))
    //        {
    //            comp.LatestFilingDate = latestFilingDate;
    //        }
    //    }

    //    public void Remove(string ticker)
    //    {
    //        var comp = this.FirstOrDefault(e => e.Ticker == ticker);

    //        if(comp != null)
    //            Remove(comp);
    //    }

    //    public bool ContainsKey(string ticker)
    //    {
    //        return Keys.Contains(ticker);
    //    }
    //}
}

