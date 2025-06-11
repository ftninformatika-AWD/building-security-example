using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BuildingExample.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Floors = table.Column<int>(type: "integer", nullable: false),
                    HasElevator = table.Column<bool>(type: "boolean", nullable: false),
                    YearOfConstruction = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Apartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApartmentNumber = table.Column<string>(type: "text", nullable: false),
                    Floor = table.Column<int>(type: "integer", nullable: false),
                    Area = table.Column<double>(type: "double precision", nullable: false),
                    NumberOfRooms = table.Column<int>(type: "integer", nullable: false),
                    BuildingId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apartments_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Buildings",
                columns: new[] { "Id", "Address", "Floors", "HasElevator", "YearOfConstruction" },
                values: new object[,]
                {
                    { 1, "123 Main St", 5, true, 2000 },
                    { 2, "456 Oak Ave", 6, false, 1995 },
                    { 3, "789 Pine Blvd", 4, true, 2010 },
                    { 4, "101 Maple Dr", 8, true, 2005 },
                    { 5, "202 Birch Rd", 3, false, 1990 }
                });

            migrationBuilder.InsertData(
                table: "Apartments",
                columns: new[] { "Id", "ApartmentNumber", "Area", "BuildingId", "Floor", "NumberOfRooms" },
                values: new object[,]
                {
                    { 1, "101", 50.0, 1, 1, 2 },
                    { 2, "102", 55.0, 1, 1, 2 },
                    { 3, "201", 60.0, 1, 2, 3 },
                    { 4, "202", 65.0, 1, 2, 3 },
                    { 5, "301", 70.0, 2, 3, 3 },
                    { 6, "302", 75.0, 2, 3, 3 },
                    { 7, "401", 80.0, 3, 4, 4 },
                    { 8, "402", 85.0, 3, 4, 4 },
                    { 9, "501", 90.0, 4, 5, 5 },
                    { 10, "502", 95.0, 4, 5, 5 },
                    { 11, "601", 100.0, 5, 6, 6 },
                    { 12, "602", 105.0, 5, 6, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_BuildingId",
                table: "Apartments",
                column: "BuildingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apartments");

            migrationBuilder.DropTable(
                name: "Buildings");
        }
    }
}
