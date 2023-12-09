using Lab1_SQL.Models;
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

        public static void AddStudent()
        {
            Console.Clear();
            Student newStudent = new Student();
            Console.Write("First name: ");
            newStudent.FirstName = Console.ReadLine();

            Console.Write("Last name: ");
            newStudent.LastName = Console.ReadLine();

            Console.Write("SSN: ");
            newStudent.SSN = Console.ReadLine();

            Console.Write("Address: ");
            newStudent.Address = Console.ReadLine();

            Console.Write("Phone number: ");
            newStudent.PhoneNo = Console.ReadLine();

            Console.Write("Class: ");
            string cl = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(@"Data Source = (localdb)\.; Initial Catalog = Lab1SchoolDatabase; Integrated Security = True"))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand($"INSERT INTO Students (StudentFirstName, StudentLastName, " +
                    $"SSN, " +
                    $"Address, " +
                    $"PhoneNo) " +
                    $"VALUES " +
                    $"('{newStudent.FirstName}', " +
                    $"'{newStudent.LastName}', " +
                    $"'{newStudent.SSN}', " +
                    $"'{newStudent.Address}', " +
                    $"'{newStudent.PhoneNo}')", connection))
                {
                    cmd.ExecuteNonQuery();

                    if (CheckForDigits(cl))
                    {
                        SqlCommand cmd2 = new SqlCommand($"UPDATE Students " +
                            $"SET Class = {cl} " +
                            $"WHERE SSN = '{newStudent.SSN}'", connection);
                        cmd2.ExecuteNonQuery();
                    }
                }
            }
            Console.WriteLine("Student added to database!");
            Program.EndMessage();
        }

        static bool CheckForDigits(string s)
        {
            if (s == "")
            {
                return false;
            }
            foreach (char c in s)
            {
                if (!Char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
