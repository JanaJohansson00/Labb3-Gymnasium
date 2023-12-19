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
            Console.WriteLine("4: Return to menu");

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
            using (SqlConnection connection = new SqlConnection(dbContext.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Employees";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("All employees");
                        while (reader.Read())
                        {
                            Console.WriteLine($"Employee Id {reader["EmployeeId"]}: {reader["FirstName"]} {reader["LastName"]} {reader["Role"]}");
                        }
                        Console.WriteLine("Press Enter to return");
                        Console.ReadLine();
                        Console.Clear();
                        Employees(dbContext);
                    }

                }
            }
        }
        public static void ViewAllTeachers(GymnaisumContext dbContext)
        {
            using (SqlConnection connection = new SqlConnection(dbContext.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Employees WHERE Role = @Role";

                Console.Clear();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Role", "Teacher");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("All the teachers:");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["FirstName"]} {reader["LastName"]}");

                        }
                        Console.WriteLine("Press enter to return");
                        Console.ReadLine();
                        Console.Clear();
                        Employees(dbContext);
                    }
                }
            }
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
            Console.Clear();

            var newEmployee = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Role = employeeRole
            };

            try
            {
                dbContext.Employees.Add(newEmployee);
                dbContext.SaveChanges();

                Console.WriteLine("New Employee added!");
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
    }
}

