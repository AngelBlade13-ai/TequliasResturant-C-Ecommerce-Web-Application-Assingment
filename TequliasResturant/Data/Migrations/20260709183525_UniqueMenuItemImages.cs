using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TequliasResturant.Data.Migrations
{
    /// <inheritdoc />
    public partial class UniqueMenuItemImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "ImageUrl",
                value: "/images/menu/chicken-tacos.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "ImageUrl",
                value: "/images/menu/fish-tacos.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                column: "ImageUrl",
                value: "/images/menu/veggie-enchiladas.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                column: "ImageUrl",
                value: "/images/menu/carne-asada-burrito.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                column: "ImageUrl",
                value: "/images/menu/chips-salsa-trio.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10,
                column: "ImageUrl",
                value: "/images/menu/mexican-rice.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11,
                column: "ImageUrl",
                value: "/images/menu/refried-beans.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14,
                column: "ImageUrl",
                value: "/images/menu/agua-fresca-flight.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "ImageUrl",
                value: "/images/menu/tacos.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "ImageUrl",
                value: "/images/menu/tacos.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                column: "ImageUrl",
                value: "/images/menu/enchiladas.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                column: "ImageUrl",
                value: "/images/menu/tacos.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                column: "ImageUrl",
                value: "/images/menu/nachos.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10,
                column: "ImageUrl",
                value: "/images/menu/enchiladas.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11,
                column: "ImageUrl",
                value: "/images/menu/enchiladas.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14,
                column: "ImageUrl",
                value: "/images/menu/drinks.png");
        }
    }
}
