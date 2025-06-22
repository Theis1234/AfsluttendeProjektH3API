using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AfsluttendeProjektH3API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changedUserProfileObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserProfile_DateOfBirth",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserProfile_Age",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserProfile_Age",
                table: "Users");

            migrationBuilder.AddColumn<DateTime>(
                name: "UserProfile_DateOfBirth",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }
    }
}
