using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Benchmark.Migrations
{
    public partial class InitialCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Benchmark",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarketName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SectorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndustryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarketBenchmark_MarketName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SectorBenchmark_MarketName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SectorBenchmark_SectorName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Benchmark", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_Metrics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BenchmarkID = table.Column<int>(type: "int", nullable: false),
                    MetricName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Percentile01 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Percentile05 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Percentile10 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Percentile25 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Median = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Average = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Percentile75 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Percentile90 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Percentile95 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Percentile99 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TopCompanies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorstCompanies = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Metrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Metrics_Benchmark_BenchmarkID",
                        column: x => x.BenchmarkID,
                        principalTable: "Benchmark",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyLatestFiling",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LatestFilingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BenchmarkId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyLatestFiling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyLatestFiling_Benchmark_BenchmarkId",
                        column: x => x.BenchmarkId,
                        principalTable: "Benchmark",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MetricDataPoint",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MetricId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetricDataPoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MetricDataPoint__Metrics_MetricId",
                        column: x => x.MetricId,
                        principalTable: "_Metrics",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX__Metrics_BenchmarkID",
                table: "_Metrics",
                column: "BenchmarkID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyLatestFiling_BenchmarkId",
                table: "CompanyLatestFiling",
                column: "BenchmarkId");

            migrationBuilder.CreateIndex(
                name: "IX_MetricDataPoint_MetricId",
                table: "MetricDataPoint",
                column: "MetricId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyLatestFiling");

            migrationBuilder.DropTable(
                name: "MetricDataPoint");

            migrationBuilder.DropTable(
                name: "_Metrics");

            migrationBuilder.DropTable(
                name: "Benchmark");
        }
    }
}
