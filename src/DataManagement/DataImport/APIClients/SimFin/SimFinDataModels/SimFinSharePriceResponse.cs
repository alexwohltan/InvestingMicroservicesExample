using System;
namespace DataImport.APIClients.SimFin
{
	public class SimFinSharePriceResponse : List<SimFinSharePriceBasicCollection>
	{
        public IEnumerable<SimFinSharePriceCollection> Companies { get; set; }

        public void CalculateProperties()
        {
            var companies = new List<SimFinSharePriceCollection>();
            foreach (var collection in this)
            {
                collection.CalculateProperties();
                companies.Add(new SimFinSharePriceCollection() { Prices = collection.Prices });
            }
            Companies = companies;
        }
    }

    public class SimFinSharePriceCollection
    {
        public string? Ticker => Prices != null && Prices.Count() > 0 ? Prices.First().Ticker : null;
        public IEnumerable<SimFinSharePriceEntry> Prices { get; set; }

        public List<StockPrice> ToStockPriceCollection()
        {
            var collection = new List<StockPrice>();

            foreach (var price in Prices)
            {
                collection.Add(price.ToStockPrice());
            }

            return collection;
        }
    }

    public class SimFinSharePriceBasicCollection : SimFinBasic2dDataView
    {
        public int SimFinIdIndex { get; set; }
        public int TickerIndex { get; set; }
        public int DateIndex { get; set; }
        public int OpenIndex { get; set; }
        public int HighIndex { get; set; }
        public int LowIndex { get; set; }
        public int CloseIndex { get; set; }
        public int AdjCloseIndex { get; set; }
        public int VolumeIndex { get; set; }
        public int DividendIndex { get; set; }
        public int CommonSharesOutstandingIndex { get; set; }
        public int MarketCapIndex { get; set; }
        public int PricetoEarningsRatioquarterlyIndex { get; set; }
        public int PricetoEarningsRatiottmIndex { get; set; }
        public int PricetoSalesRatioquarterlyIndex { get; set; }
        public int PricetoSalesRatiottmIndex { get; set; }
        public int PricetoBookValuettmIndex { get; set; }
        public int PricetoFreeCashFlowquarterlyIndex { get; set; }
        public int PricetoFreeCashFlowttmIndex { get; set; }
        public int EnterpriseValuettmIndex { get; set; }
        public int EVEBITDAttmIndex { get; set; }
        public int EVSalesttmIndex { get; set; }
        public int EVFCFttmIndex { get; set; }
        public int BooktoMarketValuettmIndex { get; set; }
        public int OperatingIncomeEVttmIndex { get; set; }

        public IEnumerable<SimFinSharePriceEntry> Prices { get; set; }

        public bool found { get; set; }
        public string? currency { get; set; }

