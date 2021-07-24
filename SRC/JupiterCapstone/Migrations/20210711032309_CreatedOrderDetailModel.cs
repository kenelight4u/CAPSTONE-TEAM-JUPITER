using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JupiterCapstone.Migrations
{
    public partial class CreatedOrderDetailModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    UsersAddressId = table.Column<string>(nullable: true),
                    PaymentMethod = table.Column<string>(nullable: true),
                    OrderId = table.Column<string>(nullable: true),
                    ShippingStatus = table.Column<string>(nullable: true),
                    OrderStatus = table.Column<string>(nullable: true),
                    EstimatedDeliveryTime = table.Column<DateTime>(nullable: false),
                    Confirmed = table.Column<DateTime>(nullable: false),
                    Shippped = table.Column<DateTime>(nullable: false),
                    Delivered = table.Column<DateTime>(nullable: false),
                    Cancelled = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_UsersAddresses_UsersAddressId",
                        column: x => x.UsersAddressId,
                        principalTable: "UsersAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ProductId = table.Column<string>(nullable: true),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderDetailId = table.Column<string>(nullable: true),
                    OrderId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_OrderDetails_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalTable: "OrderDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_UserId",
                table: "OrderDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_UsersAddressId",
                table: "OrderDetails",
                column: "UsersAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderDetailId",
                table: "OrderItems",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "OrderDetails");
        }
    }
}
