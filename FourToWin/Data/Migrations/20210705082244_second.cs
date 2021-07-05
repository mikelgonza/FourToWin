using Microsoft.EntityFrameworkCore.Migrations;

namespace FourToWin.Data.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_AspNetUsers_AppUserId",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_AspNetUsers_AppUserId1",
                table: "Match");

            migrationBuilder.DropIndex(
                name: "IX_Match_AppUserId",
                table: "Match");

            migrationBuilder.DropIndex(
                name: "IX_Match_AppUserId1",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Match");

            migrationBuilder.AlterColumn<string>(
                name: "User2Id",
                table: "Match",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "User1Id",
                table: "Match",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserImage",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Match_User1Id",
                table: "Match",
                column: "User1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Match_User2Id",
                table: "Match",
                column: "User2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_AspNetUsers_User1Id",
                table: "Match",
                column: "User1Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Match_AspNetUsers_User2Id",
                table: "Match",
                column: "User2Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_AspNetUsers_User1Id",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_AspNetUsers_User2Id",
                table: "Match");

            migrationBuilder.DropIndex(
                name: "IX_Match_User1Id",
                table: "Match");

            migrationBuilder.DropIndex(
                name: "IX_Match_User2Id",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "UserImage",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "User2Id",
                table: "Match",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "User1Id",
                table: "Match",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Match",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Match",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Match_AppUserId",
                table: "Match",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_AppUserId1",
                table: "Match",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_AspNetUsers_AppUserId",
                table: "Match",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Match_AspNetUsers_AppUserId1",
                table: "Match",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
