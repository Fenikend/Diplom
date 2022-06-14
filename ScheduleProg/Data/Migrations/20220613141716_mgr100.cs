using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScheduleProg.Data.Migrations
{
    public partial class mgr100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Potoks_Potok_Id",
                table: "Groups");

            migrationBuilder.DropTable(
                name: "Potoks");

            migrationBuilder.DropIndex(
                name: "IX_Groups_Potok_Id",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Potok_Id",
                table: "Groups");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Potok_Id",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Potoks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Potok_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Potoks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Potok_Id",
                table: "Groups",
                column: "Potok_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Potoks_Potok_Id",
                table: "Groups",
                column: "Potok_Id",
                principalTable: "Potoks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
