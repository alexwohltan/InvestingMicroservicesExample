using System;
namespace DataImport.APIClients.SimFin;

	public abstract class SimFinFinancialStatement
	{
    public int SimFinId { get; set; }
    public string? Ticker { get; set; }
    public string? FiscalPeriod { get; set; }
    public int FiscalYear { get; set; }
    public DateTime ReportDate { get; set; }
    public DateTime PublishDate { get; set; }
    public DateTime RestatedDate { get; set; }
    public string? Source { get; set; }
    public bool TTM { get; set; }
    public bool ValueCheck { get; set; }

    public SimFinFinancialStatement(IList<string> columns, IList<object> dataPoints)
    {
        SetProperties(columns, dataPoints);

        //SimFinId = ((JsonElement)dataPoints[columns.IndexOf("SimFinId")]).GetInt32();
        //Ticker = ((JsonElement)dataPoints[columns.IndexOf("Ticker")]).GetString();
        //FiscalPeriod = ((JsonElement)dataPoints[columns.IndexOf("Fiscal Period")]).GetString();
        //FiscalYear = ((JsonElement)dataPoints[columns.IndexOf("Fiscal Year")]).GetInt32();
        //ReportDate = ((JsonElement)dataPoints[columns.IndexOf("Report Date")]).GetDateTime();
        //PublishDate = ((JsonElement)dataPoints[columns.IndexOf("Publish Date")]).GetDateTime();
        //RestatedDate = ((JsonElement)dataPoints[columns.IndexOf("Restated Date")]).GetDateTime();
        //Source = ((JsonElement)dataPoints[columns.IndexOf("Source")]).GetString();
        //TTM = ((JsonElement)dataPoints[columns.IndexOf("TTM")]).GetBoolean();
        //ValueCheck = ((JsonElement)dataPoints[columns.IndexOf("Value Check")]).GetBoolean();
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
                    else if(((JsonElement)dataPoints[columns.IndexOf(colName)]).ValueKind == JsonValueKind.Number)
                        prop.SetValue(this, ((JsonElement)dataPoints[columns.IndexOf(colName)]).GetDecimal());

            }
            else
            {
                Debug.WriteLine("Watch out: Column '" + colName + "' not found in properties. Maybe you missed this?");
            }
        }
    }
}

