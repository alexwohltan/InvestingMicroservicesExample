using System;
namespace Benchmark
{
	public class Metric
	{
        public string MetricName { get; set; }
        [JsonIgnore]
        public Dictionary<string, decimal> RawDataPoints { get; set; }
        [NotMapped]
        [JsonIgnore]
        private List<decimal> RawValues => RawDataPoints.Values.OrderBy(e => e).ToList();

        [NotMapped]
        [JsonIgnore]
        public List<decimal> Values => RawValues.Where(e => e > MinValue && e < MaxValue).ToList();

        [NotMapped]
        [JsonIgnore]
        private decimal RawMedian => Percentile(RawValues, 0.5m);
        [NotMapped]
        [JsonIgnore]
        private decimal RawPerc25 => Percentile(RawValues, 0.25m);
        [NotMapped]
        [JsonIgnore]
        private decimal RawPerc75 => Percentile(RawValues, 0.75m);
        [NotMapped]
        [JsonIgnore]
        private decimal IQR => RawPerc75 - RawPerc25;

        [NotMapped]
        public decimal MaxValue => (RawMedian + IQR * 1.5m) < RawValues.Max() ? (RawMedian + IQR * 1.5m) : RawValues.Max();
        [NotMapped]
        public decimal MinValue => (RawMedian - IQR * 1.5m) > RawValues.Min() ? (RawMedian - IQR * 1.5m) : RawValues.Min();

        [NotMapped]
        public decimal Percentile01 => Percentile(Values, 0.01m);
        [NotMapped]
        public decimal Percentile05 => Percentile(Values, 0.05m);
        [NotMapped]
        public decimal Percentile10 => Percentile(Values, 0.10m);
        [NotMapped]
        public decimal Percentile25 => Percentile(Values, 0.25m);
        [NotMapped]
        public decimal Median => Percentile(Values, 0.5m);
        [NotMapped]
        public decimal Average => Values.Average();
        [NotMapped]
        public decimal Percentile75 => Percentile(Values, 0.75m);
        [NotMapped]
        public decimal Percentile90 => Percentile(Values, 0.90m);
        [NotMapped]
        public decimal Percentile95 => Percentile(Values, 0.95m);
        [NotMapped]
        public decimal Percentile99 => Percentile(Values, 0.99m);

        [NotMapped]
        [JsonIgnore]
        private IEnumerable<decimal> Top5Values => Values.OrderByDescending(e => e).Take(5);
        [NotMapped]
        [JsonIgnore]
        private IEnumerable<decimal> Worst5Values => Values.Take(5);

        [NotMapped]
        public IEnumerable<string> TopCompanies => RawDataPoints.Where(e => Top5Values.Contains(e.Value)).Select(e => e.Key);
        [NotMapped]
        public IEnumerable<string> WorstCompanies => RawDataPoints.Where(e => Worst5Values.Contains(e.Value)).Select(e => e.Key);

        public Metric(string metricName)
            : this (metricName, new Dictionary<string, decimal>())
        {
            
            
        }
        public Metric(string metricName, Dictionary<string, decimal> rawDataPoints)
        {
            MetricName = metricName;
            RawDataPoints = rawDataPoints.OrderBy(e => e.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public void AddCompany(CompanyFundamentals company)
        {
            var value = GetMetricValueFromCompanyFundamentals(company);

            if (value == null || value == 0)
                return;

            if (RawDataPoints.ContainsKey(company.Ticker))
                UpdateCompany(company);
            else
                RawDataPoints.Add(company.Ticker, (decimal)value);
        }
        public void UpdateCompany(CompanyFundamentals company)
        {
            var value = GetMetricValueFromCompanyFundamentals(company);

            if (value == null || value == 0)
                return;

            if (!RawDataPoints.ContainsKey(company.Ticker))
                AddCompany(company);
            else
                RawDataPoints[company.Ticker] = (decimal)value;
        }
        public void DeleteCompany(CompanyFundamentals company)
        {
            DeleteCompany(company.Ticker);
        }
        public void DeleteCompany(string ticker)
        {
            if (RawDataPoints.ContainsKey(ticker))
                RawDataPoints.Remove(ticker);
        }

        private decimal? GetMetricValueFromCompanyFundamentals(CompanyFundamentals company)
        {
            return (decimal?)company.GetType().GetProperty(MetricName).GetValue(company);
        }

        private static decimal Percentile(List<decimal> sequence, decimal percentile)
        {
            int N = sequence.Count;
            decimal n = (N - 1) * percentile + 1;
            // Another method: double n = (N + 1) * excelPercentile;
            if (n == 1m) return sequence[0];
            else if (n == N) return sequence[N - 1];
            else
            {
                int k = (int)n;
                decimal d = n - k;
                return sequence[k - 1] + d * (sequence[k] - sequence[k - 1]);
            }
        }
    }
}

