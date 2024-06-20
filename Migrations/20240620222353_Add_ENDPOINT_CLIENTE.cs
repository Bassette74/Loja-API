using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loja.Migrations
{
    /// <inheritdoc />
    public partial class Add_ENDPOINT_CLIENTE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Clientes",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Clientes",
                newName: "id");
        }
    }
}
