using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Digital_BE.Migrations
{
    public partial class UpdateTableForProcess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessStepId",
                table: "Processes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProcessStepId",
                table: "Processes",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
