using Microsoft.EntityFrameworkCore.Migrations;
using System;
using DAO.Models;
using DAO.Services;

namespace DAO.Migrations
{
    public partial class AddedUserNameAndPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var dummyUser = new User { PasswordText = "default" };
            new HashPasswordService().AddSaltAndHashPassword(dummyUser);

            migrationBuilder.AddColumn<byte[]>(
                name: "Password",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Salt",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Users",
                nullable: true);

            migrationBuilder.UpdateData("Users", "Password", null, new []{"Password", "Salt"}, new object[] { dummyUser.Password, dummyUser.Salt });

            migrationBuilder.Sql($@"
                UPDATE Users
                SET UserName=Name
            ");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Password",
                table: "Users",
                nullable: false);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Salt",
                table: "Users",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Users");
        }
    }
}
