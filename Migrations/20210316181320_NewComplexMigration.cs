using Microsoft.EntityFrameworkCore.Migrations;

namespace LTPruebaTecnica.Migrations
{
    public partial class NewComplexMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Country_CountryId",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_CountryId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Booking");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_CountryId",
                table: "Booking",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Country_CountryId",
                table: "Booking",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
