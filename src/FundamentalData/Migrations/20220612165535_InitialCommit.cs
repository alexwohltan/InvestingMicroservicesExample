using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FundamentalData.Migrations
{
    public partial class InitialCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BalanceSheet",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CashCashEquivalentsShortTermInvestments = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashCashEquivalents = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShortTermInvestments = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccountsNotesReceivable = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccountsReceivableNet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NotesReceivableNet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnbilledRevenues = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Inventories = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RawMaterials = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WorkInProcess = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinishedGoods = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherInventory = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherShortTermAssets = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrepaidExpenses = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DerivativeHedgingAssetsShortTerm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AssetsHeldforSale = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeferredTaxAssetsShortTerm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IncomeTaxesReceivable = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscontinuedOperationsShortTerm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MiscShortTermAssets = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCurrentAssets = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PropertyPlantEquipmentNet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PropertyPlantEquipment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccumulatedDepreciation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LongTermInvestmentsReceivables = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LongTermInvestments = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LongTermMarketableSecurities = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LongTermReceivables = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherLongTermAssets = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IntangibleAssets = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Goodwill = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherIntangibleAssets = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrepaidExpense = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeferredTaxAssetsLongTerm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DerivativeHedgingAssetsLongTerm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrepaidPensionCosts = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscontinuedOperationsLongTerm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InvestmentsinAffiliates = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MiscLongTermAssets = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalNoncurrentAssets = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAssets = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PayablesAccruals = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccountsPayable = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccruedTaxes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InterestDividendsPayable = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherPayablesAccruals = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShortTermDebt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShortTermBorrowings = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShortTermCapitalLeases = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrentPortionofLongTermDebt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherShortTermLiabilities = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeferredRevenueShortTerm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LiabilitiesfromDerivativesHedgingShortTerm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeferredTaxLiabilitiesShortTerm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LiabilitiesfromDiscontinuedOperationsShortTerm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MiscShortTermLiabilities = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCurrentLiabilities = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LongTermDebt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LongTermBorrowings = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LongTermCapitalLeases = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherLongTermLiabilities = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccruedLiabilities = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PensionLiabilities = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Pensions = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherPostRetirementBenefits = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeferredCompensation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeferredRevenueLongTerm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeferredTaxLiabilitiesLongTerm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LiabilitiesfromDerivativesHedgingLongTerm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LiabilitiesfromDiscontinuedOperationsLongTerm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MiscLongTermLiabilities = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalNoncurrentLiabilities = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalLiabilities = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PreferredEquity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShareCapitalAdditionalPaidInCapital = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CommonStock = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdditionalPaidinCapital = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherShareCapital = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TreasuryStock = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RetainedEarnings = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherEquity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EquityBeforeMinorityInterest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinorityInterest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalEquity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalLiabilitiesEquity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "CashFlowStatement",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NetIncomeStartingLine = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetIncomefromDiscontinuedOperations = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherAdjustments = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DepreciationAmortization = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NonCashItems = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockBasedCompensation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeferredIncomeTaxes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherNonCashAdjustments = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChangeinWorkingCapital = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChangeinAccountsReceivable = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChangeinInventories = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChangeinAccountsPayable = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChangeinOther = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetCashfromDiscontinuedOperationsOperating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetCashfromOperatingActivities = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChangeinFixedAssetsIntangibles = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DispositionofFixedAssetsIntangibles = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DispositionofFixedAssets = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DispositionofIntangibleAssets = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AcquisitionofFixedAssetsIntangibles = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchaseofFixedAssets = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AcquisitionofIntangibleAssets = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherChangeinFixedAssetsIntangibles = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetChangeinLongTermInvestment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DecreaseinLongTermInvestment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IncreaseinLongTermInvestment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetCashfromAcquisitionsDivestitures = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetCashfromDivestitures = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashforAcquisitionofSubsidiaries = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashforJointVentures = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetCashfromOtherAcquisitions = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherInvestingActivities = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetCashfromDiscontinuedOperationsInvesting = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetCashfromInvestingActivities = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DividendsPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashfromRepaymentofDebt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashfromRepaymentofShortTermDebtNet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashfromRepaymentofLongTermDebtNet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RepaymentsofLongTermDebt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashfromLongTermDebt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashfromRepurchaseofEquity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IncreaseinCapitalStock = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DecreaseinCapitalStock = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherFinancingActivities = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetCashfromDiscontinuedOperationsFinancing = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetCashfromFinancingActivities = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetCashBeforeDiscOperationsandFX = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChangeinCashfromDiscOperationsandOther = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetCashBeforeFX = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EffectofForeignExchangeRates = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetChangeinCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FreeCashFlow = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "IncomeStatement",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Revenue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalesServicesRevenue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinancingRevenue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherRevenue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostofRevenue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostofGoodsServices = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostofFinancingRevenue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostofOtherRevenue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrossProfit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherOperatingIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ResearchDevelopment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SellingGeneralAdministrative = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SellingMarketing = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GeneralAdministrative = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OperatingExpenses = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DepreciationAmortization = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProvisionforDoubtfulAccounts = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherOperatingExpenses = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OperatingIncomeLoss = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NonOperatingIncomeLoss = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InterestExpense = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InterestExpenseNet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InterestIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherInvestmentIncomeLoss = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ForeignExchangeGainLoss = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IncomeLossfromAffiliates = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherNonOperatingIncomeLoss = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PretaxIncomeLossAdj = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AbnormalGainsLosses = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AcquiredInProcessRD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MergerAcquisitionExpense = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AbnormalDerivatives = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DisposalofAssets = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EarlyExtinguishmentofDebt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AssetWriteDown = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImpairmentofGoodwillIntangibles = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SaleofBusiness = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LegalSettlement = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RestructuringCharges = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SaleofInvestmentsUnrealizedInvestments = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InsuranceSettlement = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherAbnormalItems = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PretaxIncomeLoss = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IncomeTaxExpenseBenefitNet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrentIncomeTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeferredIncomeTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAllowanceCredit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IncomeLossfromAffiliatesNetofTaxes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IncomeLossfromContinuingOperations = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetExtraordinaryGainsLosses = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccountingChargesOther = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IncomeLossInclMinorityInterest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinorityInterest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetIncomeNonControllingInt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscontinuedOperations = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PreferredDividends = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherAdjustments = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetIncomeCommon = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EPS = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EPSDiluted = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WeightedAverageShsOut = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WeightedAverageShsOutDiluted = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DividendPerShare = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrossMargin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EBITDAMargin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EBITMargin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProfitMargin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FreeCashFlowMargin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EBITDA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EBIT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConsolidatedIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EarningsBeforeTaxMargin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
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
                });

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
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthFyEnd = table.Column<int>(type: "int", nullable: false),
                    NumberEmployees = table.Column<int>(type: "int", nullable: false),
                    BusinessSummary = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    IncomeStatementID = table.Column<int>(type: "int", nullable: true),
                    BalanceSheetID = table.Column<int>(type: "int", nullable: true),
                    CashflowStatementID = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_Filings_BalanceSheet_BalanceSheetID",
                        column: x => x.BalanceSheetID,
                        principalTable: "BalanceSheet",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Filings_CashFlowStatement_CashflowStatementID",
                        column: x => x.CashflowStatementID,
                        principalTable: "CashFlowStatement",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Filings_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Filings_IncomeStatement_IncomeStatementID",
                        column: x => x.IncomeStatementID,
                        principalTable: "IncomeStatement",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Open = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    High = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Low = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Close = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AdjClose = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Volume = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Dividend = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Prices_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_IndustryID",
                table: "Companies",
                column: "IndustryID");

            migrationBuilder.CreateIndex(
                name: "IX_Filings_BalanceSheetID",
                table: "Filings",
                column: "BalanceSheetID");

            migrationBuilder.CreateIndex(
                name: "IX_Filings_CashflowStatementID",
                table: "Filings",
                column: "CashflowStatementID");

            migrationBuilder.CreateIndex(
                name: "IX_Filings_CompanyID",
                table: "Filings",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Filings_IncomeStatementID",
                table: "Filings",
                column: "IncomeStatementID");

            migrationBuilder.CreateIndex(
                name: "IX_Industries_SectorID",
                table: "Industries",
                column: "SectorID");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_CompanyID",
                table: "Prices",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Sectors_MarketID",
                table: "Sectors",
                column: "MarketID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Filings");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "BalanceSheet");

            migrationBuilder.DropTable(
                name: "CashFlowStatement");

            migrationBuilder.DropTable(
                name: "IncomeStatement");

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
