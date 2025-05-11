using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class FixedContactSurveyResp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastActiveTime",
                value: new DateTime(2025, 5, 9, 14, 4, 34, 346, DateTimeKind.Utc).AddTicks(8790));

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponses_ContactId",
                table: "SurveyResponses",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyResponses_Contacts_ContactId",
                table: "SurveyResponses",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyResponses_Contacts_ContactId",
                table: "SurveyResponses");

            migrationBuilder.DropIndex(
                name: "IX_SurveyResponses_ContactId",
                table: "SurveyResponses");

            migrationBuilder.AddColumn<int>(
                name: "ContactId",
                table: "SurveyTemplates",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastActiveTime",
                value: new DateTime(2025, 5, 9, 12, 0, 17, 269, DateTimeKind.Utc).AddTicks(9180));
        }
    }
}
