

namespace Benchmark
{
	public class Benchmark
	{
        [NotMapped]
        public virtual IEnumerable<Metric> Metrics { get; private set; }
        [JsonIgnore]
        public virtual Dictionary<string, DateTime> Companies { get; private set; }

        public Benchmark()
        {
            GenerateMetrics();
            Companies = new Dictionary<string, DateTime>();
        }
        public Benchmark(IEnumerable<CompanyFundamentals> companies)
            : this()
        {
            foreach (var company in companies)
            {
                AddCompany(company);
            }
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

                if (property.GetGetMethod().ReturnType == typeof(decimal))
                    metrics.Add(new Metric(property.Name));
            }

            Metrics = metrics;
        }

        public void AddCompany(CompanyFundamentals company)
        {
            foreach (var metric in Metrics)
            {
                metric.AddCompany(company);
            }
            Companies.Add(company.Ticker, company.LatestFilingDate);
        }
        public void UpdateCompany(CompanyFundamentals company)
        {
            var compTicker = Companies.Keys.FirstOrDefault(e => e == company.Ticker);
            if (compTicker == null)
                throw new ArgumentException("Company " + company.Ticker + " does not exist in Benchmark.");

            Companies[compTicker] = company.LatestFilingDate;

            foreach (var metric in Metrics)
            {
                metric.UpdateCompany(company);
            }
        }
        public void DeleteCompany(CompanyFundamentals company)
        {
            var compTicker = Companies.Keys.FirstOrDefault(e => e == company.Ticker);
            if (compTicker == null)
                throw new ArgumentException("Company " + company.Ticker + " does not exist in Benchmark.");

            foreach (var metric in Metrics)
            {
                metric.DeleteCompany(company);
            }

            Companies.Remove(compTicker);
        }
        public void DeleteCompany(string ticker)
        {
            var compTicker = Companies.Keys.FirstOrDefault(e => e == ticker);
            if (compTicker == null)
                throw new ArgumentException("Company " + ticker + " does not exist in Benchmark.");

            foreach (var metric in Metrics)
            {
                metric.DeleteCompany(ticker);
            }

            Companies.Remove(compTicker);
        }
    }
}

