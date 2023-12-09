using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Lab1_SQL.Utilities;

namespace Lab1_SQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                Console.WriteLine("1. View all students\n" +
                "2. View students by class\n" +
                "3. Add staff\n" +
                "4. View all staff\n" +
                "5. View all grades from last month\n" +
                "6. View average grade per course\n" +
                "7. Add student");

                Console.Write("Enter command: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        StudentMethods.ShowAllStudents();
                        break;
                    case 2:
                        StudentMethods.ShowStudentByClass();
                        break;
                    case 3:
                        StaffMethods.AddStaff();
                        break;
                    case 4:
                        StaffMethods.GetStaff();
                        break;
                    case 5:
                        GradesMethods.ShowAllGradesFromLastMonth();
                        break;
                    case 6:
                        GradesMethods.GradeStatisticsByCourse();
                        break;
                    case 7:
                        StudentMethods.AddStudent();
                        break;
                }
            }
        }

        public static void EndMessage()
        {
            Console.WriteLine("\nPress ENTER to return to menu");
            Console.ReadKey();
            Console.Clear();
        }
    }
}