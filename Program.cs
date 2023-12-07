using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab1_SQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                Console.WriteLine("1. Get all students\n" +
                "2. Get students by class\n" +
                "3. Add staff\n" +
                "4. Get all staff\n" +
                "5. Get all grades from last month\n" +
                "6. View average grade per course\n" +
                "7. Add student");

                Console.Write("Enter command: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        ShowFromDb.ShowAllStudents();
                        break;
                    case 2:
                        ShowFromDb.ShowClasses();
                        break;
                    case 3:
                        AddToDb.AddStaff();
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                }
            }
        }

        
    }
}