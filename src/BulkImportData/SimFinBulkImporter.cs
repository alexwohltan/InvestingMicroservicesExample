using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataStructures;

namespace BulkImportData
{
    public class SimFinHandler
    {
        public const string FILEPATH_BULKDATA = "data/SimFin/";

        private string[] industriesFile;

        private string[] companiesUSFile;
        private string[] companiesDEFile;

        private string[] incomeUSFileY;
        private string[] balanceUSFileY;
        private string[] cashflowUSFileY;
        private string[] incomeDEFileY;
        private string[] balanceDEFileY;
        private string[] cashflowDEFileY;

        public SimFinHandler()
        {
            industriesFile = File.ReadAllLines(FILEPATH_BULKDATA + "industries.csv");

            companiesUSFile = File.ReadAllLines(FILEPATH_BULKDATA + "us-companies.csv");
            companiesDEFile = File.ReadAllLines(FILEPATH_BULKDATA + "de-companies.csv");

            incomeUSFileY = File.ReadAllLines(FILEPATH_BULKDATA + "us-income-annual.csv");
            balanceUSFileY = File.ReadAllLines(FILEPATH_BULKDATA + "us-balance-annual.csv");
            cashflowUSFileY = File.ReadAllLines(FILEPATH_BULKDATA + "us-cashflow-annual.csv");

            incomeDEFileY = File.ReadAllLines(FILEPATH_BULKDATA + "de-income-annual.csv");
            balanceDEFileY = File.ReadAllLines(FILEPATH_BULKDATA + "de-balance-annual.csv");
            cashflowDEFileY = File.ReadAllLines(FILEPATH_BULKDATA + "de-cashflow-annual.csv");
        }

        public List<Market> RetrieveMarkets()
        {
            return new List<Market> { RetrieveMarket("US"), RetrieveMarket("DE") };
        }

        public Market RetrieveMarket(string name)
        {
            Market market = new Market() { Name = name };
            market.Sectors = RetrieveSectors();

            string[] companiesFile = new string[0];

            switch (name)
            {
                case "US":
                    companiesFile = companiesUSFile;
                    break;
                case "DE":
                    companiesFile = companiesDEFile;
                    break;
            }

            foreach (var companyLine in companiesFile)
            {
                if(!companyLine.StartsWith("Ticker"))
                {
                    AddCompany(market, companyLine.Split(';'));
                }
            }

            return market;
        }

        public List<Sector> RetrieveSectors()
        {
            var result = new List<Sector>();
            result.Add(new Sector { Name = "", Industries = new List<Industry> { new Industry { Name = "", Companies = new List<Company>() } } });

            foreach (var industryLine in industriesFile)
            {
                if(!industryLine.StartsWith("IndustryId"))
                {
                    string[] data = industryLine.Replace("\"","").Split(';');
                    var ind = new Industry { Name = data[2], Companies = new List<Company>() };

                    if(result.FirstOrDefault(e => e.Name == data[1]) != null)
                    {
                        result.First(e => e.Name == data[1]).Industries.Add(ind);
                    }
                    else
                    {
                        result.Add(new Sector { Name = data[1], Industries = new List<Industry> { ind } });
                    }
                }
            }

            return result;
        }

        public Company AddCompany(Market market, string[] simfinData)
        {
            string name = simfinData[2].Replace("\"", "");
            string ticker = simfinData[0].Replace("\"", "");

            string industryID = simfinData[3];
            var ind = ParseIndustry(industryID);
            string sectorName = ind.Item1;
            string industryName = ind.Item2;

            Company company = new Company { Name = name, Ticker = ticker };

            company.Filings = RetrieveFilings(ticker, market.Name);

            market.Sectors.First(e => e.Name == sectorName).Industries.First(e => e.Name == industryName).Companies.Add(company);

            return company;
        }

        public List<Filing> RetrieveFilings(string symbol, string marketName)
        {
            string[] incomeFile = new string[0];
            string[] balanceFile = new string[0];
            string[] cashFile = new string[0];

            switch (marketName)
            {
                case "US":
                    incomeFile = incomeUSFileY;
                    balanceFile = balanceUSFileY;
                    cashFile = cashflowUSFileY;
                    break;
                case "DE":
                    incomeFile = incomeDEFileY;
                    balanceFile = balanceDEFileY;
                    cashFile = cashflowDEFileY;
                    break;
            }

            var incomeStatements = RetrieveIncomeStatements(symbol, incomeFile);
            var balanceSheets = RetrieveBalanceSheets(symbol, balanceFile);
            var cashflowStatements = RetrieveCashFlowStatements(symbol, cashFile);

            var filingDates =
                incomeStatements.Select(e => e.FilingDate)
                .Union(balanceSheets.Select(e => e.FilingDate))
                .Union(cashflowStatements.Select(e => e.FilingDate))
                .Distinct();

            List<Filing> filings = new List<Filing>();

            foreach (var filingDate in filingDates)
            {
                var filing = new Filing { FilingDate = filingDate };
                var i = incomeStatements.FirstOrDefault(e => e.FilingDate == filingDate);
                var b = balanceSheets.FirstOrDefault(e => e.FilingDate == filingDate);
                var c = cashflowStatements.FirstOrDefault(e => e.FilingDate == filingDate);

                filing.IncomeStatement = i;
                filing.BalanceSheet = b;
                filing.CashflowStatement = c;

                filings.Add(filing);
            }

            return filings;
        }

