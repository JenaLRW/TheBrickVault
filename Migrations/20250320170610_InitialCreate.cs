using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheBrickVault.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DbLegoSets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    SetNum = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Images = table.Column<string>(type: "TEXT", nullable: true),
                    PieceCount = table.Column<int>(type: "INTEGER", nullable: true),
                    Instructions = table.Column<int>(type: "INTEGER", nullable: true),
                    PartsList = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbLegoSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DbLegoParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SetNum = table.Column<string>(type: "TEXT", nullable: false),
                    PartNum = table.Column<string>(type: "TEXT", nullable: true),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: true),
                    DbLegoSetId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbLegoParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DbLegoParts_DbLegoSets_DbLegoSetId",
                        column: x => x.DbLegoSetId,
                        principalTable: "DbLegoSets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbLegoParts_DbLegoSetId",
                table: "DbLegoParts",
                column: "DbLegoSetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbLegoParts");

            migrationBuilder.DropTable(
                name: "DbLegoSets");
        }
    }
}
