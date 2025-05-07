using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class SurveyStruc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.CreateTable(
                name: "QuestionTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionNumber = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurveyTemplateId = table.Column<int>(type: "int", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    OrderInSurvey = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionTemplates_SurveyTemplates_SurveyTemplateId",
                        column: x => x.SurveyTemplateId,
                        principalTable: "SurveyTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnswerOptionTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyQuestionTemplateId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    SurveyTemplateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerOptionTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerOptionTemplates_QuestionTemplates_SurveyQuestionTemplateId",
                        column: x => x.SurveyQuestionTemplateId,
                        principalTable: "QuestionTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerOptionTemplates_SurveyTemplates_SurveyTemplateId",
                        column: x => x.SurveyTemplateId,
                        principalTable: "SurveyTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SurveyResponseAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyResponseId = table.Column<int>(type: "int", nullable: false),
                    SurveyQuestionTemplateId = table.Column<int>(type: "int", nullable: false),
                    AnswerOptionTemplateId = table.Column<int>(type: "int", nullable: true),
                    FreeTextAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnsweredAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyResponseAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyResponseAnswers_AnswerOptionTemplates_AnswerOptionTemplateId",
                        column: x => x.AnswerOptionTemplateId,
                        principalTable: "AnswerOptionTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SurveyResponseAnswers_QuestionTemplates_SurveyQuestionTemplateId",
                        column: x => x.SurveyQuestionTemplateId,
                        principalTable: "QuestionTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SurveyResponseAnswers_SurveyResponses_SurveyResponseId",
                        column: x => x.SurveyResponseId,
                        principalTable: "SurveyResponses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastActiveTime",
                value: new DateTime(2025, 5, 7, 2, 2, 48, 354, DateTimeKind.Utc).AddTicks(1340));

            migrationBuilder.CreateIndex(
                name: "IX_AnswerOptionTemplates_SurveyQuestionTemplateId",
                table: "AnswerOptionTemplates",
                column: "SurveyQuestionTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerOptionTemplates_SurveyTemplateId",
                table: "AnswerOptionTemplates",
                column: "SurveyTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTemplates_SurveyTemplateId",
                table: "QuestionTemplates",
                column: "SurveyTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponseAnswers_AnswerOptionTemplateId",
                table: "SurveyResponseAnswers",
                column: "AnswerOptionTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponseAnswers_SurveyQuestionTemplateId",
                table: "SurveyResponseAnswers",
                column: "SurveyQuestionTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponseAnswers_SurveyResponseId",
                table: "SurveyResponseAnswers",
                column: "SurveyResponseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurveyResponseAnswers");

            migrationBuilder.DropTable(
                name: "AnswerOptionTemplates");

            migrationBuilder.DropTable(
                name: "QuestionTemplates");

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    OrderInSurvey = table.Column<int>(type: "int", nullable: false),
                    QuestionNumber = table.Column<int>(type: "int", nullable: false),
                    SurveyTemplateId = table.Column<int>(type: "int", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_SurveyTemplates_SurveyTemplateId",
                        column: x => x.SurveyTemplateId,
                        principalTable: "SurveyTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    SurveyId = table.Column<int>(type: "int", nullable: false),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SurveyResponseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answers_SurveyResponses_SurveyResponseId",
                        column: x => x.SurveyResponseId,
                        principalTable: "SurveyResponses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Answers_SurveyTemplates_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "SurveyTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastActiveTime",
                value: new DateTime(2025, 5, 6, 3, 18, 32, 13, DateTimeKind.Utc).AddTicks(2450));

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "IsRequired", "OrderInSurvey", "QuestionNumber", "SurveyTemplateId", "Text", "Type" },
                values: new object[,]
                {
                    { 1, true, 1, 1, null, "What is your thoughts on the demo so far? (1 being awful and 10 being perfect)", 2 },
                    { 2, true, 2, 2, null, "Would you recommend this demo to a friend or family member?", 0 },
                    { 3, false, 3, 3, null, "What do you like so far about the demo?", 1 },
                    { 4, false, 4, 4, null, "Any thoughts or suggestions?", 3 }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "ContactId", "QuestionId", "Response", "SubmittedAt", "SurveyId", "SurveyResponseId" },
                values: new object[,]
                {
                    { 1, 1, 3, "The Text Messages", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 2, 1, 3, "The iMessage like preview", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 3, 1, 3, "The Slack Opt-in Page", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 4, 1, 3, "Ease of Use", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 5, 1, 3, "The Survey itself", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 6, 1, 3, "N/A", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_SurveyId",
                table: "Answers",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_SurveyResponseId",
                table: "Answers",
                column: "SurveyResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SurveyTemplateId",
                table: "Questions",
                column: "SurveyTemplateId");
        }
    }
}
