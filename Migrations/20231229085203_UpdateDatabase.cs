using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb3_Gymnasium.Migrations
{
    public partial class UpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Course",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Student",
                table: "Enrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments");

            migrationBuilder.AlterColumn<int>(
                name: "Grade",
                table: "Enrollments",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Employees",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldUnicode: false,
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmploymentYear",
                table: "Employees",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Salary",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDepartmentInfo",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_EmployeeDepartmentInfo_Department",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId");
                    table.ForeignKey(
                        name: "FK_EmployeeDepartmentInfo_Employee",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDepartmentInfo_DepartmentId",
                table: "EmployeeDepartmentInfo",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDepartmentInfo_EmployeeId",
                table: "EmployeeDepartmentInfo",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK__Enrollmen__Cours__3D5E1FD2",
                table: "Enrollments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_STUDENT_ID",
                table: "Enrollments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Enrollmen__Cours__3D5E1FD2",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_STUDENT_ID",
                table: "Enrollments");

            migrationBuilder.DropTable(
                name: "EmployeeDepartmentInfo");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "EmploymentYear",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Employees");

            migrationBuilder.AlterColumn<double>(
                name: "Grade",
                table: "Enrollments",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Employees",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldUnicode: false,
                oldMaxLength: 30);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments",
                columns: new[] { "StudentId", "CourseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Course",
                table: "Enrollments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Student",
                table: "Enrollments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");
        }
    }
}
