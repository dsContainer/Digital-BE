using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Digital_BE.Migrations
{
    public partial class deleteFieldNameOfSignature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Signatures");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Signatures",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
