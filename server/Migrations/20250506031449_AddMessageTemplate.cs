using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class AddMessageTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SurveyId",
                table: "SurveyResponses",
                newName: "SurveyTemplateId");

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastActiveTime",
                value: new DateTime(2025, 5, 6, 3, 14, 48, 577, DateTimeKind.Utc).AddTicks(270));

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponses_SurveyTemplateId",
                table: "SurveyResponses",
                column: "SurveyTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyResponses_SurveyTemplates_SurveyTemplateId",
                table: "SurveyResponses",
                column: "SurveyTemplateId",
                principalTable: "SurveyTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyResponses_SurveyTemplates_SurveyTemplateId",
                table: "SurveyResponses");

            migrationBuilder.DropIndex(
                name: "IX_SurveyResponses_SurveyTemplateId",
                table: "SurveyResponses");

            migrationBuilder.RenameColumn(
                name: "SurveyTemplateId",
                table: "SurveyResponses",
                newName: "SurveyId");

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastActiveTime",
                value: new DateTime(2025, 5, 4, 17, 18, 41, 252, DateTimeKind.Utc).AddTicks(5900));
        }
    }
}
