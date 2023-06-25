using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace DigitalEducation.Migrations
{
    /// <inheritdoc />
    public partial class Addtrainerstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrainerId",
                table: "Theory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ProblemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainers_Problems_ProblemId",
                        column: x => x.ProblemId,
                        principalTable: "Problems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TrainerTheories",
                columns: table => new
                {
                    TrainerId = table.Column<int>(type: "int", nullable: false),
                    TheoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerTheories", x => new { x.TheoryId, x.TrainerId });
                    table.ForeignKey(
                        name: "FK_TrainerTheories_Theory_TheoryId",
                        column: x => x.TheoryId,
                        principalTable: "Theory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainerTheories_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Theory_TrainerId",
                table: "Theory",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_ProblemId",
                table: "Trainers",
                column: "ProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerTheories_TrainerId",
                table: "TrainerTheories",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Theory_Trainers_TrainerId",
                table: "Theory",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Theory_Trainers_TrainerId",
                table: "Theory");

            migrationBuilder.DropTable(
                name: "TrainerTheories");

            migrationBuilder.DropTable(
                name: "Trainers");

            migrationBuilder.DropIndex(
                name: "IX_Theory_TrainerId",
                table: "Theory");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "Theory");
        }
    }
}
