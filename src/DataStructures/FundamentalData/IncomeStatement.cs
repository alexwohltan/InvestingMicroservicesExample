using System;
using System.Text.Json.Serialization;

namespace DataStructures.FundamentalData
{
    public class IncomeStatement : FinancialStatement
    {
        /// <summary>
        /// Umsatzerlöse, Sales
        /// </summary>
        public virtual decimal Revenue { get; set; }
        public virtual decimal SalesServicesRevenue { get; set; }
        public virtual decimal FinancingRevenue { get; set; }
        public virtual decimal OtherRevenue { get; set; }
        /// <summary>
        /// Umsatzkosten, Cost of Sales
        /// Herstellungskosten, Distributionskosten, Servicekosten
        /// </summary>
        public virtual decimal CostofRevenue { get; set; }
        public virtual decimal CostofGoodsServices { get; set; }
        public virtual decimal CostofFinancingRevenue { get; set; }
        public virtual decimal CostofOtherRevenue { get; set; }
        /// <summary>
        /// Rohertrag, Rohgewinn, Bruttoertrag, Bruttomarge
        /// = Revenue - CostOfRevenue
        /// </summary>
        public virtual decimal GrossProfit { get; set; }

        public virtual decimal OtherOperatingIncome { get; set; }

        /// <summary>
        /// Umsatzkostenquote, Cost of sales ratio
        /// = Cost of Revenue / Revenue
        /// </summary>
        [JsonIgnore]
        public virtual decimal CostOfRevenueRatio => CostofRevenue / Revenue;

        /// <summary>
        /// Kosten für Forschung und Entwicklung
        /// </summary>
        public virtual decimal ResearchDevelopment { get; set; }
        /// <summary>
        /// Verwaltungs- und Vertriebskosten
        /// </summary>
        public virtual decimal SellingGeneralAdministrative { get; set; }
        public virtual decimal SellingMarketing { get; set; }
        public virtual decimal GeneralAdministrative { get; set; }
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
        [JsonIgnore]
        public virtual decimal SellingExpenseRatio => SellingGeneralAdministrative / Revenue;
        /// <summary>
        /// Forschungs- und Entwicklungskostenquote
        /// = ResearchAndDevelopmentExpenses / Revenue
        /// </summary>
        [JsonIgnore]
        public virtual decimal ResearchDevelopmentExpenseRatio => ResearchDevelopment / Revenue;

        public virtual decimal ProvisionforDoubtfulAccounts { get; set; }
        public virtual decimal OtherOperatingExpenses { get; set; }

        /// <summary>
        /// Betriebsergebnis
        /// Earnings before Interest and Taxes (EBIT)
        /// = GrossProfit - OperatingExpenses
        /// </summary>
        public virtual decimal OperatingIncomeLoss { get; set; }
        /// <summary>
        /// Einkommen aus nicht-operativer Tätigkeit
        /// </summary>
        public virtual decimal NonOperatingIncomeLoss { get; set; }
        /// <summary>
        /// Zinsaufwendungen
        /// </summary>
        public virtual decimal InterestExpense { get; set; }
        public virtual decimal InterestExpenseNet { get; set; }
        public virtual decimal InterestIncome { get; set; }

        public virtual decimal OtherInvestmentIncomeLoss { get; set; }
        public virtual decimal ForeignExchangeGainLoss { get; set; }
        public virtual decimal IncomeLossfromAffiliates { get; set; }
        public virtual decimal OtherNonOperatingIncomeLoss { get; set; }

        /// <summary>
        /// Ergebnis vor Steuern (NOPLAT?)
        /// EBT
        /// = NetIncome + ExtraordinaryGains + IncomeTaxExpense
        /// </summary>
        public virtual decimal PretaxIncomeLossAdj { get; set; }
        /// <summary>
        /// Gewinn/Verlust der nur einmal auftritt und nicht wieder erwartet werden kann.
        /// </summary>
        public virtual decimal AbnormalGainsLosses { get; set; }

        public virtual decimal AcquiredInProcessRD { get; set; }
        public virtual decimal MergerAcquisitionExpense { get; set; }
        public virtual decimal AbnormalDerivatives { get; set; }
        public virtual decimal DisposalofAssets { get; set; }
        public virtual decimal EarlyExtinguishmentofDebt { get; set; }
        public virtual decimal AssetWriteDown { get; set; }
        public virtual decimal ImpairmentofGoodwillIntangibles { get; set; }
        public virtual decimal SaleofBusiness { get; set; }
        public virtual decimal LegalSettlement { get; set; }
        public virtual decimal RestructuringCharges { get; set; }
        public virtual decimal SaleofInvestmentsUnrealizedInvestments { get; set; }
        public virtual decimal InsuranceSettlement { get; set; }
        public virtual decimal OtherAbnormalItems { get; set; }

