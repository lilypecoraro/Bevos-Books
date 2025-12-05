using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Team24_BevosBooks.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFreeShippingAllOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FreeShippingAllOrders",
                table: "Coupons",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FreeShippingAllOrders",
                table: "Coupons");
        }
    }
}
