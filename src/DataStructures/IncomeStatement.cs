using System;
namespace DataStructures
{
    public class IncomeStatement : FinancialStatement
    {
        public virtual int? ReportID { get; set; }
        public virtual Filing Report { get; set; }

        /// <summary>
        /// Umsatzerlöse, Sales
        /// </summary>
        public virtual decimal Revenue { get; set; }
        /// <summary>
        /// Umsatzkosten, Cost of Sales
        /// Herstellungskosten, Distributionskosten, Servicekosten
        /// </summary>
        public virtual decimal CostOfRevenue { get; set; }
        /// <summary>
        /// Rohertrag, Rohgewinn, Bruttoertrag, Bruttomarge
        /// = Revenue - CostOfRevenue
        /// </summary>
        public virtual decimal GrossProfit { get; set; }
        /// <summary>
        /// Umsatzkostenquote, Cost of sales ratio
        /// = Cost of Revenue / Revenue
        /// </summary>
        public virtual decimal CostOfRevenueRatio => CostOfRevenue / Revenue;

        /// <summary>
        /// Kosten für Forschung und Entwicklung
        /// </summary>
        public virtual decimal ResearchAndDevelopmentExpenses { get; set; }
        /// <summary>
        /// Verwaltungs- und Vertriebskosten
        /// </summary>
        public virtual decimal SellingGeneralAndAdministrativeExpenses { get; set; }
        /// <summary>
        /// Betriebsaufwand
        /// = ResearchAndDevelopmentExpenses + SellingGeneralAndAdministrativeExpenses
        /// </summary>
        public virtual decimal OperatingExpenses { get; set; }
        /// <summary>
        /// Abschreibungen
        /// </summary>
        public virtual decimal DepreciationAmortization { get; set; }
        /// <summary>
        /// Verwaltungs- und Vertriebskostenquote
        /// = SellingGeneralAndAdministrativeExpenses / Revenue
        /// </summary>
        public virtual decimal SellingExpenseRatio => SellingGeneralAndAdministrativeExpenses / Revenue;
        /// <summary>
        /// Forschungs- und Entwicklungskostenquote
        /// = ResearchAndDevelopmentExpenses / Revenue
        /// </summary>
        public virtual decimal ResearchDevelopmentExpenseRatio => ResearchAndDevelopmentExpenses / Revenue;

        /// <summary>
        /// Betriebsergebnis
        /// Earnings before Interest and Taxes (EBIT)
        /// = GrossProfit - OperatingExpenses
        /// </summary>
        public virtual decimal OperatingIncome { get; set; }
        /// <summary>
        /// Einkommen aus nicht-operativer Tätigkeit
        /// </summary>
        public virtual decimal NonOperatingIncome { get; set; }
        /// <summary>
        /// Zinsaufwendungen
        /// </summary>
        public virtual decimal InterestExpense { get; set; }
        /// <summary>
        /// Ergebnis vor Steuern (NOPLAT?)
        /// EBT
        /// = NetIncome + ExtraordinaryGains + IncomeTaxExpense
        /// </summary>
        public virtual decimal EarningsBeforeTaxAdj { get; set; }
        /// <summary>
        /// Gewinn/Verlust der nur einmal auftritt und nicht wieder erwartet werden kann.
        /// </summary>
        public virtual decimal ExtraordinaryGains { get; set; }
        /// <summary>
        /// Ergebnis vor Steuern (NOPLAT?)
        /// EBT
        /// = NetIncome + IncomeTaxExpense
        /// </summary>
        public virtual decimal EarningsBeforeTax { get; set; }

        /// <summary>
        /// Ertragsteueraufwand
        /// </summary>
        public virtual decimal IncomeTaxExpense { get; set; }
        /// <summary>
        /// Steuerquote
        /// = IncomeTaxExpense / EBT
        /// </summary>
        public virtual decimal TaxRate => IncomeTaxExpense / EarningsBeforeTax;


        /// <summary>
        /// Minderheitsanteile??
        /// </summary>
        public virtual decimal NetIncomeNonControllingInt { get; set; }
        /// <summary>
        /// Erträge aus aufgegebenen Geschäftsbereichen
        /// </summary>
        public virtual decimal NetIncomeDiscontinuedOps { get; set; }
        /// <summary>
        /// Nettoeinkommen
        /// </summary>
        public virtual decimal NetIncome { get; set; }
        /// <summary>
        /// Dividenden an Vorzugsaktien
        /// </summary>
        public virtual decimal PreferredDividends { get; set; }
        /// <summary>
        /// Nettoeinkommen für Stammaktien
        /// = NetIncome - PreferredDividends
        /// </summary>
        public virtual decimal NetIncomeCom { get; set; }
        /// <summary>
        /// Earnings Per Share
        /// = NetIncomeCom / WeightedAverageShsOut
        /// </summary>
        public virtual decimal EPS { get; set; }
        /// <summary>
        /// Earnings Per Share Diluted
        /// = NetIncomeCom / WeightedAverageShsOutDiluted
        /// </summary>
        public virtual decimal EPSDiluted { get; set; }
        /// <summary>
        /// Gewichtete durchschnittliche Anzahl an ausgegebenen Aktien
        /// </summary>
        public virtual decimal WeightedAverageShsOut { get; set; }
        /// <summary>
        /// Gewichtete durchschnittliche Anzahl an ausgegebenen Aktien (verwässert)
        /// </summary>
        public virtual decimal WeightedAverageShsOutDiluted { get; set; }
        /// <summary>
        /// Dividende pro Aktie
        /// </summary>
        public virtual decimal DividendPerShare { get; set; }
        /// <summary>
        /// = GrossProfit / Revenue
        /// </summary>
        public virtual decimal GrossMargin { get; set; }
        /// <summary>
        /// = EBITDA / Revenue
        /// </summary>
        public virtual decimal EBITDAMargin { get; set; }
        /// <summary>
        /// EBIT / Revenue
        /// </summary>
        public virtual decimal EBITMargin { get; set; }
        /// <summary>
        /// NetIncome (?) / Revenue (Was ist der Unterschied zu NetProfitMargin?)
        /// </summary>
        public virtual decimal ProfitMargin { get; set; }
        /// <summary>
        /// FreeCashFlow / Revenue
        /// </summary>
        public virtual decimal FreeCashFlowMargin { get; set; }
        /// <summary>
        /// Ergebnis vor Zinsen, Steuern, Abschreibungen auf Sachanlagen und Abschreibungen auf immaterielle Vermögensgegenstände
        /// </summary>
        public virtual decimal EBITDA { get; set; }
        /// <summary>
        /// Ergebnis vor Zinsen und Steuern
        /// </summary>
        public virtual decimal EBIT { get; set; }
        /// <summary>
        /// Konzernergebnis
        /// </summary>
        public virtual decimal ConsolidatedIncome { get; set; }
        /// <summary>
        /// = EBT / Revenue
        /// </summary>
        public virtual decimal EarningsBeforeTaxMargin { get; set; }
        /// <summary>
        /// = ConsolidatedIncome / Revenue (Ist das vielleicht der Unterschied zu ProfitMargin?)
        /// </summary>
        public virtual decimal NetProfitMargin { get; set; }

        public override string ToString()
        {
            return base.ToString() + "Sales: " + Revenue + ", Net Income: " + NetIncome;
        }
    }
}
