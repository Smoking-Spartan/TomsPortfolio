using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class QuestionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "QuestionTemplates",
                newName: "QuestionTypeID");

            migrationBuilder.CreateTable(
                name: "QuestionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InputControl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllowOptions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFreeText = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTypes", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastActiveTime",
                value: new DateTime(2025, 5, 7, 13, 52, 26, 798, DateTimeKind.Utc).AddTicks(8760));

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTemplates_QuestionTypeID",
                table: "QuestionTemplates",
                column: "QuestionTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTemplates_QuestionTypes_QuestionTypeID",
                table: "QuestionTemplates",
                column: "QuestionTypeID",
                principalTable: "QuestionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTemplates_QuestionTypes_QuestionTypeID",
                table: "QuestionTemplates");

            migrationBuilder.DropTable(
                name: "QuestionTypes");

            migrationBuilder.DropIndex(
                name: "IX_QuestionTemplates_QuestionTypeID",
                table: "QuestionTemplates");

            migrationBuilder.RenameColumn(
                name: "QuestionTypeID",
                table: "QuestionTemplates",
                newName: "Type");

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastActiveTime",
                value: new DateTime(2025, 5, 7, 2, 2, 48, 354, DateTimeKind.Utc).AddTicks(1340));
        }
    }
}
