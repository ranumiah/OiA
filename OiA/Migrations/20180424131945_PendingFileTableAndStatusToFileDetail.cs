using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OiA.Migrations
{
    public partial class PendingFileTableAndStatusToFileDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "FileSystem",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PendingFile",
                columns: table => new
                {
                    FileFullName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingFile", x => x.FileFullName);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PendingFile");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "FileSystem");
        }
    }
}
