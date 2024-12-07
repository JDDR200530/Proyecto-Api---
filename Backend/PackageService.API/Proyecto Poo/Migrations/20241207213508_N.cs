using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Poo.Migrations
{
    /// <inheritdoc />
    public partial class N : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shipments_packages_Package_id",
                schema: "dbo",
                table: "shipments");

            migrationBuilder.DropIndex(
                name: "IX_shipments_Package_id",
                schema: "dbo",
                table: "shipments");

            migrationBuilder.RenameColumn(
                name: "Package_id",
                schema: "dbo",
                table: "shipments",
                newName: "package_id");

            migrationBuilder.CreateIndex(
                name: "IX_shipments_package_id",
                schema: "dbo",
                table: "shipments",
                column: "package_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_shipments_packages_package_id",
                schema: "dbo",
                table: "shipments",
                column: "package_id",
                principalSchema: "dbo",
                principalTable: "packages",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shipments_packages_package_id",
                schema: "dbo",
                table: "shipments");

            migrationBuilder.DropIndex(
                name: "IX_shipments_package_id",
                schema: "dbo",
                table: "shipments");

            migrationBuilder.RenameColumn(
                name: "package_id",
                schema: "dbo",
                table: "shipments",
                newName: "Package_id");

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
    }
}
