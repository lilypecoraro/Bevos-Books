using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Team24_BevosBooks.Migrations
{
    /// <inheritdoc />
    public partial class AddFreeShippingAllOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CouponID",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CouponID",
                table: "Orders",
                column: "CouponID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Coupons_CouponID",
                table: "Orders",
                column: "CouponID",
                principalTable: "Coupons",
                principalColumn: "CouponID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Coupons_CouponID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CouponID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CouponID",
                table: "Orders");
        }
    }
}
