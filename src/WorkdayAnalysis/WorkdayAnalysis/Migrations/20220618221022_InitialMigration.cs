using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkdayAnalysis.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FundamentalDataId = table.Column<int>(type: "int", nullable: false),
                    WorkdayHCMGoLive = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkdayFINGoLive = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkdayPLNGoLive = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkdaySRCGoLive = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkdayPKNGoLive = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Markets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountIds = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Filings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Revenue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CostOfRevenue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GrossProfit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SellingGeneralAdministrative = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SellingMarketing = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GeneralAdministrative = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OperatingExpenses = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OperatingIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PretaxIncomeAdj = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PretaxIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OperatingCashflow = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FinancingCashflow = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InvestingCashflow = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FreeCashflow = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AccountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Filings_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CloseAdj = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AccountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prices_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Filings_AccountId",
                table: "Filings",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_AccountId",
                table: "Prices",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Filings");

            migrationBuilder.DropTable(
                name: "Markets");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
