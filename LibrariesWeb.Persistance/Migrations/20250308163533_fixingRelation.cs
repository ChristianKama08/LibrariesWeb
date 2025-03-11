using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrariesWeb.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class fixingRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_authors_books_BookId",
                table: "authors");

            migrationBuilder.DropIndex(
                name: "IX_authors_BookId",
                table: "authors");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "authors");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "books",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_books_AuthorId",
                table: "books",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_books_authors_AuthorId",
                table: "books",
                column: "AuthorId",
                principalTable: "authors",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_authors_AuthorId",
                table: "books");

            migrationBuilder.DropIndex(
                name: "IX_books_AuthorId",
                table: "books");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "books");

            migrationBuilder.AddColumn<Guid>(
                name: "BookId",
                table: "authors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_authors_BookId",
                table: "authors",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_authors_books_BookId",
                table: "authors",
                column: "BookId",
                principalTable: "books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
