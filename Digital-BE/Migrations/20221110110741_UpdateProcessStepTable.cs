using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Digital_BE.Migrations
{
    public partial class UpdateProcessStepTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleUser_Roles_RoleId",
                table: "RoleUser");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProcessSteps");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "RoleUser",
                newName: "RolesId");

            migrationBuilder.AddColumn<float>(
                name: "XPointPercent",
                table: "ProcessSteps",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "YPointPercent",
                table: "ProcessSteps",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUser_Roles_RolesId",
                table: "RoleUser",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleUser_Roles_RolesId",
                table: "RoleUser");

            migrationBuilder.DropColumn(
                name: "XPointPercent",
                table: "ProcessSteps");

            migrationBuilder.DropColumn(
                name: "YPointPercent",
                table: "ProcessSteps");

            migrationBuilder.RenameColumn(
                name: "RolesId",
                table: "RoleUser",
                newName: "RoleId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProcessSteps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUser_Roles_RoleId",
                table: "RoleUser",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
