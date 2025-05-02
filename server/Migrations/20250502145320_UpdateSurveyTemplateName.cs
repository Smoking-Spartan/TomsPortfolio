using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSurveyTemplateName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Surveys_SurveyId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Contacts_ContactId",
                table: "Surveys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Surveys",
                table: "Surveys");

            migrationBuilder.RenameTable(
                name: "Surveys",
                newName: "SurveyTemplates");

            migrationBuilder.RenameIndex(
                name: "IX_Surveys_ContactId",
                table: "SurveyTemplates",
                newName: "IX_SurveyTemplates_ContactId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurveyTemplates",
                table: "SurveyTemplates",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastActiveTime",
                value: new DateTime(2025, 5, 2, 14, 53, 20, 30, DateTimeKind.Utc).AddTicks(4940));

            migrationBuilder.UpdateData(
                table: "SurveyTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartedAt",
                value: new DateTime(2025, 5, 2, 14, 53, 20, 31, DateTimeKind.Utc).AddTicks(1750));

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_SurveyTemplates_SurveyId",
                table: "Answers",
                column: "SurveyId",
                principalTable: "SurveyTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyTemplates_Contacts_ContactId",
                table: "SurveyTemplates",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_SurveyTemplates_SurveyId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyTemplates_Contacts_ContactId",
                table: "SurveyTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SurveyTemplates",
                table: "SurveyTemplates");

            migrationBuilder.RenameTable(
                name: "SurveyTemplates",
                newName: "Surveys");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyTemplates_ContactId",
                table: "Surveys",
                newName: "IX_Surveys_ContactId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Surveys",
                table: "Surveys",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastActiveTime",
                value: new DateTime(2025, 5, 1, 19, 24, 9, 63, DateTimeKind.Utc).AddTicks(370));

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartedAt",
                value: new DateTime(2025, 5, 1, 19, 24, 9, 63, DateTimeKind.Utc).AddTicks(2970));

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Surveys_SurveyId",
                table: "Answers",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Contacts_ContactId",
                table: "Surveys",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
