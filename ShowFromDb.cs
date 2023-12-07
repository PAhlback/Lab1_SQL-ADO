using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_SQL
{
    internal class ShowFromDb
    {
        public static void ShowAllStudents()
        {
            Console.Clear();
            using (SqlConnection connection = new SqlConnection(@"Data Source = (localdb)\.; Initial Catalog = Lab1SchoolDatabase; Integrated Security = True"))
            {
                connection.Open();
                

                using (SqlCommand command = new SqlCommand($"SELECT * FROM Students ORDER BY @SortName ASC", connection))
                {
                    //string sortName = GetSortByFirstOrLastName();
                    //string sortAscOrDesc = GetSortByAscOrDesc();
                    string sortName = Console.ReadLine();
                    command.Parameters.AddWithValue("@SortName", sortName);
                    //command.Parameters.AddWithValue("@SortAscOrDesc", sortAscOrDesc);
                    //string input = Console.ReadLine();
                    //command.Parameters.AddWithValue("@Name", input);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        
                        while (reader.Read())
                        {
                            Console.WriteLine($"\t{reader.GetString(0)} {reader.GetString(1)}, {reader.GetString(2)}" +
                                $"\n\t{reader.GetString(5)}" +
                                $"\n\t{reader.GetString(3)}" +
                                $"\n\t{reader.GetString(4)}");
                            Console.WriteLine("\t=============================");
                        }
                    }
                }
            }
            Console.WriteLine("\nPress ENTER to return to menu");
            Console.ReadKey();
            Console.Clear();
        }

        public static void ShowClasses()
        {
            Console.Clear();
            using (SqlConnection connection = new SqlConnection(@"Data Source = (localdb)\.; Initial Catalog = Lab1SchoolDatabase; Integrated Security = True"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"SELECT * FROM Classes", connection))
                {
                    //command.Parameters.AddWithValue();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int i = 1;
                        while (reader.Read())
                        {
                            Console.WriteLine($"\t{i}. {reader.GetString(1)}");
                            i++;
                        }
                    }
                    Console.Write("Select class by number: ");
                    string choice = Console.ReadLine();

                    using (SqlCommand com = new SqlCommand($"SELECT * FROM StudentList WHERE ClassName = 'Class{choice}'", connection))
                    {
                        using (SqlDataReader r = com.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                Console.WriteLine($"\t{r.GetString(0)} {r.GetString(1)}, {r.GetString(2)}" +
                                $"\n\t{r.GetString(5)}" +
                                $"\n\t{r.GetString(3)}" +
                                $"\n\t{r.GetString(4)}");
                                Console.WriteLine("\t=============================");
                            }
                        }
                    }
                }
            }
            Console.WriteLine("\nPress ENTER to return to menu");
            Console.ReadKey();
            Console.Clear();
        }

        private static string GetSortByFirstOrLastName()
        {
            string s;

            while (true)
            {
                Console.Write("Sort by first (F) or last (L) name: ");
                string input = Console.ReadLine().ToLower();
                if (input == "f")
                {
                    s = "StudentFirstName";
                    break;
                }
                else if (input == "l")
                {
                    s = "StudentLastName";
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect input. Try again.");
                }
            }
            Console.Clear();
            return s;
        }

        private static string GetSortByAscOrDesc()
        {
            Console.Write("Sort ascending (a) or descending (d): ");
            if (Console.ReadLine().ToLower() == "d")
            {
                Console.Clear();
                return "DESC";
            }
            else
            {
                Console.Clear();
                return "ASC";
            }
        }
    }
}
