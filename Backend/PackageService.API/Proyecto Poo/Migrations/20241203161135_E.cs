using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Poo.Migrations
{
    /// <inheritdoc />
    public partial class E : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "truck_id",
                schema: "dbo",
                table: "orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_orders_truck_id",
                schema: "dbo",
                table: "orders",
                column: "truck_id");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_trucks_truck_id",
                schema: "dbo",
                table: "orders",
                column: "truck_id",
                principalSchema: "dbo",
                principalTable: "trucks",
                principalColumn: "truck_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_trucks_truck_id",
                schema: "dbo",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_truck_id",
                schema: "dbo",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "truck_id",
                schema: "dbo",
                table: "orders");
        }
    }
}
