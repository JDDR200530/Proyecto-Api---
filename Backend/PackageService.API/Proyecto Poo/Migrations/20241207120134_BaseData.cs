using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Poo.Migrations
{
    /// <inheritdoc />
    public partial class BaseData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_payments_packages_package_id",
                schema: "dbo",
                table: "payments");

            migrationBuilder.RenameColumn(
                name: "amount",
                schema: "dbo",
                table: "payments",
                newName: "Amount");

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

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                schema: "dbo",
                table: "payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "distance",
                schema: "dbo",
                table: "orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "payment_status",
                schema: "dbo",
                table: "orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_orders_order_id",
                schema: "dbo",
                table: "payments",
                column: "order_id",
                principalSchema: "dbo",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_payments_orders_order_id",
                schema: "dbo",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                schema: "dbo",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "distance",
                schema: "dbo",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "payment_status",
                schema: "dbo",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "Amount",
                schema: "dbo",
                table: "payments",
                newName: "amount");

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
                name: "FK_payments_packages_package_id",
                schema: "dbo",
                table: "payments",
                column: "package_id",
                principalSchema: "dbo",
                principalTable: "packages",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
