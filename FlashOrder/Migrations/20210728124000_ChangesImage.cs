using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashOrder.Migrations
{
    public partial class ChangesImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Images_ThumbnailId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "ThumbnailId",
                table: "Items",
                newName: "ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_ThumbnailId",
                table: "Items",
                newName: "IX_Items_ImageId");

            migrationBuilder.AddColumn<string>(
                name: "path",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Images_ImageId",
                table: "Items",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Images_ImageId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "path",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Items",
                newName: "ThumbnailId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_ImageId",
                table: "Items",
                newName: "IX_Items_ThumbnailId");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Images",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Images_ThumbnailId",
                table: "Items",
                column: "ThumbnailId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
