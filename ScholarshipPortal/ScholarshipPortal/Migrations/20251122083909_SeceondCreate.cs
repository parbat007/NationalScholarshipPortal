using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScholarshipPortal.Migrations
{
    /// <inheritdoc />
    public partial class SeceondCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    FinalDeclarationAccepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScholarshipApplications", x => x.ApplicationId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScholarshipApplications");
        }
    }
}
