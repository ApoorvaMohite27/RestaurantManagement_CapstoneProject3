using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantManagement.Migrations
{
    public partial class ChangedInTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerOrderDetails",
                table: "CustomerOrderDetails");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "CustomerOrderDetails");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "CustomerOrderDetails");

            migrationBuilder.DropColumn(
                name: "EmailId",
                table: "CustomerOrderDetails");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "CustomerOrderDetails");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "CustomerOrderDetails",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerOrderDetails",
                table: "CustomerOrderDetails",
                column: "OrderId");

            migrationBuilder.CreateTable(
                name: "CustomerDetails",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(maxLength: 50, nullable: false),
                    EmailId = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDetails", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_CustomerDetails_CustomerOrderDetails_OrderId",
                        column: x => x.OrderId,
                        principalTable: "CustomerOrderDetails",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDetails_OrderId",
                table: "CustomerDetails",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerOrderDetails",
                table: "CustomerOrderDetails");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "CustomerOrderDetails");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "CustomerOrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "CustomerOrderDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmailId",
                table: "CustomerOrderDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MobileNumber",
                table: "CustomerOrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerOrderDetails",
                table: "CustomerOrderDetails",
                column: "CustomerId");
        }
    }
}
