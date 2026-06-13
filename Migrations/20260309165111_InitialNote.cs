using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkVault.Migrations
{
    /// <inheritdoc />
    public partial class InitialNote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Categories_CategoryID",
                table: "Notes");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Notes",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_CategoryID",
                table: "Notes",
                newName: "IX_Notes_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Categories_CategoryId",
                table: "Notes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Categories_CategoryId",
                table: "Notes");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Notes",
                newName: "CategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_CategoryId",
                table: "Notes",
                newName: "IX_Notes_CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Categories_CategoryID",
                table: "Notes",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
