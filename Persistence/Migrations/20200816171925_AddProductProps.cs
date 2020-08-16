using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddProductProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BuyingPrice",
                table: "Products",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ConfidentialData",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Products",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyingPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ConfidentialData",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Products");
        }
    }
}
