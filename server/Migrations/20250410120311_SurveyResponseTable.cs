using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class SurveyResponseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SurveyResponseId",
                table: "Answers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SurveyResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SurveyId = table.Column<string>(type: "text", nullable: false),
                    ContactId = table.Column<string>(type: "text", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyResponses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_SurveyResponseId",
                table: "Answers",
                column: "SurveyResponseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_SurveyResponses_SurveyResponseId",
                table: "Answers",
                column: "SurveyResponseId",
                principalTable: "SurveyResponses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_SurveyResponses_SurveyResponseId",
                table: "Answers");

            migrationBuilder.DropTable(
                name: "SurveyResponses");

            migrationBuilder.DropIndex(
                name: "IX_Answers_SurveyResponseId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "SurveyResponseId",
                table: "Answers");
        }
    }
}
