using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DOCUMENT_MANAGER.Migrations
{
    /// <inheritdoc />
    public partial class AddFolderRelationshipToUploadedFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Folders_FolderId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Folders_FolderId",
                table: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_Folders_FolderId",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "FolderId",
                table: "Folders");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Folders_FolderId",
                table: "Documents",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Folders_FolderId",
                table: "Documents");

            migrationBuilder.AddColumn<Guid>(
                name: "FolderId",
                table: "Folders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Folders_FolderId",
                table: "Folders",
                column: "FolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Folders_FolderId",
                table: "Documents",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Folders_FolderId",
                table: "Folders",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id");
        }
    }
}
