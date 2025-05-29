using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asp2025_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOrderInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "State",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "OrderHeader");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "OrderHeader",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "OrderHeader",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "OrderHeader",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
