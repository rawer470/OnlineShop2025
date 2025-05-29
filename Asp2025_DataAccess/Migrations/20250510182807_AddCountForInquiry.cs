using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asp2025_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCountForInquiry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "InquiryDetail",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "InquiryDetail");
        }
    }
}
