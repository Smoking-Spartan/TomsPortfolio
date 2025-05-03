using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class ForeignRelationToMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MessageTypeID",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastActiveTime",
                value: new DateTime(2025, 5, 3, 22, 23, 30, 743, DateTimeKind.Utc).AddTicks(2310));

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MessageTypeID",
                table: "Messages",
                column: "MessageTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MessageTypes_MessageTypeID",
                table: "Messages",
                column: "MessageTypeID",
                principalTable: "MessageTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MessageTypes_MessageTypeID",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_MessageTypeID",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "MessageTypeID",
                table: "Messages");

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastActiveTime",
                value: new DateTime(2025, 5, 3, 22, 9, 44, 128, DateTimeKind.Utc).AddTicks(1810));
        }
    }
}
