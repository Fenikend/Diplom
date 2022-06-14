using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScheduleProg.Data.Migrations
{
    public partial class migr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_AspNetUsers_User_Id",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_User_Id",
                table: "Teachers");

            migrationBuilder.AlterColumn<string>(
                name: "User_Id",
                table: "Teachers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_User_Id",
                table: "Teachers",
                column: "User_Id",
                unique: true,
                filter: "[User_Id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_AspNetUsers_User_Id",
                table: "Teachers",
                column: "User_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_AspNetUsers_User_Id",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_User_Id",
                table: "Teachers");

            migrationBuilder.AlterColumn<string>(
                name: "User_Id",
                table: "Teachers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_User_Id",
                table: "Teachers",
                column: "User_Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_AspNetUsers_User_Id",
                table: "Teachers",
                column: "User_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
