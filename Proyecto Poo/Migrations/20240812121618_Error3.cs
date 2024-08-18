using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Poo.Migrations
{
    /// <inheritdoc />
    public partial class Error3 : Migration
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