        public void CalculateProperties()
        {
            CalculateIndicies();
            CalculatePriceEntries();
        }
        private void CalculateIndicies()
        {
            SimFinIdIndex = columns.IndexOf("SimFinId");
            TickerIndex = columns.IndexOf("Ticker");
            DateIndex = columns.IndexOf("Date");
            OpenIndex = columns.IndexOf("Open");
            HighIndex = columns.IndexOf("High");
            LowIndex = columns.IndexOf("Low");
            CloseIndex = columns.IndexOf("Close");
            AdjCloseIndex = columns.IndexOf("Adj. Close");
            VolumeIndex = columns.IndexOf("Volume");
            DividendIndex = columns.IndexOf("Dividend");
            CommonSharesOutstandingIndex = columns.IndexOf("Common Shares Outstanding");
            MarketCapIndex = columns.IndexOf("Market-Cap");
            PricetoEarningsRatioquarterlyIndex = columns.IndexOf("Price to Earnings Ratio (quarterly)");
            PricetoEarningsRatiottmIndex = columns.IndexOf("Price to Earnings Ratio (ttm)");
            PricetoSalesRatioquarterlyIndex = columns.IndexOf("Price to Sales Ratio (quarterly)");
            PricetoSalesRatiottmIndex = columns.IndexOf("Price to Sales Ratio (ttm)");
            PricetoBookValuettmIndex = columns.IndexOf("Price to Book Value (ttm)");
            PricetoFreeCashFlowquarterlyIndex = columns.IndexOf("Price to Free Cash Flow (quarterly)");
            PricetoFreeCashFlowttmIndex = columns.IndexOf("Price to Free Cash Flow (ttm)");
            EnterpriseValuettmIndex = columns.IndexOf("Enterprise Value (ttm)");
            EVEBITDAttmIndex = columns.IndexOf("EV/EBITDA (ttm)");
            EVSalesttmIndex = columns.IndexOf("EV/Sales (ttm)");
            EVFCFttmIndex = columns.IndexOf("EV/FCF (ttm)");
            BooktoMarketValuettmIndex = columns.IndexOf("Book to Market Value (ttm)");
            OperatingIncomeEVttmIndex = columns.IndexOf("Operating Income/EV (ttm)");
        }
        private void CalculatePriceEntries()
        {
            var prices = new List<SimFinSharePriceEntry>();

            foreach (var priceData in data)
            {
                prices.Add(new SimFinSharePriceEntry()
                {
                    SimFinId = SimFinIdIndex > -1 && priceData[SimFinIdIndex] != null ? ((JsonElement)priceData[SimFinIdIndex]).GetInt32() : null,
                    Ticker = TickerIndex > -1 && priceData[TickerIndex] != null ? ((JsonElement)priceData[TickerIndex]).GetString() : null,
                    Date = DateIndex > -1 && priceData[DateIndex] != null ? ((JsonElement)priceData[DateIndex]).GetDateTime() : null,
                    Open = OpenIndex > -1 && priceData[OpenIndex] != null ? ((JsonElement)priceData[OpenIndex]).GetDecimal() : null,
                    High = HighIndex > -1 && priceData[HighIndex] != null ? ((JsonElement)priceData[HighIndex]).GetDecimal() : null,
                    Low = LowIndex > -1 && priceData[LowIndex] != null ? ((JsonElement)priceData[LowIndex]).GetDecimal() : null,
                    Close = CloseIndex > -1 && priceData[CloseIndex] != null ? ((JsonElement)priceData[CloseIndex]).GetDecimal() : null,
                    AdjClose = AdjCloseIndex > -1 && priceData[AdjCloseIndex] != null ? ((JsonElement)priceData[AdjCloseIndex]).GetDecimal() : null,
                    Volume = VolumeIndex > -1 && priceData[VolumeIndex] != null ? ((JsonElement)priceData[VolumeIndex]).GetDecimal() : null,
                    Dividend = DividendIndex > -1 && priceData[DividendIndex] != null ? ((JsonElement)priceData[DividendIndex]).GetDecimal() : null,
                    CommonSharesOutstanding = CommonSharesOutstandingIndex > -1 && priceData[CommonSharesOutstandingIndex] != null ? ((JsonElement)priceData[CommonSharesOutstandingIndex]).GetDecimal() : null,
                    MarketCap = MarketCapIndex > -1 && priceData[MarketCapIndex] != null ? ((JsonElement)priceData[MarketCapIndex]).GetDecimal() : null,
                    PricetoEarningsRatioquarterly = PricetoEarningsRatioquarterlyIndex > -1 && priceData[PricetoEarningsRatioquarterlyIndex] != null ? ((JsonElement)priceData[PricetoEarningsRatioquarterlyIndex]).GetDecimal() : null,
                    PricetoEarningsRatiottm = PricetoEarningsRatiottmIndex > -1 && priceData[PricetoEarningsRatiottmIndex] != null ? ((JsonElement)priceData[PricetoEarningsRatiottmIndex]).GetDecimal() : null,
                    PricetoSalesRatioquarterly = PricetoSalesRatioquarterlyIndex > -1 && priceData[PricetoSalesRatioquarterlyIndex] != null ? ((JsonElement)priceData[PricetoSalesRatioquarterlyIndex]).GetDecimal() : null,
                    PricetoSalesRatiottm = PricetoSalesRatiottmIndex > -1 && priceData[PricetoSalesRatiottmIndex] != null ? ((JsonElement)priceData[PricetoSalesRatiottmIndex]).GetDecimal() : null,
                    PricetoBookValuettm = PricetoBookValuettmIndex > -1 && priceData[PricetoBookValuettmIndex] != null ? ((JsonElement)priceData[PricetoBookValuettmIndex]).GetDecimal() : null,
                    PricetoFreeCashFlowquarterly = PricetoFreeCashFlowquarterlyIndex > -1 && priceData[PricetoFreeCashFlowquarterlyIndex] != null ? ((JsonElement)priceData[PricetoFreeCashFlowquarterlyIndex]).GetDecimal() : null,
                    PricetoFreeCashFlowttm = PricetoFreeCashFlowttmIndex > -1 && priceData[PricetoFreeCashFlowttmIndex] != null ? ((JsonElement)priceData[PricetoFreeCashFlowttmIndex]).GetDecimal() : null,
                    EnterpriseValuettm = EnterpriseValuettmIndex > -1 && priceData[EnterpriseValuettmIndex] != null ? ((JsonElement)priceData[EnterpriseValuettmIndex]).GetDecimal() : null,
                    EVEBITDAttm = EVEBITDAttmIndex > -1 && priceData[EVEBITDAttmIndex] != null ? ((JsonElement)priceData[EVEBITDAttmIndex]).GetDecimal() : null,
                    EVSalesttm = EVSalesttmIndex > -1 && priceData[EVSalesttmIndex] != null ? ((JsonElement)priceData[EVSalesttmIndex]).GetDecimal() : null,
                    EVFCFttm = EVFCFttmIndex > -1 && priceData[EVFCFttmIndex] != null ? ((JsonElement)priceData[EVFCFttmIndex]).GetDecimal() : null,
                    BooktoMarketValuettm = BooktoMarketValuettmIndex > -1 && priceData[BooktoMarketValuettmIndex] != null ? ((JsonElement)priceData[BooktoMarketValuettmIndex]).GetDecimal() : null,
                    OperatingIncomeEVttm = OperatingIncomeEVttmIndex > -1 && priceData[OperatingIncomeEVttmIndex] != null ? ((JsonElement)priceData[OperatingIncomeEVttmIndex]).GetDecimal() : null
                });
            }

            Prices = prices;
        }
    }

