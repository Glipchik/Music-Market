using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnalyticsService.DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class change_defult_schema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "analytics");

            migrationBuilder.RenameTable(
                name: "UserStats",
                newName: "UserStats",
                newSchema: "analytics");

            migrationBuilder.RenameTable(
                name: "InstrumentStats",
                newName: "InstrumentStats",
                newSchema: "analytics");

            migrationBuilder.RenameTable(
                name: "InstrumentDailyStats",
                newName: "InstrumentDailyStats",
                newSchema: "analytics");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "UserStats",
                schema: "analytics",
                newName: "UserStats");

            migrationBuilder.RenameTable(
                name: "InstrumentStats",
                schema: "analytics",
                newName: "InstrumentStats");

            migrationBuilder.RenameTable(
                name: "InstrumentDailyStats",
                schema: "analytics",
                newName: "InstrumentDailyStats");
        }
    }
}
