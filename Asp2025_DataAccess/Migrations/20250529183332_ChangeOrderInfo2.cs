using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asp2025_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOrderInfo2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "StreetAddress",
                table: "OrderHeader");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "OrderHeader",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress",
                table: "OrderHeader",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
