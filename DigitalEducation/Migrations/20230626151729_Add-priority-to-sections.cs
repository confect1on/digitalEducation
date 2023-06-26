using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalEducation.Migrations
{
    /// <inheritdoc />
    public partial class Addprioritytosections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Sections",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Sections");
        }
    }
}
