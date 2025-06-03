using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnalyticsService.DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstrumentDailyStats",
                columns: table => new
                {
                    InstrumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Views = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentDailyStats", x => new { x.InstrumentId, x.Date });
                });

            migrationBuilder.CreateTable(
                name: "InstrumentStats",
                columns: table => new
                {
                    InstrumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Views = table.Column<int>(type: "integer", nullable: false),
                    ContactViews = table.Column<int>(type: "integer", nullable: false),
                    Bookmarks = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentStats", x => x.InstrumentId);
                });

            migrationBuilder.CreateTable(
                name: "UserStats",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalLogins = table.Column<int>(type: "integer", nullable: false),
                    InstrumentsCreated = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStats", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstrumentDailyStats");

            migrationBuilder.DropTable(
                name: "InstrumentStats");

            migrationBuilder.DropTable(
                name: "UserStats");
        }
    }
}
