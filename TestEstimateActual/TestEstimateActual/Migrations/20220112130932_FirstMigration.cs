using Microsoft.EntityFrameworkCore.Migrations;

namespace TestEstimateActual.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actuals",
                columns: table => new
                {
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    ActualPopulation = table.Column<int>(type: "INTEGER", nullable: false),
                    ActualHouseholds = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actuals", x => x.State);
                });

            migrationBuilder.CreateTable(
                name: "Estimates",
                columns: table => new
                {
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    District = table.Column<int>(type: "INTEGER", nullable: false),
                    EstimatePopulation = table.Column<int>(type: "INTEGER", nullable: false),
                    EstimateHouseholds = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("estimate_pk", x => new { x.State, x.District });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actuals");

            migrationBuilder.DropTable(
                name: "Estimates");
        }
    }
}
