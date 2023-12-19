using Labb3_Gymnasium.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Labb3_Gymnasium
{
    public class MethodsStudent
    {
        public static void Students(GymnaisumContext dbContext)
        {
            Console.WriteLine("1: View all students");
            Console.WriteLine("2: View students by class");
            Console.WriteLine("3: View grades recent month");
            Console.WriteLine("4. View grades statistics");
            Console.WriteLine("5. Add new student");
            Console.WriteLine("6: Return");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid selection. Try again (1-6).");
            }

            Console.Clear();
            switch (choice)
            {
                case 1:
                    ViewAllStudents(dbContext);
                    break;
                case 2:
                    ViewStudentsByClass(dbContext);
                    break;
                case 3:
                    MethodsGrades.ViewGradesLatestMonth(dbContext);
                    break;
                case 4:
                    MethodsGrades.ViewCourseGradesStatistics(dbContext);
                    break;
                case 5:
                    MethodsStudent.AddNewStudent(dbContext);
                    break;
                case 6:
                    return;
                default:
                    Console.WriteLine("Wrong input, try again");
                    Students(dbContext);
                    Console.Clear();
                    break;
            }
            Console.Clear();
        }
        public static void ViewAllStudents(GymnaisumContext dbContext)
        {
            Console.WriteLine("Select sorting options:");
            Console.WriteLine("1. Sort by first name, ascending");
            Console.WriteLine("2. Sort by first name, descending");
            Console.WriteLine("3. Sort by last name, ascending");
            Console.WriteLine("4. Sort by last name, descending");

            int sortChoice;
            while (!int.TryParse(Console.ReadLine(), out sortChoice) || sortChoice < 1 || sortChoice > 4)
            {
                Console.WriteLine("Invalid input. Please enter a valid number (1-4):");
            }

            Console.Clear();
            IQueryable<Student> students;

            switch (sortChoice)
            {
                case 1:
                    students = dbContext.Students.OrderBy(s => s.FirstName);
                    break;
                case 2:
                    students = dbContext.Students.OrderByDescending(s => s.FirstName);
                    break;
                case 3:
                    students = dbContext.Students.OrderBy(s => s.LastName);
                    break;
                case 4:
                    students = dbContext.Students.OrderByDescending(s => s.LastName);
                    break;
                default:
                    Console.WriteLine("Invalid sorting option. Returning to the main menu.");
                    return;
            }
            Console.Clear();
            Console.WriteLine("* All students *");
            foreach (var student in students)
            {
                Console.WriteLine($"Student Id {student.StudentId}: {student.FirstName} {student.LastName}");
            }
            Console.WriteLine("Press Enter to return");
            Console.ReadLine();
            Console.Clear();
            Students(dbContext);
        }
        public static void ViewStudentsByClass(GymnaisumContext dbContext)
        {
            Console.WriteLine("Select class");
            Console.WriteLine("1: EK14");
            Console.WriteLine("2: EK16");
            Console.WriteLine("3: NA11");
            Console.WriteLine("4: SA15");
            Console.WriteLine("5: TE17");

            {
                Console.WriteLine("Available classes: EK14, EK16, NA11, SA15, TE17 ");

                string classChoice = "";
                bool isValidClass = false;

                while (!isValidClass)
                {
                    Console.WriteLine("Enter a class to view students");
                    classChoice = Console.ReadLine();

                    string[] validClassCodes = { "EK14", "EK16", "NA11", "SA15", "TE17" };

                    isValidClass = validClassCodes.Contains(classChoice);

                    if (!isValidClass)
                    {
                        Console.WriteLine("Invalid class. Enter a class available in the list.");
                    }
                }
                Console.Clear();

                try
                {
                    var studentsInClass = dbContext.Students
                        .Where(predicate: s => s.Class == classChoice)
                        .ToList();
                    if (studentsInClass.Any())
                    {
                        Console.WriteLine($"* Students in Class {classChoice} *");
                        foreach (var student in studentsInClass)
                        {
                            Console.WriteLine($"Student Id {student.StudentId}: {student.FirstName} {student.LastName}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No students found in Class {classChoice}.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occured: {ex.Message}");
                }
                Console.WriteLine("Press Enter to return");
                Console.ReadLine();
                Console.Clear();
                Students(dbContext);
            }
        }
        public static void AddNewStudent(GymnaisumContext dbContext)
        {
            Console.WriteLine("Enter student information: ");

            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            int personalNumber;
            while (true)
            {
                Console.WriteLine("Enter personalnumber (YYYYMMDD):");
                string personalNumberInput = Console.ReadLine();

                if (personalNumberInput.Length == 8 && int.TryParse(personalNumberInput, out personalNumber))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid personalnumber. Please enter 8 digits (YYMMDD)");
                }
            }
            bool isValidClass = false;
            string studentClass = "";

            while (!isValidClass)
            {
                Console.WriteLine("Class (EK14, EK16, NA11, SA15, TE17)");
                studentClass = Console.ReadLine();

                string[] validClass = { "EK14", "EK16", "NA11", "SA15", "TE17" };
                isValidClass = validClass.Contains(studentClass);

                if (!isValidClass)
                {
                    Console.WriteLine("Class not available. Enter a class available from the list.");
                }
            }
            Console.Clear();

            var newStudent = new Student
            {
                FirstName = firstName,
                LastName = lastName,
                Class = studentClass,
                PersonalNumber = personalNumber
            };

            try
            {
                dbContext.Students.Add(newStudent);
                dbContext.SaveChanges();

                Console.WriteLine("New student added!");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
            Console.WriteLine("Press Enter to return");
            Console.ReadLine();
            Console.Clear();
            Students(dbContext);
        }
    }
}