        /// <summary>
        /// Ergebnis vor Steuern (NOPLAT?)
        /// EBT
        /// = NetIncome + IncomeTaxExpense
        /// </summary>
        public virtual decimal PretaxIncomeLoss { get; set; }

        /// <summary>
        /// Ertragsteueraufwand
        /// </summary>
        public virtual decimal IncomeTaxExpenseBenefitNet { get; set; }
        /// <summary>
        /// Steuerquote
        /// = IncomeTaxExpense / EBT
        /// </summary>
        [JsonIgnore]
        public virtual decimal TaxRate => IncomeTaxExpenseBenefitNet / PretaxIncomeLoss;

        public virtual decimal CurrentIncomeTax { get; set; }
        public virtual decimal DeferredIncomeTax { get; set; }
        public virtual decimal TaxAllowanceCredit { get; set; }
        public virtual decimal IncomeLossfromAffiliatesNetofTaxes { get; set; }
        public virtual decimal IncomeLossfromContinuingOperations { get; set; }
        public virtual decimal NetExtraordinaryGainsLosses { get; set; }
        public virtual decimal AccountingChargesOther { get; set; }
        public virtual decimal IncomeLossInclMinorityInterest { get; set; }
        public virtual decimal MinorityInterest { get; set; }

        /// <summary>
        /// Minderheitsanteile??
        /// </summary>
        public virtual decimal NetIncomeNonControllingInt { get; set; } // not in SimFin
        /// <summary>
        /// Erträge aus aufgegebenen Geschäftsbereichen
        /// </summary>
        public virtual decimal DiscontinuedOperations { get; set; }
        /// <summary>
        /// Nettoeinkommen
        /// </summary>
        public virtual decimal NetIncome { get; set; }
        /// <summary>
        /// Dividenden an Vorzugsaktien
        /// </summary>
        public virtual decimal PreferredDividends { get; set; } // not in SimFin

        public virtual decimal OtherAdjustments { get; set; }

        /// <summary>
        /// Nettoeinkommen für Stammaktien
        /// = NetIncome - PreferredDividends
        /// </summary>
        public virtual decimal NetIncomeCommon { get; set; }
        /// <summary>
        /// Earnings Per Share
        /// = NetIncomeCom / WeightedAverageShsOut
        /// </summary>
        public virtual decimal? EPS { get; set; }
        /// <summary>
        /// Earnings Per Share Diluted
        /// = NetIncomeCom / WeightedAverageShsOutDiluted
        /// </summary>
        public virtual decimal? EPSDiluted { get; set; }
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
        public virtual decimal DividendPerShare { get; set; } // not in SimFin
        /// <summary>
        /// = GrossProfit / Revenue
        /// </summary>
        public virtual decimal? GrossMargin { get; set; }
        /// <summary>
        /// = EBITDA / Revenue
        /// </summary>
        public virtual decimal? EBITDAMargin { get; set; }
        /// <summary>
        /// EBIT / Revenue
        /// </summary>
        public virtual decimal? EBITMargin { get; set; }
        /// <summary>
        /// NetIncome (?) / Revenue (Was ist der Unterschied zu NetProfitMargin?)
        /// </summary>
        public virtual decimal ProfitMargin { get; set; } // not in SimFin
        /// <summary>
        /// FreeCashFlow / Revenue
        /// </summary>
        public virtual decimal FreeCashFlowMargin { get; set; } // not in SimFin
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
        public virtual decimal ConsolidatedIncome { get; set; } // not in SimFin
        /// <summary>
        /// = EBT / Revenue
        /// </summary>
        public virtual decimal? EarningsBeforeTaxMargin { get; set; }
        /// <summary>
        /// = ConsolidatedIncome / Revenue (Ist das vielleicht der Unterschied zu ProfitMargin?)
        /// </summary>
        public virtual decimal NetProfitMargin { get; set; } // not in SimFin

        public override string ToString()
        {
            return base.ToString() + "Sales: " + Revenue + ", Net Income: " + NetIncome;
        }
    }
}
