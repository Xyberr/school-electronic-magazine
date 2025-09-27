using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace school_electronic_magazine.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneNumberParentToStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<string>>(
                name: "HomeroomClasses",
                table: "Teachers",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<List<string>>(
                name: "TeachingSubjects",
                table: "Teachers",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Teachers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "phoneNumber",
                table: "Teachers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumberParent",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SchoolClassId",
                table: "Students",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Admins",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HomeroomClasses",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "TeachingSubjects",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "email",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "phoneNumber",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "PhoneNumberParent",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SchoolClassId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Admins");
        }
    }
}
