using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OiA.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileSystem",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileCreationTimeUtc = table.Column<DateTime>(nullable: false),
                    FileExtension = table.Column<string>(nullable: false),
                    FileFullName = table.Column<string>(nullable: false),
                    FileLastAccessTimeUtc = table.Column<DateTime>(nullable: false),
                    FileLastWriteTimeUtc = table.Column<DateTime>(nullable: false),
                    FileLength = table.Column<long>(nullable: false),
                    FileName = table.Column<string>(nullable: false),
                    Md5Hash = table.Column<string>(maxLength: 32, nullable: true),
                    Sha256Hash = table.Column<string>(maxLength: 64, nullable: true),
                    Sha512Hash = table.Column<string>(maxLength: 128, nullable: true),
                    TimeStamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileSystem", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileSystem_Md5Hash",
                table: "FileSystem",
                column: "Md5Hash")
                .Annotation("SqlServer:Clustered", false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileSystem");
        }
    }
}
