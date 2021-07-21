using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopIt.Migrations
{
    public partial class fixBasket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddedProducts_AspNetUsers_UserId",
                table: "AddedProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_AddedProducts_Products_ProductId",
                table: "AddedProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddedProducts",
                table: "AddedProducts");

            migrationBuilder.RenameTable(
                name: "AddedProducts",
                newName: "Basket");

            migrationBuilder.RenameIndex(
                name: "IX_AddedProducts_UserId",
                table: "Basket",
                newName: "IX_Basket_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AddedProducts_ProductId",
                table: "Basket",
                newName: "IX_Basket_ProductId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Basket",
                table: "Basket",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_AspNetUsers_UserId",
                table: "Basket",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_Products_ProductId",
                table: "Basket",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_AspNetUsers_UserId",
                table: "Basket");

            migrationBuilder.DropForeignKey(
                name: "FK_Basket_Products_ProductId",
                table: "Basket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Basket",
                table: "Basket");

            migrationBuilder.RenameTable(
                name: "Basket",
                newName: "AddedProducts");

            migrationBuilder.RenameIndex(
                name: "IX_Basket_UserId",
                table: "AddedProducts",
                newName: "IX_AddedProducts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Basket_ProductId",
                table: "AddedProducts",
                newName: "IX_AddedProducts_ProductId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddedProducts",
                table: "AddedProducts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AddedProducts_AspNetUsers_UserId",
                table: "AddedProducts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddedProducts_Products_ProductId",
                table: "AddedProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
