using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashOrder.Migrations
{
    public partial class AddedRoleConfigurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2645fb6c-e155-4523-a862-acc05e9c364e", "f2475aee-5619-40d2-bbc3-a119eebbdab4", "Chef", "Chef" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ef3bcc64-70b7-4b2f-9bde-91bbd60a5cd1", "e004ac34-45d9-4c8e-ab69-8ee1532aca65", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bd2bc695-cbe5-46fb-a647-da4fd93591b1", "741d7777-b237-4c9d-8583-4a406cdb51c8", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2645fb6c-e155-4523-a862-acc05e9c364e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd2bc695-cbe5-46fb-a647-da4fd93591b1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ef3bcc64-70b7-4b2f-9bde-91bbd60a5cd1");
        }
    }
}
