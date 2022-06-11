using System;
namespace DataImport.APIClients.SimFin;

public class SimFinFinancialStatementCollection
{
    public List<SimFinProfitLossStatement> ProfitLossStatements { get; set; }
    public List<SimFinBalanceSheet> BalanceSheets { get; set; }
    public List<SimFinCashFlowStatement> CashFlows { get; set; }
    public List<SimFinDerivedFigures> DerivedFigures { get; set; }

    public SimFinFinancialStatementCollection(SimFinFundamentalsBasicCompanyResponse companyFundamentals)
        : this(companyFundamentals.columns, companyFundamentals.data)
    {
        
    }
    public SimFinFinancialStatementCollection(IList<string> columns, IList<IList<object>> data)
    {
        ProfitLossStatements = new List<SimFinProfitLossStatement>();
        BalanceSheets = new List<SimFinBalanceSheet>();
        CashFlows = new List<SimFinCashFlowStatement>();
        DerivedFigures = new List<SimFinDerivedFigures>();

        foreach (var rawStatements in data)
        {
            if (columns.Contains("Revenue"))
                ProfitLossStatements.Add(new SimFinProfitLossStatement(columns, rawStatements));
            if (columns.Contains("Cash, Cash Equivalents & Short Term Investments"))
                BalanceSheets.Add(new SimFinBalanceSheet(columns, rawStatements));
            if (columns.Contains("Net Income/Starting Line"))
                CashFlows.Add(new SimFinCashFlowStatement(columns, rawStatements));
            if (columns.Contains("Return on Equity"))
                DerivedFigures.Add(new SimFinDerivedFigures(columns, rawStatements));
        }
    }

    public void Merge(SimFinFinancialStatementCollection otherCollection)
    {
        if (ProfitLossStatements == null || ProfitLossStatements.Count() == 0)
            ProfitLossStatements = otherCollection.ProfitLossStatements;
        if (BalanceSheets == null || BalanceSheets.Count() == 0)
            BalanceSheets = otherCollection.BalanceSheets;
        if (CashFlows == null || CashFlows.Count() == 0)
            CashFlows = otherCollection.CashFlows;
        if (DerivedFigures == null || DerivedFigures.Count() == 0)
            DerivedFigures = otherCollection.DerivedFigures;

        if (ProfitLossStatements != null && ProfitLossStatements.Count() > 0 && otherCollection.ProfitLossStatements != null && otherCollection.ProfitLossStatements.Count() > 0)
        {
            foreach (var otherPl in otherCollection.ProfitLossStatements)
            {
                if (ProfitLossStatements.FirstOrDefault(e => e.FiscalPeriod == otherPl.FiscalPeriod && e.FiscalYear == otherPl.FiscalYear) == null)
                    ProfitLossStatements.Add(otherPl);
            }
        }
        if (BalanceSheets != null && BalanceSheets.Count() > 0 && otherCollection.ProfitLossStatements != null && otherCollection.BalanceSheets.Count() > 0)
        {
            foreach (var otherBs in otherCollection.BalanceSheets)
            {
                if (BalanceSheets.FirstOrDefault(e => e.FiscalPeriod == otherBs.FiscalPeriod && e.FiscalYear == otherBs.FiscalYear) == null)
                    BalanceSheets.Add(otherBs);
            }
        }
        if (CashFlows != null && CashFlows.Count() > 0 && otherCollection.CashFlows != null && otherCollection.CashFlows.Count() > 0)
        {
            foreach (var otherCf in otherCollection.CashFlows)
            {
                if (CashFlows.FirstOrDefault(e => e.FiscalPeriod == otherCf.FiscalPeriod && e.FiscalYear == otherCf.FiscalYear) == null)
                    CashFlows.Add(otherCf);
            }
        }
        if (DerivedFigures != null && DerivedFigures.Count() > 0 && otherCollection.DerivedFigures != null && otherCollection.DerivedFigures.Count() > 0)
        {
            foreach (var otherDerived in otherCollection.DerivedFigures)
            {
                if (DerivedFigures.FirstOrDefault(e => e.FiscalPeriod == otherDerived.FiscalPeriod && e.FiscalYear == otherDerived.FiscalYear) == null)
                    DerivedFigures.Add(otherDerived);
            }
        }
    }
}

