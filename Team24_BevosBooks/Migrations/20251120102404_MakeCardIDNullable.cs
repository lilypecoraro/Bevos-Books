using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Team24_BevosBooks.Migrations
{
    /// <inheritdoc />
    public partial class MakeCardIDNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Cards_CardID",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<int>(
                name: "CardID",
                table: "OrderDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Cards_CardID",
                table: "OrderDetails",
                column: "CardID",
                principalTable: "Cards",
                principalColumn: "CardID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Cards_CardID",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<int>(
                name: "CardID",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Cards_CardID",
                table: "OrderDetails",
                column: "CardID",
                principalTable: "Cards",
                principalColumn: "CardID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
