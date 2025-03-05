using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TheBrickVault.Migrations
{
    /// <inheritdoc />
    public partial class SeedLegoSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LegoSets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LegoSets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LegoSets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LegoSets",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.InsertData(
                table: "LegoSets",
                columns: new[] { "Id", "Images", "Instructions", "Name", "PartsList", "PieceCount", "SetNum" },
                values: new object[,]
                {
                    { 6, null, null, "Test Set", null, null, "123" },
                    { 7, null, null, "Another Test Set", null, null, "5678" },
                    { 8, null, null, "Yet Another Test Set", null, null, "91011593598" },
                    { 9, null, null, "", null, null, "1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LegoSets",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "LegoSets",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "LegoSets",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "LegoSets",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.InsertData(
                table: "LegoSets",
                columns: new[] { "Id", "Images", "Instructions", "Name", "PartsList", "PieceCount", "SetNum" },
                values: new object[,]
                {
                    { 1, null, null, "Test Set", null, null, "123" },
                    { 2, null, null, "Another Test Set", null, null, "5678" },
                    { 3, null, null, "Yet Another Test Set", null, null, "91011593598" },
                    { 4, null, null, "", null, null, "1" }
                });
        }
    }
}
