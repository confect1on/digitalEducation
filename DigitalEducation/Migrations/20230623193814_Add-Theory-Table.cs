using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace DigitalEducation.Migrations
{
    /// <inheritdoc />
    public partial class AddTheoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TheoryId",
                table: "ImageFiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Theory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Theme = table.Column<string>(type: "longtext", nullable: false),
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    SubsectionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Theory_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Theory_Subsections_SubsectionId",
                        column: x => x.SubsectionId,
                        principalTable: "Subsections",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ImageFiles_TheoryId",
                table: "ImageFiles",
                column: "TheoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Theory_SectionId",
                table: "Theory",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Theory_SubsectionId",
                table: "Theory",
                column: "SubsectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageFiles_Theory_TheoryId",
                table: "ImageFiles",
                column: "TheoryId",
                principalTable: "Theory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageFiles_Theory_TheoryId",
                table: "ImageFiles");

            migrationBuilder.DropTable(
                name: "Theory");

            migrationBuilder.DropIndex(
                name: "IX_ImageFiles_TheoryId",
                table: "ImageFiles");

            migrationBuilder.DropColumn(
                name: "TheoryId",
                table: "ImageFiles");
        }
    }
}
