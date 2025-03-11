using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrariesWeb.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class fixBookId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "books",
                newName: "bookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "bookId",
                table: "books",
                newName: "Id");
        }
    }
}
