using Labb3_Gymnasium.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3_Gymnasium
{
    public class MethodsDepartment
    {
        public static void Departments(GymnaisumContext dbContext)
        {
            Console.Clear();
            Console.WriteLine("Departments: ");
            Console.WriteLine("1: View all departments and how many teachers work in the departments. ");
            Console.WriteLine("2: Salarys");
            Console.WriteLine("3: Salary statistics");
            Console.WriteLine("4: Return to menu");

            string choice = Console.ReadLine();

            Console.Clear();

            Console.Clear();
            switch (choice)
            {
                case "1":
                    GetDepartmentsAndEmployees(dbContext);
                    break;
                case "2":
                    Salarys(dbContext);
                    break;
                case "3":
                    SalaryStatistics(dbContext);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Wrong input, try again 1-4");
                    Departments(dbContext);
                    Console.Clear();
                    break;
            }
            

        }
        public static void GetDepartmentsAndEmployees(GymnaisumContext dbContext)
        {
            // Retrieve and display information about departments and the number of teachers in each
            var departments = dbContext.Departments.ToList();

            foreach(var department in departments)
            {
                var Teachers = dbContext.EmployeeDepartmentInfos.Where(EmployeeDepartmentInfo => EmployeeDepartmentInfo.DepartmentId == department.DepartmentId && EmployeeDepartmentInfo.Employee.Role == "Teacher").ToList();
                Console.WriteLine($"Department: {department.DepartmentName}, number of teachers: {Teachers.Count}");
            }
            Console.WriteLine("Press enter to return.");
            Console.ReadLine();
            Console.Clear();
            Departments(dbContext);
        }

        public static void Salarys(GymnaisumContext dbContext)
        {
            // Retrieve and display total salaries for each department
            var departments = dbContext.Departments.ToList();

            foreach (var department in departments)
            {
                var totalSalary = dbContext.EmployeeDepartmentInfos
                       .Where(info => info.DepartmentId == department.DepartmentId)
                       .Sum(info => info.Salary);

                Console.WriteLine($"Department: {department.DepartmentName}, Total Salary: {totalSalary}");

            }
            Console.WriteLine("Press enter to return.");
            Console.ReadLine();
            Console.Clear();
            Departments(dbContext);
        }

        public static void SalaryStatistics(GymnaisumContext dbContext)
        {
            // Retrieve and display average salary statistics for each department
            var departments = dbContext.Departments.ToList();

            foreach(var department in departments)
            {
                var departmentInfo = dbContext.EmployeeDepartmentInfos
                    .Where(info => info.DepartmentId == department.DepartmentId)
                    .ToList();
                var avgSalary = departmentInfo.Sum(info => info.Salary) / departmentInfo.Count();

                Console.WriteLine($"Department: {department.DepartmentName}, Average Salary: {avgSalary}");
            }
            Console.WriteLine("Press enter to return.");
            Console.ReadLine();
            Console.Clear();
            Departments(dbContext);
        }

    }
}
