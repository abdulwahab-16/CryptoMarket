using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate_Mvc_.Migrations
{
    /// <inheritdoc />
    public partial class CryptoMarket2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(max)",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "abcd",
                column: "DateCrated",
                value: new DateTime(2024, 12, 4, 17, 51, 59, 588, DateTimeKind.Local).AddTicks(7302));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "abcd123",
                column: "DateCrated",
                value: new DateTime(2024, 12, 4, 17, 51, 59, 588, DateTimeKind.Local).AddTicks(7412));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "abcd",
                column: "DateDeleted",
                value: new DateTime(2024, 12, 4, 17, 51, 59, 588, DateTimeKind.Local).AddTicks(7062));

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "001",
                column: "DateCrated",
                value: new DateTime(2024, 12, 4, 17, 51, 59, 382, DateTimeKind.Local).AddTicks(5157));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "Super001",
                columns: new[] { "DateCrated", "Password" },
                values: new object[] { new DateTime(2024, 12, 4, 17, 51, 59, 382, DateTimeKind.Local).AddTicks(5369), "$2a$11$IzE8meu48VteKHlygHnrPOq2nJ2jVRrZnkxq.Y3gMINhJeTTFyPWK" });
        }
    }
}
