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
        public static void ViewGradesLatestMonth(GymnaisumContext dbContext)
        {
            try
            {
                DateTime latestMonth = DateTime.Now.AddMonths(-1);
                Console.WriteLine($"Month; {latestMonth}");

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

            Console.ReadLine();
        }
        public static void ViewCourseGradesStatistics(GymnaisumContext dbContext)
        {
            try
            {
                var courseIds = dbContext.Enrollments
                    .Select(enrollment => enrollment.CourseId)
                    .Distinct()
                    .ToList();

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
                    Console.WriteLine("Course statistics:");

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
            Console.ReadLine();
        }
    }
}



