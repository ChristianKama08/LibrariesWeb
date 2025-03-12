using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrariesWeb.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class initi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_authors_AuthorId",
                table: "books");

            migrationBuilder.DropForeignKey(
                name: "FK_issuedBooks_Users_UserId",
                table: "issuedBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_issuedBooks_books_BookId",
                table: "issuedBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_issuedBooks",
                table: "issuedBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_books",
                table: "books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_authors",
                table: "authors");

            migrationBuilder.RenameTable(
                name: "issuedBooks",
                newName: "IssuedBooks");

            migrationBuilder.RenameTable(
                name: "books",
                newName: "Books");

            migrationBuilder.RenameTable(
                name: "authors",
                newName: "Authors");

            migrationBuilder.RenameIndex(
                name: "IX_issuedBooks_UserId",
                table: "IssuedBooks",
                newName: "IX_IssuedBooks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_issuedBooks_BookId",
                table: "IssuedBooks",
                newName: "IX_IssuedBooks_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_books_AuthorId",
                table: "Books",
                newName: "IX_Books_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IssuedBooks",
                table: "IssuedBooks",
                column: "IssuedBookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "bookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors",
                table: "Authors",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IssuedBooks_Books_BookId",
                table: "IssuedBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "bookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IssuedBooks_Users_UserId",
                table: "IssuedBooks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_IssuedBooks_Books_BookId",
                table: "IssuedBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_IssuedBooks_Users_UserId",
                table: "IssuedBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IssuedBooks",
                table: "IssuedBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors",
                table: "Authors");

            migrationBuilder.RenameTable(
                name: "IssuedBooks",
                newName: "issuedBooks");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "books");

            migrationBuilder.RenameTable(
                name: "Authors",
                newName: "authors");

            migrationBuilder.RenameIndex(
                name: "IX_IssuedBooks_UserId",
                table: "issuedBooks",
                newName: "IX_issuedBooks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_IssuedBooks_BookId",
                table: "issuedBooks",
                newName: "IX_issuedBooks_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_AuthorId",
                table: "books",
                newName: "IX_books_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_issuedBooks",
                table: "issuedBooks",
                column: "IssuedBookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_books",
                table: "books",
                column: "bookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_authors",
                table: "authors",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_books_authors_AuthorId",
                table: "books",
                column: "AuthorId",
                principalTable: "authors",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_issuedBooks_Users_UserId",
                table: "issuedBooks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_issuedBooks_books_BookId",
                table: "issuedBooks",
                column: "BookId",
                principalTable: "books",
                principalColumn: "bookId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
