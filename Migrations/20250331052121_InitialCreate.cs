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
                name: "ImportedParts",
                columns: table => new
                {
                    part_num = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    part_cat_id = table.Column<string>(type: "TEXT", nullable: true),
                    part_material = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportedParts", x => x.part_num);
                });

            migrationBuilder.CreateTable(
                name: "ImportedSets",
                columns: table => new
                {
                    set_num = table.Column<string>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    year = table.Column<int>(type: "INTEGER", nullable: true),
                    theme_id = table.Column<string>(type: "TEXT", nullable: true),
                    num_parts = table.Column<int>(type: "INTEGER", nullable: true),
                    img_url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportedSets", x => x.set_num);
                });

            migrationBuilder.CreateTable(
                name: "DbLegoParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InvPartId = table.Column<int>(type: "INTEGER", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "ImportedInventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    version = table.Column<string>(type: "TEXT", nullable: true),
                    set_num = table.Column<string>(type: "TEXT", nullable: false),
                    set_num1 = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportedInventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportedInventories_ImportedSets_set_num",
                        column: x => x.set_num,
                        principalTable: "ImportedSets",
                        principalColumn: "set_num",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImportedInventories_ImportedSets_set_num1",
                        column: x => x.set_num1,
                        principalTable: "ImportedSets",
                        principalColumn: "set_num",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportedInventoryParts",
                columns: table => new
                {
                    inventory_id = table.Column<int>(type: "INTEGER", nullable: false),
                    part_num = table.Column<int>(type: "INTEGER", nullable: false),
                    quantity = table.Column<int>(type: "INTEGER", nullable: true),
                    InventoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    part_num1 = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportedInventoryParts", x => new { x.inventory_id, x.part_num });
                    table.ForeignKey(
                        name: "FK_ImportedInventoryParts_ImportedInventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "ImportedInventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImportedInventoryParts_ImportedInventories_inventory_id",
                        column: x => x.inventory_id,
                        principalTable: "ImportedInventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImportedInventoryParts_ImportedParts_part_num",
                        column: x => x.part_num,
                        principalTable: "ImportedParts",
                        principalColumn: "part_num",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImportedInventoryParts_ImportedParts_part_num1",
                        column: x => x.part_num1,
                        principalTable: "ImportedParts",
                        principalColumn: "part_num",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportedInventorySets",
                columns: table => new
                {
                    inventory_id = table.Column<int>(type: "INTEGER", nullable: false),
                    set_num = table.Column<string>(type: "TEXT", nullable: false),
                    quantity = table.Column<int>(type: "INTEGER", nullable: true),
                    InventoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    set_num1 = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportedInventorySets", x => new { x.inventory_id, x.set_num });
                    table.ForeignKey(
                        name: "FK_ImportedInventorySets_ImportedInventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "ImportedInventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImportedInventorySets_ImportedInventories_inventory_id",
                        column: x => x.inventory_id,
                        principalTable: "ImportedInventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImportedInventorySets_ImportedSets_set_num",
                        column: x => x.set_num,
                        principalTable: "ImportedSets",
                        principalColumn: "set_num",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImportedInventorySets_ImportedSets_set_num1",
                        column: x => x.set_num1,
                        principalTable: "ImportedSets",
                        principalColumn: "set_num",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbLegoParts_DbLegoSetId",
                table: "DbLegoParts",
                column: "DbLegoSetId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportedInventories_set_num",
                table: "ImportedInventories",
                column: "set_num");

            migrationBuilder.CreateIndex(
                name: "IX_ImportedInventories_set_num1",
                table: "ImportedInventories",
                column: "set_num1");

            migrationBuilder.CreateIndex(
                name: "IX_ImportedInventoryParts_InventoryId",
                table: "ImportedInventoryParts",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportedInventoryParts_part_num",
                table: "ImportedInventoryParts",
                column: "part_num");

            migrationBuilder.CreateIndex(
                name: "IX_ImportedInventoryParts_part_num1",
                table: "ImportedInventoryParts",
                column: "part_num1");

            migrationBuilder.CreateIndex(
                name: "IX_ImportedInventorySets_InventoryId",
                table: "ImportedInventorySets",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportedInventorySets_set_num",
                table: "ImportedInventorySets",
                column: "set_num");

            migrationBuilder.CreateIndex(
                name: "IX_ImportedInventorySets_set_num1",
                table: "ImportedInventorySets",
                column: "set_num1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbLegoParts");

            migrationBuilder.DropTable(
                name: "ImportedInventoryParts");

            migrationBuilder.DropTable(
                name: "ImportedInventorySets");

            migrationBuilder.DropTable(
                name: "DbLegoSets");

            migrationBuilder.DropTable(
                name: "ImportedParts");

            migrationBuilder.DropTable(
                name: "ImportedInventories");

            migrationBuilder.DropTable(
                name: "ImportedSets");
        }
    }
}
