using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OiA.Migrations
{
    public partial class UpdateFileDetailTableStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FileSystem_Sha256Hash",
                table: "FileSystem");

            migrationBuilder.DropIndex(
                name: "IX_FileSystem_Sha512Hash",
                table: "FileSystem");

            migrationBuilder.DropColumn(
                name: "Sha256Hash",
                table: "FileSystem");

            migrationBuilder.DropColumn(
                name: "Sha512Hash",
                table: "FileSystem");

            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "FileSystem");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "FileSystem",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FileLength",
                table: "FileSystem",
                newName: "Length");

            migrationBuilder.RenameColumn(
                name: "FileFullName",
                table: "FileSystem",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "FileExtension",
                table: "FileSystem",
                newName: "Extension");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "FileSystem",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "Length",
                table: "FileSystem",
                newName: "FileLength");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "FileSystem",
                newName: "FileFullName");

            migrationBuilder.RenameColumn(
                name: "Extension",
                table: "FileSystem",
                newName: "FileExtension");

            migrationBuilder.AddColumn<string>(
                name: "Sha256Hash",
                table: "FileSystem",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sha512Hash",
                table: "FileSystem",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "TimeStamp",
                table: "FileSystem",
                rowVersion: true,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileSystem_Sha256Hash",
                table: "FileSystem",
                column: "Sha256Hash")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_FileSystem_Sha512Hash",
                table: "FileSystem",
                column: "Sha512Hash")
                .Annotation("SqlServer:Clustered", false);
        }
    }
}
