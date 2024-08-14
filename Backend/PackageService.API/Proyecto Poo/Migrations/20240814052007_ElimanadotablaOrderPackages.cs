using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Poo.Migrations
{
    /// <inheritdoc />
    public partial class ElimanadotablaOrderPackages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_packages",
                schema: "dbo");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "dbo",
                table: "packages");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "dbo",
                table: "orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "dbo",
                table: "packages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "dbo",
                table: "orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "order_packages",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    package_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_packages", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_packages_orders_order_id",
                        column: x => x.order_id,
                        principalSchema: "dbo",
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_packages_packages_package_id",
                        column: x => x.package_id,
                        principalSchema: "dbo",
                        principalTable: "packages",
                        principalColumn: "package_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_packages_order_id",
                schema: "dbo",
                table: "order_packages",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_packages_package_id",
                schema: "dbo",
                table: "order_packages",
                column: "package_id");
        }
    }
}
