using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class RemoveContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
             // 1. Drop the foreign key constraint
migrationBuilder.DropForeignKey(
    name: "FK_SurveyTemplates_Contacts_ContactId",
    table: "SurveyTemplates");

// 2. Drop the index
migrationBuilder.DropIndex(
    name: "IX_SurveyTemplates_ContactId",
    table: "SurveyTemplates");

// 3. Drop the column
migrationBuilder.DropColumn(
    name: "ContactId",
    table: "SurveyTemplates");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastActiveTime",
                value: new DateTime(2025, 5, 3, 15, 4, 56, 917, DateTimeKind.Utc).AddTicks(3850));
        }
    }
}
