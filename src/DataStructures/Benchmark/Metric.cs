namespace DataStructures.Benchmark
{
	public class Metric
	{
        [Key]
        public int Id { get; set; }

        [ForeignKey("Benchmark")]
        public virtual int BenchmarkID { get; set; }
        [JsonIgnore]
        public virtual Benchmark Benchmark { get; set; }

        public virtual string MetricName { get; set; }
        [JsonIgnore]
        public virtual List<MetricDataPoint> RawDataPoints { get; set; }
        [NotMapped]
        [JsonIgnore]
        private List<decimal?> RawValues => RawDataPoints.Select(e => e.Value).OrderBy(e => e).ToList();

        [NotMapped]
        [JsonIgnore]
        public List<decimal?> Values => RawValues.Where(e => e > MinValue && e < MaxValue).ToList();

        [NotMapped]
        [JsonIgnore]
        private decimal? RawMedian => Percentile(RawValues, 0.5m);
        [NotMapped]
        [JsonIgnore]
        private decimal? RawPerc25 => Percentile(RawValues, 0.25m);
        [NotMapped]
        [JsonIgnore]
        private decimal? RawPerc75 => Percentile(RawValues, 0.75m);
        [NotMapped]
        [JsonIgnore]
        private decimal? IQR => RawPerc75 - RawPerc25;

        //[NotMapped]
        public decimal? MaxValue { get; set; }
        //[NotMapped]
        public decimal? MinValue { get; set; }

        //[NotMapped]
        public decimal? Percentile01 { get; set; }
        //[NotMapped]
        public decimal? Percentile05 { get; set; }
        //[NotMapped]
        public decimal? Percentile10 { get; set; }
        //[NotMapped]
        public decimal? Percentile25 { get; set; }
        //[NotMapped]
        public decimal? Median { get; set; }
        //[NotMapped]
        public decimal? Average { get; set; }
        //[NotMapped]
        public decimal? Percentile75 { get; set; }
        //[NotMapped]
        public decimal? Percentile90 { get; set; }
        //[NotMapped]
        public decimal? Percentile95 { get; set; }
        //[NotMapped]
        public decimal? Percentile99 { get; set; }

        //[NotMapped]
        //[JsonIgnore]
        //private IEnumerable<decimal?> Top5Values => Values.OrderByDescending(e => e).Take(5);
        //[NotMapped]
        //[JsonIgnore]
        //private IEnumerable<decimal?> Worst5Values => Values.Take(5);

        //[NotMapped]
        public virtual List<string?> TopCompanies { get; set; }
        //[NotMapped]
        public virtual List<string?> WorstCompanies { get; set; }

        public virtual void AddCompany(CompanyFundamentals company)
        {
            var value = GetMetricValueFromCompanyFundamentals(company);

            if (value == null || value == 0)
                return;

            if (RawDataPoints.FirstOrDefault(e => e.Ticker == company.Ticker) != null)
                UpdateCompany(company);
            else
                RawDataPoints.Add(new MetricDataPoint() { Ticker = company.Ticker, Value = value, CompanyName = company.Name });

            RecalculateValues();
        }
        public virtual void UpdateCompany(CompanyFundamentals company)
        {
            var value = GetMetricValueFromCompanyFundamentals(company);

            if (value == null || value == 0)
                return;

            var dataPoint = RawDataPoints.FirstOrDefault(e => e.Ticker == company.Ticker);

            if (dataPoint == null)
                AddCompany(company);
            else
                dataPoint.Value = value;

            RecalculateValues();
        }
        public virtual void DeleteCompany(CompanyFundamentals company)
        {
            DeleteCompany(company.Ticker);
        }
        public virtual void DeleteCompany(string ticker)
        {
            var dataPoint = RawDataPoints.FirstOrDefault(e => e.Ticker == ticker);

            if (dataPoint != null)
                RawDataPoints.Remove(dataPoint);

            RecalculateValues();
        }

        /// <summary>
        /// Gives the percentile in which a number would be in this series.
        /// </summary>
        /// <param name="value">the number that should be compared to the values.</param>
        /// <returns>number of percentile -> returns 25 for Percentile25, 0 = less than 1% of the values are higher than the provided value</returns>
        public int GetPercentileForValue(decimal value)
        {
            if (value >= Percentile99)
                return 99;
            if (value >= Percentile95)
                return 95;
            if (value >= Percentile90)
                return 90;
            if (value >= Percentile75)
                return 75;
            if (value >= Median)
                return 50;
            if (value >= Percentile25)
                return 25;
            if (value >= Percentile10)
                return 10;
            if (value >= Percentile05)
                return 5;
            if (value >= Percentile01)
                return 1;
            return 0;
        }

        private void RecalculateValues()
        {
            if (RawDataPoints == null)
                return;
            if (RawDataPoints.Count() < 3)
                return;


            MaxValue = (RawMedian + IQR * 1.5m) < RawValues.Max() ? (RawMedian + IQR * 1.5m) : RawValues.Max();
            MinValue = (RawMedian - IQR * 1.5m) > RawValues.Min() ? (RawMedian - IQR * 1.5m) : RawValues.Min();

            Percentile01 = Percentile(Values, 0.01m);
            Percentile05 = Percentile(Values, 0.05m);
            Percentile10 = Percentile(Values, 0.10m);
            Percentile25 = Percentile(Values, 0.25m);
            Median = Percentile(Values, 0.5m);
            Average = Values.Average();
            Percentile75 = Percentile(Values, 0.75m);
            Percentile90 = Percentile(Values, 0.90m);
            Percentile95 = Percentile(Values, 0.95m);
            Percentile99 = Percentile(Values, 0.99m);

            var top5Values = Values.OrderByDescending(e => e).Take(5);
            var worst5Values = Values.Take(5);

            TopCompanies = RawDataPoints.Where(e => top5Values.Contains(e.Value)).Select(e => e.CompanyName).ToList();
            WorstCompanies = RawDataPoints.Where(e => worst5Values.Contains(e.Value)).Select(e => e.CompanyName).ToList();
        }

        private decimal? GetMetricValueFromCompanyFundamentals(CompanyFundamentals company)
        {
            return (decimal?)company.GetType().GetProperty(MetricName).GetValue(company);
        }

        private static decimal Percentile(List<decimal?> sequence, decimal percentile)
        {
            if (sequence == null)
                return 0;

            int N = sequence.Count;

            if (N == 0)
                return 0;
            if (N == 1)
                return 0;

            decimal n = (N - 1) * percentile + 1;
            // Another method: double n = (N + 1) * excelPercentile;
            if (n == 1m) return (decimal)sequence[0];
            else if (n == N) return (decimal)sequence[N - 1];
            else
            {
                int k = (int)n;
                decimal d = n - k;
                return (decimal)sequence[k - 1] + d * ((decimal)sequence[k] - (decimal)sequence[k - 1]);
            }
        }
    }

    public class MetricDataPoint
    {
        [Key]
        public int Id { get; set; }
        public string? Ticker { get; set; }
        public string? CompanyName { get; set; }
        public decimal? Value { get; set; }
    }
    //public class MetricDictionary : List<MetricDataPoint>
    //{
    //    public IEnumerable<string> Keys => Tickers;
    //    public IEnumerable<string> Tickers => this.Select(e => e.Ticker);

    //    public IEnumerable<decimal?> Values => this.Select(e => e.Value);

    //    public MetricDataPoint this[string ticker]
    //    {
    //        get { return this.FirstOrDefault(e => e.Ticker == ticker); }
    //    }

    //    public void Add(string ticker, decimal? value)
    //    {
    //        Add(new MetricDataPoint() { Value = value, Ticker = ticker });
    //    }

    //    public void SetValue(string ticker, decimal? value)
    //    {
    //        foreach (var comp in this.Where(e => e.Ticker == ticker))
    //        {
    //            comp.Value = value;
    //        }
    //    }

    //    public void Remove(string ticker)
    //    {
    //        var comp = this.FirstOrDefault(e => e.Ticker == ticker);

    //        if (comp != null)
    //            Remove(comp);
    //    }

    //    public bool ContainsKey(string ticker)
    //    {
    //        return Keys.Contains(ticker);
    //    }
    //}
}