	public class SimFinSharePriceEntry
    {
        public int? SimFinId { get; set; }
        public string? Ticker { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Open { get; set; }
        public decimal? High { get; set; }
        public decimal? Low { get; set; }
        public decimal? Close { get; set; }
        public decimal? AdjClose { get; set; }
        public decimal? Volume { get; set; }
        public decimal? Dividend { get; set; }
        public decimal? CommonSharesOutstanding { get; set; }
        public decimal? MarketCap { get; set; }
        public decimal? PricetoEarningsRatioquarterly { get; set; }
        public decimal? PricetoEarningsRatiottm { get; set; }
        public decimal? PricetoSalesRatioquarterly { get; set; }
        public decimal? PricetoSalesRatiottm { get; set; }
        public decimal? PricetoBookValuettm { get; set; }
        public decimal? PricetoFreeCashFlowquarterly { get; set; }
        public decimal? PricetoFreeCashFlowttm { get; set; }
        public decimal? EnterpriseValuettm { get; set; }
        public decimal? EVEBITDAttm { get; set; }
        public decimal? EVSalesttm { get; set; }
        public decimal? EVFCFttm { get; set; }
        public decimal? BooktoMarketValuettm { get; set; }
        public decimal? OperatingIncomeEVttm { get; set; }

        public SimFinSharePriceEntry()
        {

        }

        public SimFinSharePriceEntry(IList<string> columns, IList<object> dataPoints)
        {
            SetProperties(columns, dataPoints);
        }

        private void SetProperties(IList<string> columns, IList<object> dataPoints)
        {
            foreach (var colName in columns)
            {
                var colNameAsProperty = colName.Replace(" ", "").Replace(",", "").Replace("/", "").Replace("&", "").Replace("(", "").Replace(")", "").Replace(".", "").Replace("-", "");

                var prop = this.GetType().GetProperty(colNameAsProperty);
                if (prop != null)
                {
                    if (dataPoints[columns.IndexOf(colName)] == null)
                        continue;

                    if (prop.PropertyType == typeof(int))
                        prop.SetValue(this, ((JsonElement)dataPoints[columns.IndexOf(colName)]).GetInt32());
                    if (prop.PropertyType == typeof(string))
                        prop.SetValue(this, ((JsonElement)dataPoints[columns.IndexOf(colName)]).GetString());
                    if (prop.PropertyType == typeof(bool))
                        prop.SetValue(this, ((JsonElement)dataPoints[columns.IndexOf(colName)]).GetBoolean());
                    if (prop.PropertyType == typeof(DateTime))
                        prop.SetValue(this, ((JsonElement)dataPoints[columns.IndexOf(colName)]).GetDateTime());
                    if (prop.PropertyType == typeof(decimal))
                        if (((JsonElement)dataPoints[columns.IndexOf(colName)]).ValueKind == JsonValueKind.String)
                            prop.SetValue(this, decimal.Parse(((JsonElement)dataPoints[columns.IndexOf(colName)]).GetString()));
                        else if (((JsonElement)dataPoints[columns.IndexOf(colName)]).ValueKind == JsonValueKind.Number)
                            prop.SetValue(this, ((JsonElement)dataPoints[columns.IndexOf(colName)]).GetDecimal());

                }
                else
                {
                    Debug.WriteLine("Watch out: Column '" + colName + "' not found in properties. Maybe you missed this?");
                }
            }
        }

        public StockPrice ToStockPrice()
        {
            var stockPrice = new StockPrice();
            stockPrice.Date = (DateTime)Date;
            stockPrice.Open = Open;
            stockPrice.High = High;
            stockPrice.Low = Low;
            stockPrice.Close = Close;
            stockPrice.AdjClose = AdjClose;
            stockPrice.Volume = Volume;
            stockPrice.Dividend = Dividend;

            return stockPrice;
        }
    }
}

