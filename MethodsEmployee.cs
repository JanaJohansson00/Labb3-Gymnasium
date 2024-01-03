using Labb3_Gymnasium.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3_Gymnasium
{
    public class MethodsEmployee
    {
        public static void Employees(GymnaisumContext dbContext)
        {
            Console.Clear();
            Console.WriteLine("Employees: ");
            Console.WriteLine("1: View all employees");
            Console.WriteLine("2: View all teachers");
            Console.WriteLine("3: Add new employee");
            Console.WriteLine("4: View active courses");
            Console.WriteLine("5: Return to menu");

            string choice = Console.ReadLine();

            Console.Clear();

            switch (choice)
            {
                case "1":
                    ViewAllEmployees(dbContext);
                    break;
                case "2":
                    ViewAllTeachers(dbContext);
                    break;
                case "3":
                    AddNewEmployee(dbContext);
                    break;
                case "4":
                    ActiveCourses(dbContext);
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid selection. Try again (1-4)");
                    Employees(dbContext);
                    Console.Clear();
                    break;
            }
        }
        public static void ViewAllEmployees(GymnaisumContext dbContext)
        {
            // Retrieve and display information about all employees

            var listOfEmployees = dbContext.Employees.ToList();
            foreach (var employee in listOfEmployees)
            {
                Console.WriteLine($"Name: {employee.FirstName} {employee.LastName} | Role: {employee.Role} | Years worked: {DateTime.Now.Year - employee.EmploymentYear.Year}");
            }

            Console.WriteLine("Press Enter to return");
            Console.ReadLine();
            Console.Clear();
            Employees(dbContext);

        }
        public static void ViewAllTeachers(GymnaisumContext dbContext)
        {
            // Retrieve and display information about all teachers
            var teachers = dbContext.Employees.Where(t => t.Role == "Teacher");
            foreach (var teacher in teachers)
            {
                Console.WriteLine($"Name: {teacher.FirstName} {teacher.LastName} | Role: {teacher.Role} | Years worked: {DateTime.Now.Year - teacher.EmploymentYear.Year}");
            }
            Console.WriteLine("Press Enter to return");
            Console.ReadLine();
            Console.Clear();
            Employees(dbContext);
        }
        public static void AddNewEmployee(GymnaisumContext dbContext)
        {
            Console.WriteLine("Enter employee information: ");

            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            bool isValidRole = false;
            string employeeRole = "";

            while (!isValidRole)
            {
                Console.WriteLine("Role (Prinicpal, Teacher or Admin): ");
                employeeRole = Console.ReadLine();

                string[] validRoles = { "Principal", "Admin", "Teacher" };
                isValidRole = validRoles.Contains(employeeRole);

                if (!isValidRole)
                {
                    Console.WriteLine("Invalid input. Enter a role available from the list.");
                }
            }

            bool isValidSubject = false;
            string teacherSubject = "";
            if (employeeRole == "Teacher")
            {
                Console.WriteLine("Choose a subject\n" +
                    "Math, English,Programming C#, Art, Civics, Sport, Databases SQL or Special Math Education");

                while (!isValidSubject)
                {
                    teacherSubject = Console.ReadLine();

                    string[] validSubjects = { "Math", "English", "Programming C#", "Art", "Civics", "Sport", "Databases SQL", "Special Math Education" };
                    isValidSubject = validSubjects.Contains(teacherSubject);

                    if (!isValidSubject)
                    {
                        Console.WriteLine("Invalid subject. Enter a subject available in the list.");
                    }
                }
            }
            Console.Clear();

            var newEmployee = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Role = employeeRole,
                EmploymentYear = DateTime.Now
            };

            try
            {
                // Add the new employee to the database and save changes
                dbContext.Employees.Add(newEmployee);
                dbContext.SaveChanges();

                Console.WriteLine($"New {employeeRole} added!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
            Console.WriteLine("Press enter to return.");
            Console.ReadLine();
            Console.Clear();
            Employees(dbContext);
        }
        public static void ActiveCourses(GymnaisumContext dbContext)
        {
            // Retrieve and display information about active courses
            Console.WriteLine("Active Courses: ");

            var listOfActiveCourses = dbContext.Courses
                .Include(c => c.Teacher)
                .ToList();

            foreach (var course in listOfActiveCourses)
            {
                Console.WriteLine($"Course Id: {course.CourseId} Course Name: {course.CourseName} Teacher: {course.Teacher.FirstName} {course.Teacher.LastName}");
            }
            Console.WriteLine("Press enter to return.");
            Console.ReadLine();
            Console.Clear();
            Employees(dbContext);
        }
    }
}

