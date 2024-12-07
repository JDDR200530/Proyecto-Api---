using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Poo.Migrations
{
    /// <inheritdoc />
    public partial class Shipments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Package_id",
                schema: "dbo",
                table: "shipments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_shipments_Package_id",
                schema: "dbo",
                table: "shipments",
                column: "Package_id");

            migrationBuilder.AddForeignKey(
                name: "FK_shipments_packages_Package_id",
                schema: "dbo",
                table: "shipments",
                column: "Package_id",
                principalSchema: "dbo",
                principalTable: "packages",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shipments_packages_Package_id",
                schema: "dbo",
                table: "shipments");

            migrationBuilder.DropIndex(
                name: "IX_shipments_Package_id",
                schema: "dbo",
                table: "shipments");

            migrationBuilder.DropColumn(
                name: "Package_id",
                schema: "dbo",
                table: "shipments");
        }
    }
}
