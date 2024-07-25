using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Poo.Migrations
{
    /// <inheritdoc />
    public partial class relaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PaymentEntityPaymentId",
                schema: "dbo",
                table: "packages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sender_name",
                schema: "dbo",
                table: "orders",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "reciver_name",
                schema: "dbo",
                table: "orders",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "adress",
                schema: "dbo",
                table: "orders",
                type: "nvarchar(350)",
                maxLength: 350,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "paymets",
                schema: "dbo",
                columns: table => new
                {
                    payment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    amount = table.Column<double>(type: "float", maxLength: 250, nullable: false),
                    payment_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    payment_method = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymets", x => x.payment_id);
                    table.ForeignKey(
                        name: "FK_paymets_orders_order_id",
                        column: x => x.order_id,
                        principalSchema: "dbo",
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trucks",
                schema: "dbo",
                columns: table => new
                {
                    truck_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    truck_available = table.Column<bool>(type: "bit", nullable: false),
                    truck_capacity = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trucks", x => x.truck_id);
                });

            migrationBuilder.CreateTable(
                name: "shipments",
                schema: "dbo",
                columns: table => new
                {
                    shipment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    payment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    truck_available = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    shipped = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipments", x => x.shipment_id);
                    table.ForeignKey(
                        name: "FK_shipments_paymets_payment_id",
                        column: x => x.payment_id,
                        principalSchema: "dbo",
                        principalTable: "paymets",
                        principalColumn: "payment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shipments_trucks_truck_available",
                        column: x => x.truck_available,
                        principalSchema: "dbo",
                        principalTable: "trucks",
                        principalColumn: "truck_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_shipments",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    shipment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_shipments", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_shipments_orders_order_id",
                        column: x => x.order_id,
                        principalSchema: "dbo",
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_shipments_shipments_shipment_id",
                        column: x => x.shipment_id,
                        principalSchema: "dbo",
                        principalTable: "shipments",
                        principalColumn: "shipment_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_packages_PaymentEntityPaymentId",
                schema: "dbo",
                table: "packages",
                column: "PaymentEntityPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_order_shipments_order_id",
                schema: "dbo",
                table: "order_shipments",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_shipments_shipment_id",
                schema: "dbo",
                table: "order_shipments",
                column: "shipment_id");

            migrationBuilder.CreateIndex(
                name: "IX_paymets_order_id",
                schema: "dbo",
                table: "paymets",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_shipments_payment_id",
                schema: "dbo",
                table: "shipments",
                column: "payment_id");

            migrationBuilder.CreateIndex(
                name: "IX_shipments_truck_available",
                schema: "dbo",
                table: "shipments",
                column: "truck_available");

            migrationBuilder.AddForeignKey(
                name: "FK_packages_paymets_PaymentEntityPaymentId",
                schema: "dbo",
                table: "packages",
                column: "PaymentEntityPaymentId",
                principalSchema: "dbo",
                principalTable: "paymets",
                principalColumn: "payment_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_packages_paymets_PaymentEntityPaymentId",
                schema: "dbo",
                table: "packages");

            migrationBuilder.DropTable(
                name: "order_shipments",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "shipments",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "paymets",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "trucks",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_packages_PaymentEntityPaymentId",
                schema: "dbo",
                table: "packages");

            migrationBuilder.DropColumn(
                name: "PaymentEntityPaymentId",
                schema: "dbo",
                table: "packages");

            migrationBuilder.AlterColumn<string>(
                name: "sender_name",
                schema: "dbo",
                table: "orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "reciver_name",
                schema: "dbo",
                table: "orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "adress",
                schema: "dbo",
                table: "orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(350)",
                oldMaxLength: 350);
        }
    }
}
