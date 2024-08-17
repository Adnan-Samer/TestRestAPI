using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestRestAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddITemsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_categories_CategoryId",
                table: "Item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Item",
                table: "Item");

            migrationBuilder.RenameTable(
                name: "Item",
                newName: "items");

            migrationBuilder.RenameIndex(
                name: "IX_Item_CategoryId",
                table: "items",
                newName: "IX_items_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_items",
                table: "items",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_items_categories_CategoryId",
                table: "items",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_items_categories_CategoryId",
                table: "items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_items",
                table: "items");

            migrationBuilder.RenameTable(
                name: "items",
                newName: "Item");

            migrationBuilder.RenameIndex(
                name: "IX_items_CategoryId",
                table: "Item",
                newName: "IX_Item_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Item",
                table: "Item",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_categories_CategoryId",
                table: "Item",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
