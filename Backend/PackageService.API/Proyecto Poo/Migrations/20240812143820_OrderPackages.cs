using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Poo.Migrations
{
    /// <inheritdoc />
    public partial class OrderPackages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                schema: "dbo",
                table: "packages");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "dbo",
                table: "orders");
        }
    }
}
