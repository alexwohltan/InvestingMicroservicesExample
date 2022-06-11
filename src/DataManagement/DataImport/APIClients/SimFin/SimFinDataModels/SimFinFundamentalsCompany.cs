using System;
namespace DataImport.APIClients.SimFin;

public class SimFinFundamentalsCompany
{
    public bool found { get; set; }
    public string? currency { get; set; }
    public SimFinFinancialStatementCollection? StatementCollection { get; set; }

    public SimFinFundamentalsCompany()
    {

    }
    public SimFinFundamentalsCompany(SimFinFundamentalsBasicCompanyResponse simFinResponse)
    {
        found = simFinResponse.found;
        if(found == true)
        {
            currency = simFinResponse.currency;
            StatementCollection = new SimFinFinancialStatementCollection(simFinResponse.columns, simFinResponse.data);
        }
    }

    public void Merge(SimFinFinancialStatementCollection otherCollection)
    {
        if (StatementCollection != null)
            StatementCollection.Merge(otherCollection);
        else
            StatementCollection = otherCollection;
    }
    public void Merge(SimFinFundamentalsCompany otherCompany)
    {
        if (otherCompany.found == true && otherCompany.StatementCollection != null)
            Merge(otherCompany.StatementCollection);
    }
}

