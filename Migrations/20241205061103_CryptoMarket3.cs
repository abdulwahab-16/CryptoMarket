using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate_Mvc_.Migrations
{
    /// <inheritdoc />
    public partial class CryptoMarket3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Customers_CustomerId",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_CustomerId",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Wallets");

            migrationBuilder.AddColumn<string>(
                name: "CustomerEmail",
                table: "Wallets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WalletId",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "abcd",
                column: "DateCrated",
                value: new DateTime(2024, 12, 5, 7, 11, 1, 995, DateTimeKind.Local).AddTicks(8049));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "abcd123",
                column: "DateCrated",
                value: new DateTime(2024, 12, 5, 7, 11, 1, 995, DateTimeKind.Local).AddTicks(8142));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "abcd",
                column: "DateDeleted",
                value: new DateTime(2024, 12, 5, 7, 11, 1, 995, DateTimeKind.Local).AddTicks(7782));

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "001",
                column: "DateCrated",
                value: new DateTime(2024, 12, 5, 7, 11, 1, 694, DateTimeKind.Local).AddTicks(3660));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "Super001",
                columns: new[] { "DateCrated", "Password" },
                values: new object[] { new DateTime(2024, 12, 5, 7, 11, 1, 694, DateTimeKind.Local).AddTicks(4009), "$2a$11$zF/jXVA2X8qIrVZoCYqfoePl8u2fEE4dAgSJI3SeEYkRJe8sVmed6" });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_WalletId",
                table: "Customers",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Wallets_WalletId",
                table: "Customers",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Wallets_WalletId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_WalletId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerEmail",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "Wallets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "abcd",
                column: "DateCrated",
                value: new DateTime(2024, 12, 5, 6, 49, 54, 691, DateTimeKind.Local).AddTicks(9497));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "abcd123",
                column: "DateCrated",
                value: new DateTime(2024, 12, 5, 6, 49, 54, 691, DateTimeKind.Local).AddTicks(9595));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "abcd",
                column: "DateDeleted",
                value: new DateTime(2024, 12, 5, 6, 49, 54, 691, DateTimeKind.Local).AddTicks(9196));

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "001",
                column: "DateCrated",
                value: new DateTime(2024, 12, 5, 6, 49, 54, 396, DateTimeKind.Local).AddTicks(2234));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "Super001",
                columns: new[] { "DateCrated", "Password" },
                values: new object[] { new DateTime(2024, 12, 5, 6, 49, 54, 396, DateTimeKind.Local).AddTicks(2782), "$2a$11$YTSnm62uv7rmDIJ/Y4MG1.0aiRfoSJgrnTHAuF2OxuTWim7.pydBe" });

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_CustomerId",
                table: "Wallets",
                column: "CustomerId",
                unique: true,
                filter: "[CustomerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Customers_CustomerId",
                table: "Wallets",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
