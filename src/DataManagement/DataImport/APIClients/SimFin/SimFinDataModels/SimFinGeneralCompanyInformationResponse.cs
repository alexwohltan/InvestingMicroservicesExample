using System;
namespace DataImport.APIClients.SimFin;

	public class SimFinGeneralCompanyInformationResponse : List<SimFinBasicDataView>
	{
    public IEnumerable<SimFinCompanyGeneralInfoEntry> CompanyInfos => this.Select(e => new SimFinCompanyGeneralInfoEntry(e.data,
        e["SimFinId"],
        e["Ticker"],
        e["Company Name"],
        e["IndustryId"],
        e["Month FY End"],
        e["Number Employees"],
        e["Business Summary"]));
}

public class SimFinCompanyGeneralInfoEntry
{
    public int SimFinId { get; set; }
    public string? Ticker { get; set; }
    public string? CompanyName { get; set; }
    public int IndustryId { get; set; }
    public int MonthFyEnd { get; set; }
    public int NumberEmployees { get; set; }
    public string? BusinessSummary { get; set; }

    public SimFinCompanyGeneralInfoEntry(IList<object> dataPoints,
        int SimFinIdIndex,
        int TickerIndex,
        int CompanyNameIndex,
        int IndustryIdIndex,
        int MonthFyEndIndex,
        int NumberEmployeesIndex,
        int BusinessSummaryIndex)
    {

        SimFinId = ((JsonElement)dataPoints[SimFinIdIndex]).GetInt32();
        Ticker = ((JsonElement)dataPoints[TickerIndex]).GetString();
        CompanyName = ((JsonElement)dataPoints[CompanyNameIndex]).GetString();
        IndustryId = ((JsonElement)dataPoints[IndustryIdIndex]).GetInt32();
        MonthFyEnd = ((JsonElement)dataPoints[MonthFyEndIndex]).GetInt32();
        NumberEmployees = ((JsonElement)dataPoints[NumberEmployeesIndex]).GetInt32();

        if(BusinessSummaryIndex != -1)
            BusinessSummary = ((JsonElement)dataPoints[BusinessSummaryIndex]).GetString();
    }
}

