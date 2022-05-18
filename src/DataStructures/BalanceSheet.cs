using System;
namespace DataStructures
{
    public class BalanceSheet : FinancialStatement
    {
        // ANLAGEVERMÖGEN
        // 0 - Immaterielle Vermögensgegenstände, Sachanlagen, Finanzanlagen

        // UMLAUFVERMÖGEN
        // 1 - Vorräte
        // 2 - Sonstiges Umlaufvermögen und Rechnungsabgrenzungsposten

        // FREMDKAPITAL
        // 3 - Rückstellungen, Verbindlichkeiten und Rechnungsabgrenzungen

        // ERFOLGSKONTEN
        // Betriebliche Erfolgskonten
        //      4 - Betriebliche Erträge
        //      5 - Materialaufwand und Aufwendungen für bezogene Leistungen
        //      6 - Personalaufwand
        //      7 - Abschreibungen und sonstige betriebliche Aufwendungen
        // Finanzerträge und Finanzaufwendungen
        //      8 - Finanzerträge und Finanzaufwendungen
        // Steuern vom Einkommen und vom Ertrag
        //      8 - Steuern vom Einkommen und vom Ertrag
        // Rücklagenbewegung
        //      8 Rücklagenbewegung

        // EIGENKAPITAL
        // 9 - Eigenkapital

        /// <summary>
        /// 2 - Zahlungsmittel
        /// Kassa, Bank, etc.
        /// </summary>
        public decimal CashAndCashEquivalents { get; set; }
        /// <summary>
        /// 2 - Kurzfristige Anlagen (Investments)
        /// Anteile an Kaptialgesellschaften im Umlaufvermögen
        /// Gläubigerpapiere des Umlaufvermögens
        /// </summary>
        public decimal ShortTermInvestments { get; set; }
        /// <summary>
        /// 2 - Zahlungsmittel + Kurzfristige Anlagen
        /// </summary>
        public decimal CashAndShortTermInvestments { get; set; }
        /// <summary>
        /// 2 - Forderungen
        /// Forderungen aus Lieferungen und Leistungen
        /// Sonstige Forderungen
        /// </summary>
        public decimal Receivables { get; set; }
        /// <summary>
        /// 1 - Vorräte
        /// </summary>
        public decimal Inventories { get; set; }
        /// <summary>
        /// UMLAUFVERMÖGEN
        /// </summary>
        public decimal TotalCurrentAssets { get; set; }
        /// <summary>
        /// 0 - Sachanlagen (Immobilien, Maschinen, Ausrüstung, etc.)
        /// </summary>
        public decimal PropertyPlantAndEquipmentNet { get; set; }
        /// <summary>
        /// 0 - Firmenwert und sonstige immaterielle Werte
        /// </summary>
        public decimal GoodwillAndIntangibleAssets { get; set; }
        /// <summary>
        /// 0 - Langfristige Anlagen (Investments)
        /// Finanzanlagen
        /// </summary>
        public decimal LongTermInvestments { get; set; }
        /// <summary>
        /// Latente Steuern (Mehr-Weniger-Rechnung)
        /// </summary>
        public decimal TaxAssets { get; set; }
        /// <summary>
        /// 0 - ANLAGEVERMÖGEN
        /// </summary>
        public decimal TotalNonCurrentAssets { get; set; }
        /// <summary>
        /// GESAMTVERMÖGEN (Aktiva)
        /// </summary>
        public decimal TotalAssets { get; set; }
        /// <summary>
        /// 3 - Lieferverbindlichkeiten
        /// </summary>
        public decimal Payables { get; set; }
        /// <summary>
        /// 3 - Kurzfristige Schulden
        /// Verbindlichkeiten gegenüber Kreditinstituten
        /// (Kontoüberzug)
        /// </summary>
        public decimal ShortTermDebt { get; set; }
        /// <summary>
        /// 3 - Kurzfristige Verbindlichkeiten
        /// </summary>
        public decimal TotalCurrentLiabilities { get; set; }
        /// <summary>
        /// 3 - Langfristige Schulden
        /// </summary>
        public decimal LongTermDebt { get; set; }
        /// <summary>
        /// 3 - Gesamtverschuldung
        /// </summary>
        public decimal TotalDebt { get; set; }
        /// <summary>
        /// 2&3 - Rechnungsabgrenzungsposten
        /// Aktive/Passive Rechnungsabgrenzung
        /// </summary>
        public decimal DeferredRevenue { get; set; }
        /// <summary>
        /// 3 - Steuerrückstellungen (Steuerschulden)
        /// </summary>
        public decimal TaxLiabilities { get; set; }
        /// <summary>
        /// ????
        /// </summary>
        public decimal DepositLiabilities { get; set; }
        /// <summary>
        /// 3 - Langfristige Verbindlichkeiten
        /// </summary>
        public decimal TotalNonCurrentLiabilities { get; set; }
        /// <summary>
        /// 3 - Gesamtverbindlichkeiten 
        /// </summary>
        public decimal TotalLiabilities { get; set; }
        /// <summary>
        /// ?????
        /// </summary>
        public decimal OtherComprehensiveIncome { get; set; }
        /// <summary>
        /// 9 - Gewinnrücklagen 
        /// (Deficit)
        /// </summary>
        public decimal RetainedEarnings { get; set; }

        public decimal ShareCapitalAndAdditionalPaidInCapital { get; set; }

        public decimal TreasuryStock { get; set; }

        /// <summary>
        /// 9 - Eigenkapital
        /// </summary>
        public decimal TotalShareholdersEquity { get; set; }
        /// <summary>
        /// 0 - Investments
        /// </summary>
        public decimal Investments { get; set; }
        /// <summary>
        /// Netto Verschuldung
        /// </summary>
        public decimal NetDebt { get; set; }
        /// <summary>
        /// Sonstige Aktiva
        /// </summary>
        public decimal OtherAssets { get; set; }
        /// <summary>
        /// Sonstige Passiva
        /// </summary>
        public decimal OtherLiabilites { get; set; }

        public decimal TotalEquity { get; set; }

        public decimal TotalLiabilitiesAndEquity { get; set; }

        /// <summary>
        /// Nettoumlaufvermögen
        /// NWC
        /// = Receivables + Inventories - Payables
        /// </summary>
        public decimal NetWorkingCapital { get { return Receivables + Inventories - Payables; } }
        /// <summary>
        /// Capital Employed
        /// = Total noncurrent Assets + Receivables + Inventories - Payables
        /// </summary>
        public decimal CapitalEmployed { get { return TotalNonCurrentAssets + NetWorkingCapital; } }

        public BalanceSheet()
        {
            
        }

        public override string ToString()
        {
            return base.ToString() + "Assets: " + TotalAssets + ", Liabilities: " + TotalLiabilities;
        }
    }
}
