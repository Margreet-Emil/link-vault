using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkVault.Migrations
{
    /// <inheritdoc />
    public partial class addbookmark : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_Categories_CategoryID",
                table: "Bookmark");

            migrationBuilder.DropForeignKey(
                name: "FK_BookmarkNote_Bookmark_BookmarkId",
                table: "BookmarkNote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookmarkNote",
                table: "BookmarkNote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookmark",
                table: "Bookmark");

            migrationBuilder.RenameTable(
                name: "BookmarkNote",
                newName: "BookmarkNotes");

            migrationBuilder.RenameTable(
                name: "Bookmark",
                newName: "Bookmarks");

            migrationBuilder.RenameIndex(
                name: "IX_BookmarkNote_BookmarkId",
                table: "BookmarkNotes",
                newName: "IX_BookmarkNotes_BookmarkId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookmark_Url",
                table: "Bookmarks",
                newName: "IX_Bookmarks_Url");

            migrationBuilder.RenameIndex(
                name: "IX_Bookmark_CategoryID",
                table: "Bookmarks",
                newName: "IX_Bookmarks_CategoryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookmarkNotes",
                table: "BookmarkNotes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookmarks",
                table: "Bookmarks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookmarkNotes_Bookmarks_BookmarkId",
                table: "BookmarkNotes",
                column: "BookmarkId",
                principalTable: "Bookmarks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmarks_Categories_CategoryID",
                table: "Bookmarks",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookmarkNotes_Bookmarks_BookmarkId",
                table: "BookmarkNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookmarks_Categories_CategoryID",
                table: "Bookmarks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookmarks",
                table: "Bookmarks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookmarkNotes",
                table: "BookmarkNotes");

            migrationBuilder.RenameTable(
                name: "Bookmarks",
                newName: "Bookmark");

            migrationBuilder.RenameTable(
                name: "BookmarkNotes",
                newName: "BookmarkNote");

            migrationBuilder.RenameIndex(
                name: "IX_Bookmarks_Url",
                table: "Bookmark",
                newName: "IX_Bookmark_Url");

            migrationBuilder.RenameIndex(
                name: "IX_Bookmarks_CategoryID",
                table: "Bookmark",
                newName: "IX_Bookmark_CategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_BookmarkNotes_BookmarkId",
                table: "BookmarkNote",
                newName: "IX_BookmarkNote_BookmarkId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookmark",
                table: "Bookmark",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookmarkNote",
                table: "BookmarkNote",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmark_Categories_CategoryID",
                table: "Bookmark",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookmarkNote_Bookmark_BookmarkId",
                table: "BookmarkNote",
                column: "BookmarkId",
                principalTable: "Bookmark",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
