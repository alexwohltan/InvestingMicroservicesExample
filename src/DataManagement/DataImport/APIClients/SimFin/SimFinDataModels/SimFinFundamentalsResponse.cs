using System;
namespace DataImport.APIClients.SimFin;

public class SimFinFundamentalsResponse : List<SimFinFundamentalsBasicCompanyResponse>
{
    public IEnumerable<SimFinFundamentalsCompany> FundamentalsCompanies => this.Select(e => new SimFinFundamentalsCompany(e));
}

public class SimFinFundamentalsBasicCompanyResponse
{
    public bool found { get; set; }
    public string? currency { get; set; }
    public IList<string>? columns { get; set; }
    public IList<IList<object>>? data { get; set; }

    public object this[int row, int column]
    {
        get { return data[row][column]; }
        set { data[row][column] = value; }
    }
    public int this[string columnName]
    {
        get { return columns.IndexOf(columnName); }
    }
    public IEnumerable<object> this[int columnIndex]
    {
        get
        {
            return data.Select(e => e[columnIndex]);

        }
    }
}

