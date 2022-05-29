﻿// <auto-generated />
using System;
using FundamentalData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FundamentalData.Migrations
{
    [DbContext(typeof(FundamentalDataContext))]
    [Migration("20220529145950_Initialize")]
    partial class Initialize
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DataStructures.FundamentalData.BalanceSheet", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<decimal>("CashAndCashEquivalents")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CashAndShortTermInvestments")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("DeferredRevenue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DepositLiabilities")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("FilingDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("GoodwillAndIntangibleAssets")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Inventories")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Investments")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("LongTermDebt")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("LongTermInvestments")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("NetDebt")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("OtherAssets")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("OtherComprehensiveIncome")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("OtherLiabilites")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Payables")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("PeriodStartDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("PropertyPlantAndEquipmentNet")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("PublishedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Receivables")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("RestatedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("RetainedEarnings")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ShareCapitalAndAdditionalPaidInCapital")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ShortTermDebt")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ShortTermInvestments")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TaxAssets")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TaxLiabilities")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Ticker")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalAssets")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalCurrentAssets")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalCurrentLiabilities")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalDebt")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalEquity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalLiabilities")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalLiabilitiesAndEquity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalNonCurrentAssets")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalNonCurrentLiabilities")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalShareholdersEquity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TreasuryStock")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.ToTable("BalanceSheet");
                });

            modelBuilder.Entity("DataStructures.FundamentalData.CashFlowStatement", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<decimal>("AcquisitionsAndDisposals")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("BuybacksOfShares")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CapitalExpenditure")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ChangeInAccountsPayable")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ChangeInAccountsReceivable")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ChangeInInventories")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ChangeInOther")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ChangeInWorkingCapital")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("DepreciationAmortization")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DividendPayments")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("EffectOfForexChangesOnCash")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("FilingDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("FinancingCashFlow")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("FreeCashFlow")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("InvestingCashFlow")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("InvestmentPurchasesAndSales")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("NetCash")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("NetCashFlow")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("NonCashItems")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("OperatingCashFlow")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("PeriodStartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PublishedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("RepaymentOfDebt")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("RestatedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("StockBasedCompensation")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Ticker")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("CashFlowStatement");
                });

            modelBuilder.Entity("DataStructures.FundamentalData.Company", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("IndustryID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ticker")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("IndustryID");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("DataStructures.FundamentalData.Filing", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int?>("BalanceSheetID")
                        .HasColumnType("int");

                    b.Property<int?>("CashflowStatementID")
                        .HasColumnType("int");

                    b.Property<int>("CompanyID")
                        .HasColumnType("int");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FilingDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IncomeStatementID")
                        .HasColumnType("int");

                    b.Property<DateTime>("PeriodStartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PublishedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RestatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Ticker")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("BalanceSheetID");

                    b.HasIndex("CashflowStatementID");

                    b.HasIndex("CompanyID");

                    b.HasIndex("IncomeStatementID");

                    b.ToTable("Filings");
                });

            modelBuilder.Entity("DataStructures.FundamentalData.IncomeStatement", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<decimal>("ConsolidatedIncome")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CostOfRevenue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("DepreciationAmortization")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DividendPerShare")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("EBIT")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("EBITDA")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EBITDAMargin")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EBITMargin")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EPS")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EPSDiluted")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("EarningsBeforeTax")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("EarningsBeforeTaxAdj")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("EarningsBeforeTaxMargin")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ExtraordinaryGains")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("FilingDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("FreeCashFlowMargin")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("GrossMargin")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("GrossProfit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("IncomeTaxExpense")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("InterestExpense")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("NetIncome")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("NetIncomeCom")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("NetIncomeDiscontinuedOps")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("NetIncomeNonControllingInt")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("NetProfitMargin")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("NonOperatingIncome")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("OperatingExpenses")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("OperatingIncome")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("PeriodStartDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("PreferredDividends")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ProfitMargin")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("PublishedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("ResearchAndDevelopmentExpenses")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("RestatedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Revenue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SellingGeneralAndAdministrativeExpenses")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Ticker")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("WeightedAverageShsOut")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("WeightedAverageShsOutDiluted")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.ToTable("IncomeStatement");
                });

            modelBuilder.Entity("DataStructures.FundamentalData.Industry", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SectorID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("SectorID");

                    b.ToTable("Industries");
                });

            modelBuilder.Entity("DataStructures.FundamentalData.Market", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Markets");
                });

            modelBuilder.Entity("DataStructures.FundamentalData.Sector", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("MarketID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("MarketID");

                    b.ToTable("Sectors");
                });

            modelBuilder.Entity("DataStructures.FundamentalData.Company", b =>
                {
                    b.HasOne("DataStructures.FundamentalData.Industry", "Industry")
                        .WithMany("Companies")
                        .HasForeignKey("IndustryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Industry");
                });

            modelBuilder.Entity("DataStructures.FundamentalData.Filing", b =>
                {
                    b.HasOne("DataStructures.FundamentalData.BalanceSheet", "BalanceSheet")
                        .WithMany()
                        .HasForeignKey("BalanceSheetID");

                    b.HasOne("DataStructures.FundamentalData.CashFlowStatement", "CashflowStatement")
                        .WithMany()
                        .HasForeignKey("CashflowStatementID");

                    b.HasOne("DataStructures.FundamentalData.Company", "Company")
                        .WithMany("Filings")
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataStructures.FundamentalData.IncomeStatement", "IncomeStatement")
                        .WithMany()
                        .HasForeignKey("IncomeStatementID");

                    b.Navigation("BalanceSheet");

                    b.Navigation("CashflowStatement");

                    b.Navigation("Company");

                    b.Navigation("IncomeStatement");
                });

            modelBuilder.Entity("DataStructures.FundamentalData.Industry", b =>
                {
                    b.HasOne("DataStructures.FundamentalData.Sector", "Sector")
                        .WithMany("Industries")
                        .HasForeignKey("SectorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sector");
                });

            modelBuilder.Entity("DataStructures.FundamentalData.Sector", b =>
                {
                    b.HasOne("DataStructures.FundamentalData.Market", "Market")
                        .WithMany("Sectors")
                        .HasForeignKey("MarketID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Market");
                });

            modelBuilder.Entity("DataStructures.FundamentalData.Company", b =>
                {
                    b.Navigation("Filings");
                });

            modelBuilder.Entity("DataStructures.FundamentalData.Industry", b =>
                {
                    b.Navigation("Companies");
                });

            modelBuilder.Entity("DataStructures.FundamentalData.Market", b =>
                {
                    b.Navigation("Sectors");
                });

            modelBuilder.Entity("DataStructures.FundamentalData.Sector", b =>
                {
                    b.Navigation("Industries");
                });
#pragma warning restore 612, 618
        }
    }
}