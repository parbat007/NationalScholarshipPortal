using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScholarshipPortal.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactSubmissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactSubmissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegisteredUsers",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StateOfDomicile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DOB = table.Column<DateTime>(type: "date", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstituteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AadhaarNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankIfscCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DeclarationAccepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredUsers", x => x.Email);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactSubmissions");

            migrationBuilder.DropTable(
                name: "RegisteredUsers");
        }
    }
}
