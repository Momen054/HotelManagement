using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagement.Migrations
{
    /// <inheritdoc />
    public partial class Edit1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Invoices_ReservationId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ReservationServices");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "ReservationServices");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Services",
                newName: "UnitPrice");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ReservationId",
                table: "Invoices",
                column: "ReservationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Invoices_ReservationId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Services");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "Services",
                newName: "Price");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ReservationServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "ReservationServices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ReservationId",
                table: "Invoices",
                column: "ReservationId");
        }
    }
}
