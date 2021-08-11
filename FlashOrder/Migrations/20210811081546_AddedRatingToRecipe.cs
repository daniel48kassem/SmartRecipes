using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashOrder.Migrations
{
    public partial class AddedRatingToRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<bool>(
                name: "IsRatingUpdated",
                table: "Recipes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "Recipes",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "82b59b66-31b2-4228-9081-a4d632403ea1", "7df45b7a-41b5-4287-b345-7e06683d4b0f", "Chef", "Chef" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7f69764b-2de8-4851-bfd7-13f1cf2e8749", "7dda0335-ffa5-46e9-9af9-ef9c95855aad", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "09888c65-4683-4bc7-98c2-dd98397c3aa8", "decc45d9-07a0-483d-be0b-f4a81c202539", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09888c65-4683-4bc7-98c2-dd98397c3aa8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f69764b-2de8-4851-bfd7-13f1cf2e8749");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82b59b66-31b2-4228-9081-a4d632403ea1");

            migrationBuilder.DropColumn(
                name: "IsRatingUpdated",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Recipes");

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
        }
    }
}
