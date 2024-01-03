using Labb3_Gymnasium.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3_Gymnasium
{
    internal class MethodsGrades
    {
        public static void Grades(GymnaisumContext dbContext)
        {
            Console.WriteLine("1. Add new grades");
            Console.WriteLine("2. View grades latest month");
            Console.WriteLine("3. View grades statistics ");
            Console.WriteLine("4. View all grades");
            Console.WriteLine("5. Return");

            string choice = Console.ReadLine();

            Console.Clear();

            switch (choice)
            {
                case "1":
                    AddGrades(dbContext);
                    break;
                case "2":
                    ViewGradesLatestMonth(dbContext);
                    break;
                case "3":
                    ViewCourseGradesStatistics(dbContext);
                    break;
                case "4":
                    ViewAllGrades(dbContext);
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Wrong input, try again");
                    Grades(dbContext);
                    Console.Clear();
                    break;
            }
            Console.Clear();
        }
        public static void ViewGradesLatestMonth(GymnaisumContext dbContext)
        {
            try
            {
                //Get the date for the latest month.
                DateTime latestMonth = DateTime.Now.AddMonths(-1);
                Console.WriteLine($"Month; {latestMonth}");

                //Retrieve recent grades from the database.
                var recentGrades = dbContext.Enrollments
                    .Where(e => e.GradeDate >= latestMonth)
                    .Include(e => e.Student)
                    .Include(e => e.Course)
                    .ToList();
                

                if (recentGrades.Any())
                {
                    Console.WriteLine("Grades set the latest month: ");

                    foreach (var grade in recentGrades)
                    {
                        Console.WriteLine($"Student: {grade.Student.FirstName} {grade.Student.LastName}");
                        Console.WriteLine($"Course: {grade.Course.CourseName}");
                        Console.WriteLine($"Grade: {grade.Grade}");
                        Console.WriteLine($"Grade Date: {grade.GradeDate}");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No recent grades available.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }
            Console.WriteLine("Press Enter to return...");
            Console.ReadLine();
            Console.Clear();
            Grades(dbContext);
        }
        public static void ViewCourseGradesStatistics(GymnaisumContext dbContext)
        {
            try
            {
                // Retrieve unique course IDs from enrollments
                var courseIds = dbContext.Enrollments
                    .Select(enrollment => enrollment.CourseId)
                    .Distinct()
                    .ToList();
                // Calculate and display statistics for each course
                var courseStatistics = courseIds
                   .Select(courseId => new
                   {
                      CourseName = dbContext.Courses
                          .Where(course => course.CourseId == courseId)
                          .Select(course => course.CourseName)
                          .FirstOrDefault(),
                      Enrollments = dbContext.Enrollments
                          .Where(enrollment => enrollment.CourseId == courseId)
                          .ToList()
                    })
                    .Select(course => new
                    {
                       CourseName = course.CourseName,
                       AverageGrade = course.Enrollments.Any() ? course.Enrollments.Average(enrollment => enrollment.Grade) : (double?)0,
                       MaxGrade = course.Enrollments.Any() ? course.Enrollments.Max(enrollment => enrollment.Grade) : (int?)0,
                       MinGrade = course.Enrollments.Any() ? course.Enrollments.Min(enrollment => enrollment.Grade) : (int?)0
                    })
                    .ToList();

                if (courseStatistics.Any())
                {
                    Console.WriteLine("*Course statistics*:");

                    foreach (var courseStat in courseStatistics)
                    {
                        Console.WriteLine($"Course: {courseStat.CourseName}");
                        Console.WriteLine($"Average Grade: {courseStat.AverageGrade}");
                        Console.WriteLine($"Max Grade: {courseStat.MaxGrade}");
                        Console.WriteLine($"Min Grade: {courseStat.MinGrade}");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No course statistics available.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
            Console.WriteLine("Press Enter to return...");
            Console.ReadLine();
            Console.Clear();
            Grades(dbContext);
        }
        public static void AddGrades(GymnaisumContext dbContext)
        {
            Console.WriteLine("Select a student:");
            var students = dbContext.Students.ToList();
            foreach (var student in students)
            {
                Console.WriteLine($"{student.StudentId}. {student.FirstName}");
            }

            int selectedStudentId;
            while (true)
            {
                Console.Write("Enter Student ID: ");
                if(int.TryParse(Console.ReadLine(), out selectedStudentId) && students.Any(s => s.StudentId == selectedStudentId))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Student ID. Check the list and try again.");
                }
            }
            Console.Clear();

            Console.WriteLine("Select a Course :");
            var courses = dbContext.Courses.ToList();
            foreach (var course in courses)
            {
                Console.WriteLine($"{course.CourseId}. {course.CourseName}");
            }
            int selectedCourseId;
            while (true)
            {
                Console.WriteLine("Enter the Course ID:");
                if(int.TryParse(Console.ReadLine(), out selectedCourseId) && courses.Any(c=>c.CourseId == selectedCourseId))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Course Id. Check the list and try again.");
                }
            }
            Console.Clear();

            Console.WriteLine("Select a teacher:");
            var teachers = dbContext.Employees
                .Where(teacher => teacher.Role == "Teacher")
                .ToList();
            foreach (var teacher in teachers)
            {
                Console.WriteLine($"{teacher.EmployeeId}: {teacher.FirstName} {teacher.LastName} ");
            }

            int selectedTeacherId;
            while (true)
            {
                Console.Write("Enter the teacher ID: ");
                if(int.TryParse(Console.ReadLine(), out selectedTeacherId) && teachers.Any(t=> t.EmployeeId == selectedTeacherId))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Teacher Id. Check the list and try again.");
                }
            }
            Console.Clear();

            int score;
            while (true)
            {
                Console.Write("Enter the grade: ");
                if (int.TryParse(Console.ReadLine(), out score) && score >= 1 && score <= 5)
                {
                    break; 
                }
                else
                {
                    Console.WriteLine("Invalid selection. Try again (1-5).");
                }
            }

            var newGrade = new Enrollment
            {
                StudentId = selectedStudentId,
                CourseId = selectedCourseId,
                GradeDate = DateTime.Now,
                Grade = score
            };

            // Add the new grade to the database
            dbContext.Enrollments.Add(newGrade);

            // Save the changes in the database
            dbContext.SaveChanges();
            Console.WriteLine("The grade has been added and saved in the database.");
            Console.WriteLine("Press Enter to return...");
            Console.ReadLine();
            Console.Clear();
            Grades(dbContext);

        }
        public static void ViewAllGrades(GymnaisumContext dbContext)
        {
            try
            {
                // Retrieve all grades from the database
                var allGrades = dbContext.Enrollments
                    .Include(e => e.Student)
                    .Include(e => e.Course)
                    .ThenInclude(c => c.Teacher)
                    .ToList();

                if (allGrades.Any())
                {
                    Console.WriteLine("All grades:");

                    foreach (var grade in allGrades)
                    {
                        Console.WriteLine($"Student: {grade.Student.FirstName}");
                        Console.WriteLine($"Course: {grade.Course.CourseName}");
                        Console.WriteLine($"Grade: {grade.Grade}");
                        Console.WriteLine($"Grade Date: {grade.GradeDate}");
                        Console.WriteLine($"Teacher: {grade.Course.Teacher.FirstName} {grade.Course.Teacher.LastName}");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No grades available.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
            Console.WriteLine("Press Enter to return...");
            Console.ReadLine();
            Console.Clear();
            Grades(dbContext);
        }
    }
}
       



