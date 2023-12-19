using Labb3_Gymnasium.Models;

namespace Labb3_Gymnasium
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Database=Gymnaisum;Trusted_Connection=True;MultipleActiveResultSets=true";
            using (GymnaisumContext dbContext = new GymnaisumContext())
            {
                while (true)
                {
                    Console.WriteLine("Select a function:");
                    Console.WriteLine("1. Employees ");
                    Console.WriteLine("2. Students");
                    Console.WriteLine("3. Exit");
                    int choice;
                    while (!int.TryParse(Console.ReadLine(), out choice))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number in the menu");
                    }
                    Console.Clear();
                    switch (choice)
                    {
                        case 1:
                            MethodsEmployee.Employees(dbContext);
                            break;
                        case 2:
                            MethodsStudent.Students(dbContext);
                            break;
                        case 3:
                            Console.WriteLine("The program ends, welcome back!");
                            Environment.Exit(0);
                            break;
                    }
                }
            }
        }
    }
}