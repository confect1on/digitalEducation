using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace DigitalEducation.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTheories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageFiles_Theory_TheoryId",
                table: "ImageFiles");

            migrationBuilder.DropIndex(
                name: "IX_ImageFiles_TheoryId",
                table: "ImageFiles");

            migrationBuilder.DropColumn(
                name: "TheoryId",
                table: "ImageFiles");

            migrationBuilder.CreateTable(
                name: "TheoryImageFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TheoryId = table.Column<int>(type: "int", nullable: false),
                    ImageFileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheoryImageFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TheoryImageFiles_ImageFiles_ImageFileId",
                        column: x => x.ImageFileId,
                        principalTable: "ImageFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TheoryImageFiles_Theory_TheoryId",
                        column: x => x.TheoryId,
                        principalTable: "Theory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TheoryImageFiles_ImageFileId",
                table: "TheoryImageFiles",
                column: "ImageFileId");

            migrationBuilder.CreateIndex(
                name: "IX_TheoryImageFiles_TheoryId",
                table: "TheoryImageFiles",
                column: "TheoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TheoryImageFiles");

            migrationBuilder.AddColumn<int>(
                name: "TheoryId",
                table: "ImageFiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImageFiles_TheoryId",
                table: "ImageFiles",
                column: "TheoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageFiles_Theory_TheoryId",
                table: "ImageFiles",
                column: "TheoryId",
                principalTable: "Theory",
                principalColumn: "Id");
        }
    }
}
