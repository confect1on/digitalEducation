using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalEducation.Migrations
{
    /// <inheritdoc />
    public partial class Addhinttoproblem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Theory_Trainers_TrainerId",
                table: "Theory");

            migrationBuilder.DropIndex(
                name: "IX_Theory_TrainerId",
                table: "Theory");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "Theory");

            migrationBuilder.AddColumn<int>(
                name: "HintImageFileId",
                table: "Problems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Problems_HintImageFileId",
                table: "Problems",
                column: "HintImageFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Problems_ImageFiles_HintImageFileId",
                table: "Problems",
                column: "HintImageFileId",
                principalTable: "ImageFiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Problems_ImageFiles_HintImageFileId",
                table: "Problems");

            migrationBuilder.DropIndex(
                name: "IX_Problems_HintImageFileId",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "HintImageFileId",
                table: "Problems");

            migrationBuilder.AddColumn<int>(
                name: "TrainerId",
                table: "Theory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Theory_TrainerId",
                table: "Theory",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Theory_Trainers_TrainerId",
                table: "Theory",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id");
        }
    }
}
