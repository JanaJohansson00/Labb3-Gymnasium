using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb3_Gymnasium.Migrations
{
    public partial class Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Enrollmen__Cours__3D5E1FD2",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_STUDENT_ID",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments");

            migrationBuilder.AlterColumn<double>(
                name: "Grade",
                table: "Enrollments",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments",
                column: "StudentId");

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
    }
}
