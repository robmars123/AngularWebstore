using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactWebstore.Migrations
{
    /// <inheritdoc />
    public partial class AddedProductImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Category_id",
                table: "Products",
                column: "Category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Subcategory_id",
                table: "Products",
                column: "Subcategory_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_Category_id",
                table: "Products",
                column: "Category_id",
                principalTable: "Categories",
                principalColumn: "Category_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Subcategories_Subcategory_id",
                table: "Products",
                column: "Subcategory_id",
                principalTable: "Subcategories",
                principalColumn: "Subcategory_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_Category_id",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Subcategories_Subcategory_id",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Category_id",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Subcategory_id",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductImages");
        }
    }
}
