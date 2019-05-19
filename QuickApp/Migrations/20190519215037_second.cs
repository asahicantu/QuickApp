using Microsoft.EntityFrameworkCore.Migrations;

namespace QuickApp.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LockedBy",
                table: "Svcs",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LockedBy",
                table: "AppProducts",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LockedBy",
                table: "AppProductCategories",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LockedBy",
                table: "AppOrders",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LockedBy",
                table: "AppOrderDetails",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LockedBy",
                table: "AppCustomers",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LockedBy",
                table: "Svcs");

            migrationBuilder.DropColumn(
                name: "LockedBy",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "LockedBy",
                table: "AppProductCategories");

            migrationBuilder.DropColumn(
                name: "LockedBy",
                table: "AppOrders");

            migrationBuilder.DropColumn(
                name: "LockedBy",
                table: "AppOrderDetails");

            migrationBuilder.DropColumn(
                name: "LockedBy",
                table: "AppCustomers");
        }
    }
}
