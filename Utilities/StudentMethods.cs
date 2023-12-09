using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_SQL.Utilities
{
    internal class StudentMethods
    {
        public static void ShowAllStudents()
        {
            Console.Clear();
            using (SqlConnection connection = new SqlConnection(@"Data Source = (localdb)\.; Initial Catalog = Lab1SchoolDatabase; Integrated Security = True"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand($"SELECT * FROM StudentList", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            Console.WriteLine($"\t{reader.GetString(0)} {reader.GetString(1)}, {reader.GetString(2)}" +
                                $"\n\tClass: Class{reader.GetInt32(5)}" +
                                $"\n\tAddress: {reader.GetString(3)}" +
                                $"\n\tPhone: {reader.GetString(4)}");
                            Console.WriteLine("\t=============================");
                        }
                    }
                }
            }
            Program.EndMessage();
        }

        public static void ShowStudentByClass()
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

                    using (SqlCommand com = new SqlCommand($"SELECT * FROM StudentList WHERE Class = '{choice}'", connection))
                    {
                        using (SqlDataReader r = com.ExecuteReader())
                        {
                            if (r.HasRows)
                            {
                                while (r.Read())
                                {
                                    Console.WriteLine($"\t{r.GetString(0)} {r.GetString(1)}, {r.GetString(2)}" +
                                    $"\n\tClass: Class{r.GetInt32(5)}" +
                                    $"\n\t{r.GetString(3)}" +
                                    $"\n\t{r.GetString(4)}");
                                    Console.WriteLine("\t=============================");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"No students found in class Class{choice}");
                            }
                        }
                    }
                }
            }
            Program.EndMessage();
        }
    }
}
