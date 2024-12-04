using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Poo.Migrations
{
    /// <inheritdoc />
    public partial class H : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "truck_id",
                schema: "dbo",
                table: "trucks",
                newName: "id");

            migrationBuilder.AddColumn<string>(
                name: "created_by",
                schema: "dbo",
                table: "trucks",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_date",
                schema: "dbo",
                table: "trucks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "updated_by",
                schema: "dbo",
                table: "trucks",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_date",
                schema: "dbo",
                table: "trucks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "truck_id",
                schema: "dbo",
                table: "shipments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_trucks_created_by",
                schema: "dbo",
                table: "trucks",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_trucks_updated_by",
                schema: "dbo",
                table: "trucks",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_shipments_truck_id",
                schema: "dbo",
                table: "shipments",
                column: "truck_id");

            migrationBuilder.AddForeignKey(
                name: "FK_shipments_trucks_truck_id",
                schema: "dbo",
                table: "shipments",
                column: "truck_id",
                principalSchema: "dbo",
                principalTable: "trucks",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_trucks_users_created_by",
                schema: "dbo",
                table: "trucks",
                column: "created_by",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_trucks_users_updated_by",
                schema: "dbo",
                table: "trucks",
                column: "updated_by",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shipments_trucks_truck_id",
                schema: "dbo",
                table: "shipments");

            migrationBuilder.DropForeignKey(
                name: "FK_trucks_users_created_by",
                schema: "dbo",
                table: "trucks");

            migrationBuilder.DropForeignKey(
                name: "FK_trucks_users_updated_by",
                schema: "dbo",
                table: "trucks");

            migrationBuilder.DropIndex(
                name: "IX_trucks_created_by",
                schema: "dbo",
                table: "trucks");

            migrationBuilder.DropIndex(
                name: "IX_trucks_updated_by",
                schema: "dbo",
                table: "trucks");

            migrationBuilder.DropIndex(
                name: "IX_shipments_truck_id",
                schema: "dbo",
                table: "shipments");

            migrationBuilder.DropColumn(
                name: "created_by",
                schema: "dbo",
                table: "trucks");

            migrationBuilder.DropColumn(
                name: "created_date",
                schema: "dbo",
                table: "trucks");

            migrationBuilder.DropColumn(
                name: "updated_by",
                schema: "dbo",
                table: "trucks");

            migrationBuilder.DropColumn(
                name: "updated_date",
                schema: "dbo",
                table: "trucks");

            migrationBuilder.DropColumn(
                name: "truck_id",
                schema: "dbo",
                table: "shipments");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "dbo",
                table: "trucks",
                newName: "truck_id");
        }
    }
}
