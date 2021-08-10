using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashOrder.Migrations
{
    public partial class RatingRelationAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21c39015-b001-4bc3-863b-05f99a759a1c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f4b634a-13f9-4439-80a5-5878a7db58b8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d96fed85-5484-4b14-9c2c-6f82e3968771");

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => new { x.RecipeId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Ratings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ratings_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "679420be-7246-4d5a-bd62-4a3b1eb37c3d", "e52c1731-9f53-48e0-8d07-9b488ccac0dd", "Chef", "Chef" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "40d588dd-c7f8-48e7-9036-de0797d348a0", "07a4aef6-2f4b-4708-a9a1-140bd0cd2a21", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2fc16313-b8fe-4063-8304-44ccaf88a3f7", "2d4e5991-6414-4324-aebf-afeae0e1cde3", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2fc16313-b8fe-4063-8304-44ccaf88a3f7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40d588dd-c7f8-48e7-9036-de0797d348a0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "679420be-7246-4d5a-bd62-4a3b1eb37c3d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d96fed85-5484-4b14-9c2c-6f82e3968771", "91d751fd-62be-4cc2-b3e7-e78073206a0f", "Chef", "Chef" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4f4b634a-13f9-4439-80a5-5878a7db58b8", "6b64782b-d7dc-4888-b414-ac5b4fd860fa", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "21c39015-b001-4bc3-863b-05f99a759a1c", "59f6f3b3-f06b-40c5-baa6-43312df5b532", "Administrator", "ADMINISTRATOR" });
        }
    }
}
