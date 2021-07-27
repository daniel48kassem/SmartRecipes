using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashOrder.Migrations
{
    public partial class ItemsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Items_ItemId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_ItemId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Ingredients");

            migrationBuilder.AddColumn<int>(
                name: "IngredientId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Items_IngredientId",
                table: "Items",
                column: "IngredientId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Ingredients_IngredientId",
                table: "Items",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Ingredients_IngredientId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_IngredientId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "IngredientId",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Ingredients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_ItemId",
                table: "Ingredients",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Items_ItemId",
                table: "Ingredients",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
