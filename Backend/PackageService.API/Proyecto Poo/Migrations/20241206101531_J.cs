using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Poo.Migrations
{
    /// <inheritdoc />
    public partial class J : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "total_weight",
                schema: "dbo",
                table: "orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "total_weight",
                schema: "dbo",
                table: "orders");
        }
    }
}
