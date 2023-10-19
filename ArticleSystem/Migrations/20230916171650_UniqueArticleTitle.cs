using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArticleSystem.Migrations
{
    /// <inheritdoc />
    public partial class UniqueArticleTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Articles_Title",
                table: "Articles",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Articles_Title",
                table: "Articles");
        }
    }
}
