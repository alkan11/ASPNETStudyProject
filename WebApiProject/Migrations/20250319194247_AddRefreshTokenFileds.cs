using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApiProject.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenFileds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16dde6d4-f225-49b0-8ab2-46409171cead");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e161cc16-3336-4024-8dcd-8e58d43b64ad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa2e33a4-3d9c-4fec-88eb-1458731894c0");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpire",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "16a4d778-e5b9-4a24-9264-fb8f37c6516d", null, "User", "USER" },
                    { "176a80a6-cdea-40ad-9ccf-e9dfbc116002", null, "Editor", "EDITOR" },
                    { "9ba84d5c-be84-4b09-a9bb-1225e1b703ac", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16a4d778-e5b9-4a24-9264-fb8f37c6516d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "176a80a6-cdea-40ad-9ccf-e9dfbc116002");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ba84d5c-be84-4b09-a9bb-1225e1b703ac");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpire",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "16dde6d4-f225-49b0-8ab2-46409171cead", null, "Editor", "EDITOR" },
                    { "e161cc16-3336-4024-8dcd-8e58d43b64ad", null, "User", "USER" },
                    { "fa2e33a4-3d9c-4fec-88eb-1458731894c0", null, "Admin", "ADMIN" }
                });
        }
    }
}
