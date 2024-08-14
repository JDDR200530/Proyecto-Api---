using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Poo.Migrations
{
    /// <inheritdoc />
    public partial class Actualizacion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customers_orders_OrderEntityOrderId",
                schema: "dbo",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "IX_customers_OrderEntityOrderId",
                schema: "dbo",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "OrderEntityOrderId",
                schema: "dbo",
                table: "customers");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerEntityCustomerId",
                schema: "dbo",
                table: "orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_CustomerEntityCustomerId",
                schema: "dbo",
                table: "orders",
                column: "CustomerEntityCustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_customers_CustomerEntityCustomerId",
                schema: "dbo",
                table: "orders",
                column: "CustomerEntityCustomerId",
                principalSchema: "dbo",
                principalTable: "customers",
                principalColumn: "customer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_customers_CustomerEntityCustomerId",
                schema: "dbo",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_CustomerEntityCustomerId",
                schema: "dbo",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "CustomerEntityCustomerId",
                schema: "dbo",
                table: "orders");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderEntityOrderId",
                schema: "dbo",
                table: "customers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_customers_OrderEntityOrderId",
                schema: "dbo",
                table: "customers",
                column: "OrderEntityOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_customers_orders_OrderEntityOrderId",
                schema: "dbo",
                table: "customers",
                column: "OrderEntityOrderId",
                principalSchema: "dbo",
                principalTable: "orders",
                principalColumn: "order_id");
        }
    }
}
