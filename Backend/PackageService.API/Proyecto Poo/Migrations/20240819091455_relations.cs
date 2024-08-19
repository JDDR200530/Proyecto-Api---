using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Poo.Migrations
{
    /// <inheritdoc />
    public partial class relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_payments_orders_package_id",
                schema: "dbo",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_packages_PackageEntityPackageId",
                schema: "dbo",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_payments_PackageEntityPackageId",
                schema: "dbo",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "PackageEntityPackageId",
                schema: "dbo",
                table: "payments");

            migrationBuilder.AddForeignKey(
                name: "FK_payments_packages_package_id",
                schema: "dbo",
                table: "payments",
                column: "package_id",
                principalSchema: "dbo",
                principalTable: "packages",
                principalColumn: "package_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_payments_packages_package_id",
                schema: "dbo",
                table: "payments");

            migrationBuilder.AddColumn<Guid>(
                name: "PackageEntityPackageId",
                schema: "dbo",
                table: "payments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_payments_PackageEntityPackageId",
                schema: "dbo",
                table: "payments",
                column: "PackageEntityPackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_payments_orders_package_id",
                schema: "dbo",
                table: "payments",
                column: "package_id",
                principalSchema: "dbo",
                principalTable: "orders",
                principalColumn: "order_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_packages_PackageEntityPackageId",
                schema: "dbo",
                table: "payments",
                column: "PackageEntityPackageId",
                principalSchema: "dbo",
                principalTable: "packages",
                principalColumn: "package_id");
        }
    }
}