        public List<IncomeStatement> RetrieveIncomeStatements(string symbol, string[] incomeFile)
        {
            var csv = GetCSVitems(symbol, incomeFile);

            var series = new List<IncomeStatement>();

            foreach (var line in csv)
            {
                series.Add(ParseIncomeStatement(line));
            }

            return series.OrderByDescending(e => e.FilingDate).ToList();
        }
        private IncomeStatement ParseIncomeStatement(string[] line)
        {
            var statement = new IncomeStatement();

            statement.Ticker = line[0].Replace("\"", "");
            statement.Currency = line[2].Replace("\"", "");
            statement.FilingDate = DateTime.Parse(line[5]);
            statement.PublishedDate = DateTime.Parse(line[6]);
            statement.RestatedDate = DateTime.Parse(line[7]);

            for (int i = 8; i < line.Length; i++)
            {
                if (line[i] == "")
                    line[i] = "0";
            }

            statement.WeightedAverageShsOut = decimal.Parse(line[8]);
            statement.WeightedAverageShsOutDiluted = decimal.Parse(line[9]);
            statement.Revenue = decimal.Parse(line[10]);
            statement.CostOfRevenue = decimal.Parse(line[11]);
            statement.GrossProfit = decimal.Parse(line[12]);
            statement.OperatingExpenses = decimal.Parse(line[13]);
            statement.SellingGeneralAndAdministrativeExpenses = decimal.Parse(line[14]);
            statement.ResearchAndDevelopmentExpenses = decimal.Parse(line[15]);
            statement.DepreciationAmortization = decimal.Parse(line[16]);
            statement.OperatingIncome = decimal.Parse(line[17]);
            statement.NonOperatingIncome = decimal.Parse(line[18]);
            statement.InterestExpense = decimal.Parse(line[19]);
            statement.EarningsBeforeTaxAdj = decimal.Parse(line[20]);
            statement.ExtraordinaryGains = decimal.Parse(line[21]);
            statement.EarningsBeforeTax = decimal.Parse(line[22]);
            statement.IncomeTaxExpense = decimal.Parse(line[23]);
            statement.NetIncomeDiscontinuedOps = decimal.Parse(line[24]);
            statement.ExtraordinaryGains = statement.ExtraordinaryGains == 0 ? decimal.Parse(line[25]) : statement.ExtraordinaryGains;
            statement.NetIncome = decimal.Parse(line[26]);
            statement.NetIncomeCom = decimal.Parse(line[27]);

            statement.EPS = statement.WeightedAverageShsOut != 0 ? statement.NetIncomeCom / statement.WeightedAverageShsOut : 0;
            statement.EPSDiluted = statement.WeightedAverageShsOutDiluted != 0 ? statement.NetIncomeCom / statement.WeightedAverageShsOutDiluted : 0;
            statement.GrossMargin = statement.Revenue != 0 ? statement.GrossProfit / statement.Revenue : 0;
            statement.EBIT = statement.InterestExpense != 0 ? statement.EarningsBeforeTax + statement.InterestExpense : 0;
            statement.EBITDA = statement.DepreciationAmortization != 0 ? statement.EBIT + statement.DepreciationAmortization : 0;
            statement.EBITMargin = statement.Revenue != 0 ? statement.EBIT / statement.Revenue : 0;
            statement.EBITDAMargin = statement.Revenue != 0 ? statement.EBITDA / statement.Revenue : 0;
            statement.EarningsBeforeTaxMargin = statement.Revenue != 0 ? statement.EarningsBeforeTaxAdj / statement.Revenue : 0;

            return statement;
        }

