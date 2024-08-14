using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightsAPI_Simple.Data.Migrations
{
    /// <inheritdoc />
    public partial class Create_Airlines_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AirlineName",
                table: "Flights");

            migrationBuilder.CreateTable(
                name: "Airlines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    FlightId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airlines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Airlines_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Airlines_FlightId",
                table: "Airlines",
                column: "FlightId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Airlines");

            migrationBuilder.AddColumn<string>(
                name: "AirlineName",
                table: "Flights",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
