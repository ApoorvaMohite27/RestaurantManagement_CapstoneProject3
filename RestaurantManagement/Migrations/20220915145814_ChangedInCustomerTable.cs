using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantManagement.Migrations
{
    public partial class ChangedInCustomerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailId",
                table: "CustomerOrderDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MobileNumber",
                table: "CustomerOrderDetails",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailId",
                table: "CustomerOrderDetails");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "CustomerOrderDetails");
        }
    }
}
