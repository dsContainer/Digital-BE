using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Digital_BE.Migrations
{
    public partial class UpdateBatchAndTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processes_Batches_BatchId",
                table: "Processes");

            migrationBuilder.DropIndex(
                name: "IX_Processes_BatchId",
                table: "Processes");

            migrationBuilder.DropIndex(
                name: "IX_Processes_TemplateId",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "BatchId",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "ProcessId",
                table: "Batches");

            migrationBuilder.CreateTable(
                name: "BatchProcess",
                columns: table => new
                {
                    BatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchProcess", x => new { x.BatchId, x.ProcessId });
                    table.ForeignKey(
                        name: "FK_BatchProcess_Batches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BatchProcess_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Processes_TemplateId",
                table: "Processes",
                column: "TemplateId",
                unique: true,
                filter: "[TemplateId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BatchProcess_ProcessId",
                table: "BatchProcess",
                column: "ProcessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BatchProcess");

            migrationBuilder.DropIndex(
                name: "IX_Processes_TemplateId",
                table: "Processes");

            migrationBuilder.AddColumn<Guid>(
                name: "BatchId",
                table: "Processes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProcessId",
                table: "Batches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Processes_BatchId",
                table: "Processes",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Processes_TemplateId",
                table: "Processes",
                column: "TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_Batches_BatchId",
                table: "Processes",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "Id");
        }
    }
}
