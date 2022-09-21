using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantManagement.Migrations
{
    public partial class AddedNewCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerDetails_CustomerOrderDetails_OrderId",
                table: "CustomerDetails");

            migrationBuilder.DropIndex(
                name: "IX_CustomerDetails_OrderId",
                table: "CustomerDetails");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "CustomerDetails");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "CustomerOrderDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderDetails_CustomerId",
                table: "CustomerOrderDetails",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrderDetails_CustomerDetails_CustomerId",
                table: "CustomerOrderDetails",
                column: "CustomerId",
                principalTable: "CustomerDetails",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrderDetails_CustomerDetails_CustomerId",
                table: "CustomerOrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_CustomerOrderDetails_CustomerId",
                table: "CustomerOrderDetails");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "CustomerOrderDetails");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "CustomerDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDetails_OrderId",
                table: "CustomerDetails",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerDetails_CustomerOrderDetails_OrderId",
                table: "CustomerDetails",
                column: "OrderId",
                principalTable: "CustomerOrderDetails",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
