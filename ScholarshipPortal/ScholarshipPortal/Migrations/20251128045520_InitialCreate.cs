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
                name: "Institutes",
                columns: table => new
                {
                    InstituteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstituteName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstituteCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DISECode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstituteType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearAdmissionStarted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AffiliatedUniversityState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniversityBoardName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstablishmentCertificate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffiliationCertificate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrincipalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecurityQuestion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateActionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institutes", x => x.InstituteId);
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

            migrationBuilder.CreateTable(
                name: "ScholarshipApplications",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchemeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AadhaarNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Religion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommunityCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FamilyAnnualIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InstituteName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PresentClassCourse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PresentClassCourseYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModeOfStudy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UniversityBoardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreviousClassCourse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreviousPassingYear = table.Column<int>(type: "int", nullable: true),
                    PreviousClassPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Class10RollNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Class10BoardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Class10PassingYear = table.Column<int>(type: "int", nullable: true),
                    Class10Percentage = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Class12RollNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Class12BoardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Class12PassingYear = table.Column<int>(type: "int", nullable: true),
                    Class12Percentage = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    ContactState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactDistrict = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlockTaluk = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pincode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdmissionFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TuitionFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    TypeOfDisability = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisabilityPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentsProfession = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DomicileCertificatePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentPhotographPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstituteIdCardPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CasteIncomeCertificatePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreviousMarksheetPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeeReceiptPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankPassbookPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AadhaarCardPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Class10MarksheetPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Class12MarksheetPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateActionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinalDeclarationAccepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScholarshipApplications", x => x.ApplicationId);
                });

            migrationBuilder.CreateTable(
                name: "StateUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StateUsers_Email",
                table: "StateUsers",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactSubmissions");

            migrationBuilder.DropTable(
                name: "Institutes");

            migrationBuilder.DropTable(
                name: "RegisteredUsers");

            migrationBuilder.DropTable(
                name: "ScholarshipApplications");

            migrationBuilder.DropTable(
                name: "StateUsers");
        }
    }
}
