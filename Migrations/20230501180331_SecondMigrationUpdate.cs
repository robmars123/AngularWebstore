using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactWebstore.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigrationUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_Product_Id",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_Category_Id",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Subcategories_Subcategory_Id",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Category_Id",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Subcategory_Id",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_Product_Id",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "Product_Id",
                table: "ProductImages");

            migrationBuilder.RenameColumn(
                name: "Subcategory_Id",
                table: "Products",
                newName: "Subcategory_id");

            migrationBuilder.RenameColumn(
                name: "Category_Id",
                table: "Products",
                newName: "Category_id");

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateSent = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.RenameColumn(
                name: "Subcategory_id",
                table: "Products",
                newName: "Subcategory_Id");

            migrationBuilder.RenameColumn(
                name: "Category_id",
                table: "Products",
                newName: "Category_Id");

            migrationBuilder.AddColumn<int>(
                name: "Product_Id",
                table: "ProductImages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Category_Id",
                table: "Products",
                column: "Category_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Subcategory_Id",
                table: "Products",
                column: "Subcategory_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_Product_Id",
                table: "ProductImages",
                column: "Product_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_Product_Id",
                table: "ProductImages",
                column: "Product_Id",
                principalTable: "Products",
                principalColumn: "Product_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_Category_Id",
                table: "Products",
                column: "Category_Id",
                principalTable: "Categories",
                principalColumn: "Category_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Subcategories_Subcategory_Id",
                table: "Products",
                column: "Subcategory_Id",
                principalTable: "Subcategories",
                principalColumn: "Subcategory_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
