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
                name: "LegoSets",
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
                    table.PrimaryKey("PK_LegoSets", x => x.Id);
                    table.UniqueConstraint("AK_LegoSets_SetNum", x => x.SetNum);
                });

            migrationBuilder.CreateTable(
                name: "LegoParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SetNum = table.Column<string>(type: "TEXT", nullable: false),
                    PartNum = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegoParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegoParts_LegoSets_SetNum",
                        column: x => x.SetNum,
                        principalTable: "LegoSets",
                        principalColumn: "SetNum",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LegoParts_SetNum",
                table: "LegoParts",
                column: "SetNum");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LegoParts");

            migrationBuilder.DropTable(
                name: "LegoSets");
        }
    }
}