        public List<CashFlowStatement> RetrieveCashFlowStatements(string symbol, string[] cashflowFile)
        {
            var csv = GetCSVitems(symbol, cashflowFile);

            var series = new List<CashFlowStatement>();

            foreach (var line in csv)
            {
                series.Add(ParseCashFlowStatement(line));
            }

            return series.OrderByDescending(e => e.FilingDate).ToList();
        }
        private CashFlowStatement ParseCashFlowStatement(string[] line)
        {
            var statement = new CashFlowStatement();

            statement.Ticker = line[0].Replace("\"", "");
            statement.Currency = line[2].Replace("\"", "");
            statement.FilingDate = DateTime.Parse(line[5]);
            statement.PublishedDate = DateTime.Parse(line[6]);
            statement.RestatedDate = DateTime.Parse(line[7]);

            for (int i = 8; i < line.Length; i++)
            {
                if (line[i] == "")
                    line[i] = "0";
            }

            statement.DepreciationAmortization = decimal.Parse(line[11]);
            statement.NonCashItems = decimal.Parse(line[12]);
            statement.ChangeInWorkingCapital = decimal.Parse(line[13]);
            statement.ChangeInAccountsReceivable = decimal.Parse(line[14]);
            statement.ChangeInInventories = decimal.Parse(line[15]);
            statement.ChangeInAccountsPayable = decimal.Parse(line[16]);
            statement.ChangeInOther = decimal.Parse(line[17]);
            statement.OperatingCashFlow = decimal.Parse(line[18]);
            statement.CapitalExpenditure = decimal.Parse(line[19]);
            statement.InvestmentPurchasesAndSales = decimal.Parse(line[20]);
            statement.AcquisitionsAndDisposals = decimal.Parse(line[21]);
            statement.InvestingCashFlow = decimal.Parse(line[22]);
            statement.DividendPaments = decimal.Parse(line[23]);
            statement.RepaymentOfDebt = decimal.Parse(line[24]);
            statement.BuybacksOfShares = decimal.Parse(line[25]);
            statement.FinancingCashFlow = decimal.Parse(line[26]);
            statement.NetCashFlow = decimal.Parse(line[27]);

            statement.FreeCashFlow = statement.OperatingCashFlow - statement.CapitalExpenditure;

            return statement;
        }

        public List<BalanceSheet> RetrieveBalanceSheets(string symbol, string[] balanceFile)
        {
            var csv = GetCSVitems(symbol, balanceFile);

            var series = new List<BalanceSheet>();

            foreach (var line in csv)
            {
                series.Add(ParseBalanceSheet(line));
            }

            return series.OrderByDescending(e => e.FilingDate).ToList();
        }
        private BalanceSheet ParseBalanceSheet(string[] line)
        {
            var statement = new BalanceSheet();

            statement.Ticker = line[0].Replace("\"", "");
            statement.Currency = line[2].Replace("\"", "");
            statement.FilingDate = DateTime.Parse(line[5]);
            statement.PublishedDate = DateTime.Parse(line[6]);
            statement.RestatedDate = DateTime.Parse(line[7]);

            for (int i = 8; i < line.Length; i++)
            {
                if (line[i] == "")
                    line[i] = "0";
            }

            statement.CashAndCashEquivalents = decimal.Parse(line[10]);
            statement.Receivables = decimal.Parse(line[11]);
            statement.Inventories = decimal.Parse(line[12]);
            statement.TotalCurrentAssets = decimal.Parse(line[13]);
            statement.PropertyPlantAndEquipmentNet = decimal.Parse(line[14]);
            statement.LongTermInvestments = decimal.Parse(line[15]);
            statement.OtherAssets = decimal.Parse(line[16]);
            statement.TotalNonCurrentAssets = decimal.Parse(line[17]);
            statement.TotalAssets = decimal.Parse(line[18]);
            statement.Payables = decimal.Parse(line[19]);
            statement.ShortTermDebt = decimal.Parse(line[20]);
            statement.TotalCurrentLiabilities = decimal.Parse(line[21]);
            statement.LongTermDebt = decimal.Parse(line[22]);
            statement.TotalNonCurrentLiabilities = decimal.Parse(line[23]);
            statement.TotalLiabilities = decimal.Parse(line[24]);
            statement.ShareCapitalAndAdditionalPaidInCapital = decimal.Parse(line[25]);
            statement.TreasuryStock = decimal.Parse(line[26]);
            statement.RetainedEarnings = decimal.Parse(line[27]);
            statement.TotalEquity = decimal.Parse(line[28]);
            statement.TotalLiabilitiesAndEquity = decimal.Parse(line[29]);

            statement.CashAndShortTermInvestments = statement.CashAndCashEquivalents + statement.ShortTermInvestments;

            return statement;
        }

        private bool CheckIfSymbolAvailable(string symbol)
        {
            for (int i = 0; i < companiesUSFile.Length; i++)
            {
                if (companiesUSFile[i].StartsWith(symbol + ";"))
                    return true;
            }
            throw new ArgumentException("Symbol is not available in dataset: " + symbol);
        }

        private Tuple<string, string> ParseIndustry(string industryID)
        {
            if (industryID == "")
                return new Tuple<string, string>("", "");

            string text = industriesFile.First(e => e.StartsWith(industryID + ";")).Replace("\"", "");
            string[] data = text.Split(';');
            return new Tuple<string, string>(data[1], data[2]);
        }

        private List<string[]> GetCSVitems(string symbol, string[] file)
        {
            var lines = file.Where(e => e.StartsWith(symbol + ";"));
            var result = new List<string[]>();
            foreach (var line in lines)
            {
                result.Add(line.Split(';'));
            }
            return result;
        }

        public IEnumerable<string> GetAllAvailableTickers()
        {
            for (int i = 1; i < companiesUSFile.Length; i++)
            {
                yield return companiesUSFile[i].Split(';')[0];
            }
        }
    }
}
