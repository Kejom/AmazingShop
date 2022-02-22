using Microsoft.EntityFrameworkCore.Migrations;

namespace AmazingShop.Migrations
{
    public partial class OrderAndProductTablesUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeaders_Addresses_AddressId",
                table: "OrderHeaders");

            migrationBuilder.DropIndex(
                name: "IX_OrderHeaders_AddressId",
                table: "OrderHeaders");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "OrderHeaders",
                newName: "LocalNumber");

            migrationBuilder.AddColumn<bool>(
                name: "Hidden",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "BuildingNumber",
                table: "OrderHeaders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ZipPostalCode",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "OrderedProducts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hidden",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BuildingNumber",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "City",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "ZipPostalCode",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "OrderedProducts");

            migrationBuilder.RenameColumn(
                name: "LocalNumber",
                table: "OrderHeaders",
                newName: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_AddressId",
                table: "OrderHeaders",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeaders_Addresses_AddressId",
                table: "OrderHeaders",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
