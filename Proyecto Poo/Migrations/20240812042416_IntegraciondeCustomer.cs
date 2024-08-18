using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Poo.Migrations
{
    /// <inheritdoc />
    public partial class IntegraciondeCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_order_packages",
                schema: "dbo",
                table: "order_packages");

            migrationBuilder.AddColumn<Guid>(
                name: "customer_id",
                schema: "dbo",
                table: "orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                schema: "dbo",
                table: "order_packages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrderEntityOrderId",
                schema: "dbo",
                table: "customers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_order_packages",
                schema: "dbo",
                table: "order_packages",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_order_packages_order_id",
                schema: "dbo",
                table: "order_packages",
                column: "order_id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customers_orders_OrderEntityOrderId",
                schema: "dbo",
                table: "customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_order_packages",
                schema: "dbo",
                table: "order_packages");

            migrationBuilder.DropIndex(
                name: "IX_order_packages_order_id",
                schema: "dbo",
                table: "order_packages");

            migrationBuilder.DropIndex(
                name: "IX_customers_OrderEntityOrderId",
                schema: "dbo",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "customer_id",
                schema: "dbo",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "id",
                schema: "dbo",
                table: "order_packages");

            migrationBuilder.DropColumn(
                name: "OrderEntityOrderId",
                schema: "dbo",
                table: "customers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_order_packages",
                schema: "dbo",
                table: "order_packages",
                column: "order_id");
        }
    }
}
