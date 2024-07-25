using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Poo.Migrations
{
    /// <inheritdoc />
    public partial class addcolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "reciverName",
                schema: "dbo",
                table: "orders",
                newName: "ReciverName");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                schema: "dbo",
                table: "orders",
                newName: "order_date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReciverName",
                schema: "dbo",
                table: "orders",
                newName: "reciverName");

            migrationBuilder.RenameColumn(
                name: "order_date",
                schema: "dbo",
                table: "orders",
                newName: "OrderDate");
        }
    }
}
