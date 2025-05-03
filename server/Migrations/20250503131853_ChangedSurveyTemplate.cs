using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class ChangedSurveyTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyTemplates_Contacts_ContactId",
                table: "SurveyTemplates");

            migrationBuilder.DropColumn(
                name: "StartedAt",
                table: "SurveyTemplates");

            migrationBuilder.AlterColumn<int>(
                name: "ContactId",
                table: "SurveyTemplates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "SurveyName",
                table: "SurveyTemplates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SurveyTemplateId",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastActiveTime",
                value: new DateTime(2025, 5, 3, 13, 18, 52, 420, DateTimeKind.Utc).AddTicks(8400));

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                column: "SurveyTemplateId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                column: "SurveyTemplateId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                column: "SurveyTemplateId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                column: "SurveyTemplateId",
                value: null);

            migrationBuilder.UpdateData(
                table: "SurveyTemplates",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ContactId", "SurveyName" },
                values: new object[] { null, "TextDemo" });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SurveyTemplateId",
                table: "Questions",
                column: "SurveyTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_SurveyTemplates_SurveyTemplateId",
                table: "Questions",
                column: "SurveyTemplateId",
                principalTable: "SurveyTemplates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyTemplates_Contacts_ContactId",
                table: "SurveyTemplates",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_SurveyTemplates_SurveyTemplateId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyTemplates_Contacts_ContactId",
                table: "SurveyTemplates");

            migrationBuilder.DropIndex(
                name: "IX_Questions_SurveyTemplateId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "SurveyName",
                table: "SurveyTemplates");

            migrationBuilder.DropColumn(
                name: "SurveyTemplateId",
                table: "Questions");

            migrationBuilder.AlterColumn<int>(
                name: "ContactId",
                table: "SurveyTemplates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartedAt",
                table: "SurveyTemplates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
                columns: new[] { "ContactId", "StartedAt" },
                values: new object[] { 1, new DateTime(2025, 5, 2, 14, 53, 20, 31, DateTimeKind.Utc).AddTicks(1750) });

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyTemplates_Contacts_ContactId",
                table: "SurveyTemplates",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
