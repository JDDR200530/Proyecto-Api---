using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Poo.Migrations
{
    /// <inheritdoc />
    public partial class fixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_payments_orders_order_id",
                schema: "dbo",
                table: "payments");

            migrationBuilder.RenameColumn(
                name: "order_id",
                schema: "dbo",
                table: "payments",
                newName: "package_id");

            migrationBuilder.RenameIndex(
                name: "IX_payments_order_id",
                schema: "dbo",
                table: "payments",
                newName: "IX_payments_package_id");

            migrationBuilder.AddForeignKey(
                name: "FK_payments_orders_package_id",
                schema: "dbo",
                table: "payments",
                column: "package_id",
                principalSchema: "dbo",
                principalTable: "orders",
                principalColumn: "order_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_payments_orders_package_id",
                schema: "dbo",
                table: "payments");

            migrationBuilder.RenameColumn(
                name: "package_id",
                schema: "dbo",
                table: "payments",
                newName: "order_id");

            migrationBuilder.RenameIndex(
                name: "IX_payments_package_id",
                schema: "dbo",
                table: "payments",
                newName: "IX_payments_order_id");

            migrationBuilder.AddForeignKey(
                name: "FK_payments_orders_order_id",
                schema: "dbo",
                table: "payments",
                column: "order_id",
                principalSchema: "dbo",
                principalTable: "orders",
                principalColumn: "order_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
