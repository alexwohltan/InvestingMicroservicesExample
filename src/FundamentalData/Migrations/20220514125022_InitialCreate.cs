using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FundamentalData.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Markets",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markets", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarketID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sectors_Markets_MarketID",
                        column: x => x.MarketID,
                        principalTable: "Markets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Industries",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectorID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Industries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Industries_Sectors_SectorID",
                        column: x => x.SectorID,
                        principalTable: "Sectors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IndustryID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Companies_Industries_IndustryID",
                        column: x => x.IndustryID,
                        principalTable: "Industries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Filings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<int>(type: "int", nullable: true),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RestatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Filings_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "BalanceSheet",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportID = table.Column<int>(type: "int", nullable: true),
                    CashAndCashEquivalents = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShortTermInvestments = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashAndShortTermInvestments = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Receivables = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Inventories = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCurrentAssets = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PropertyPlantAndEquipmentNet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GoodwillAndIntangibleAssets = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LongTermInvestments = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAssets = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalNonCurrentAssets = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAssets = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Payables = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShortTermDebt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCurrentLiabilities = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LongTermDebt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDebt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeferredRevenue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxLiabilities = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DepositLiabilities = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalNonCurrentLiabilities = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalLiabilities = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherComprehensiveIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RetainedEarnings = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShareCapitalAndAdditionalPaidInCapital = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TreasuryStock = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalShareholdersEquity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Investments = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetDebt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherAssets = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherLiabilites = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalEquity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalLiabilitiesAndEquity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RestatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceSheet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BalanceSheet_Filings_ReportID",
                        column: x => x.ReportID,
                        principalTable: "Filings",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "CashFlowStatement",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportID = table.Column<int>(type: "int", nullable: true),
                    DepreciationAmortization = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NonCashItems = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChangeInWorkingCapital = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChangeInAccountsReceivable = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChangeInInventories = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChangeInAccountsPayable = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChangeInOther = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockBasedCompensation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OperatingCashFlow = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CapitalExpenditure = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AcquisitionsAndDisposals = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InvestmentPurchasesAndSales = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InvestingCashFlow = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RepaymentOfDebt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BuybacksOfShares = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DividendPaments = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinancingCashFlow = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EffectOfForexChangesOnCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetCashFlow = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FreeCashFlow = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RestatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashFlowStatement", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CashFlowStatement_Filings_ReportID",
                        column: x => x.ReportID,
                        principalTable: "Filings",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "IncomeStatement",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportID = table.Column<int>(type: "int", nullable: true),
                    Revenue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostOfRevenue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrossProfit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ResearchAndDevelopmentExpenses = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SellingGeneralAndAdministrativeExpenses = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OperatingExpenses = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DepreciationAmortization = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OperatingIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NonOperatingIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InterestExpense = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EarningsBeforeTaxAdj = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExtraordinaryGains = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EarningsBeforeTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IncomeTaxExpense = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetIncomeNonControllingInt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetIncomeDiscontinuedOps = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PreferredDividends = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetIncomeCom = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EPS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EPSDiluted = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WeightedAverageShsOut = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WeightedAverageShsOutDiluted = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DividendPerShare = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrossMargin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EBITDAMargin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EBITMargin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProfitMargin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FreeCashFlowMargin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EBITDA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EBIT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConsolidatedIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EarningsBeforeTaxMargin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetProfitMargin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RestatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeStatement", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IncomeStatement_Filings_ReportID",
                        column: x => x.ReportID,
                        principalTable: "Filings",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BalanceSheet_ReportID",
                table: "BalanceSheet",
                column: "ReportID",
                unique: true,
                filter: "[ReportID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CashFlowStatement_ReportID",
                table: "CashFlowStatement",
                column: "ReportID",
                unique: true,
                filter: "[ReportID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_IndustryID",
                table: "Companies",
                column: "IndustryID");

            migrationBuilder.CreateIndex(
                name: "IX_Filings_CompanyID",
                table: "Filings",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeStatement_ReportID",
                table: "IncomeStatement",
                column: "ReportID",
                unique: true,
                filter: "[ReportID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Industries_SectorID",
                table: "Industries",
                column: "SectorID");

            migrationBuilder.CreateIndex(
                name: "IX_Sectors_MarketID",
                table: "Sectors",
                column: "MarketID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BalanceSheet");

            migrationBuilder.DropTable(
                name: "CashFlowStatement");

            migrationBuilder.DropTable(
                name: "IncomeStatement");

            migrationBuilder.DropTable(
                name: "Filings");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Industries");

            migrationBuilder.DropTable(
                name: "Sectors");

            migrationBuilder.DropTable(
                name: "Markets");
        }
    }
}
