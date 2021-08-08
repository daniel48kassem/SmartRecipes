using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashOrder.Migrations
{
    public partial class AddDbSetFollow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Follow_AspNetUsers_ChefId",
                table: "Follow");

            migrationBuilder.DropForeignKey(
                name: "FK_Follow_AspNetUsers_FollowerId",
                table: "Follow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Follow",
                table: "Follow");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5721d016-25cc-417a-b32e-eeb17564f245");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ad1818b-d421-4051-9e02-a90a8b868744");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6bcb6741-bdb0-4000-af43-eca83683a756");

            migrationBuilder.RenameTable(
                name: "Follow",
                newName: "Follows");

            migrationBuilder.RenameIndex(
                name: "IX_Follow_FollowerId",
                table: "Follows",
                newName: "IX_Follows_FollowerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Follows",
                table: "Follows",
                columns: new[] { "ChefId", "FollowerId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_AspNetUsers_ChefId",
                table: "Follows",
                column: "ChefId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_AspNetUsers_FollowerId",
                table: "Follows",
                column: "FollowerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Follows_AspNetUsers_ChefId",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_Follows_AspNetUsers_FollowerId",
                table: "Follows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Follows",
                table: "Follows");

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

            migrationBuilder.RenameTable(
                name: "Follows",
                newName: "Follow");

            migrationBuilder.RenameIndex(
                name: "IX_Follows_FollowerId",
                table: "Follow",
                newName: "IX_Follow_FollowerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Follow",
                table: "Follow",
                columns: new[] { "ChefId", "FollowerId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6bcb6741-bdb0-4000-af43-eca83683a756", "41270f3a-bc50-4dbc-b592-f24619426cee", "Chef", "Chef" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5721d016-25cc-417a-b32e-eeb17564f245", "d0cda8b3-f280-46df-8661-8d27f54f22ad", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6ad1818b-d421-4051-9e02-a90a8b868744", "80524914-4b5d-4915-b01b-75fad14c6c35", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.AddForeignKey(
                name: "FK_Follow_AspNetUsers_ChefId",
                table: "Follow",
                column: "ChefId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Follow_AspNetUsers_FollowerId",
                table: "Follow",
                column: "FollowerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
