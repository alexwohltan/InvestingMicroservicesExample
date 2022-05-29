using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Benchmark.Migrations
{
    public partial class MetricCompanyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "MetricDataPoint",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "MetricDataPoint");
        }
    }
}
