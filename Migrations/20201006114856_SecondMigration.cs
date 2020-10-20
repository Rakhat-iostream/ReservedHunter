using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IndustrialStudentPositionHunters.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Advertisements_PositionId",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Employers");

            migrationBuilder.AddColumn<byte[]>(
                name: "HashedPassword",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "SaltPassword",
                table: "Students",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Positions",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<byte[]>(
                name: "HashedPassword",
                table: "Employers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Employers",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "SaltPassword",
                table: "Employers",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Companies",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Advertisements",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Advertisements",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_PositionId",
                table: "Advertisements",
                column: "PositionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Advertisements_PositionId",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "HashedPassword",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SaltPassword",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "HashedPassword",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "SaltPassword",
                table: "Employers");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Students",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Positions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Employers",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Companies",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Advertisements",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Advertisements",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_PositionId",
                table: "Advertisements",
                column: "PositionId",
                unique: true);
        }
    }
}
