using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Poo.Migrations
{
    /// <inheritdoc />
    public partial class I : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_trucks_TruckEntityTruckId",
                schema: "dbo",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_TruckEntityTruckId",
                schema: "dbo",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "TruckEntityTruckId",
                schema: "dbo",
                table: "orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TruckEntityTruckId",
                schema: "dbo",
                table: "orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_TruckEntityTruckId",
                schema: "dbo",
                table: "orders",
                column: "TruckEntityTruckId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_trucks_TruckEntityTruckId",
                schema: "dbo",
                table: "orders",
                column: "TruckEntityTruckId",
                principalSchema: "dbo",
                principalTable: "trucks",
                principalColumn: "truck_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
