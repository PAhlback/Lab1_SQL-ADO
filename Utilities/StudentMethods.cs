using Lab1_SQL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Channels;
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

                // Gets the sorting. Was unable to get this to work with Parameters.AddWithValue() inside the SqlCommands using statement.
                // Kept getting the below exception:
                /* 
                 * The SELECT item identified by the ORDER BY number 1 contains a variable as part of the expression identifying a column position. 
                 * Variables are only allowed when ordering by an expression referencing a column name
                 */
                // This method doesn't allow users to enter input by themselves, it is handled with options (so hopefully no SQL Injections).
                string sort = GetSorting();

                // Gets everything from the view StudentList.
                using (SqlCommand command = new SqlCommand($"SELECT * FROM StudentList ORDER BY {sort}", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"\t{reader.GetString(1)}, {reader.GetString(0)} {reader.GetString(2)}" +
                                $"\n\tAddress: {reader.GetString(3)}" +
                                $"\n\tPhone: {reader.GetString(4)}");
                            if (!reader.IsDBNull(5))
                            {
                                Console.WriteLine($"\tClass: Class{reader.GetInt32(5)}");
                            }
                            Console.WriteLine("\t====================");
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
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Using an int in the while loop to print the list of classes.
                        int i = 1;
                        while (reader.Read())
                        {
                            Console.WriteLine($"\t{i}. {reader.GetString(1)}");
                            i++;
                        }
                    }
                    
                    // Lets user input which class to show students from. Currently only checks that input is digits, not valid class.
                    string choice = string.Empty;
                    while (true)
                    {
                        Console.Write("Select class by number: ");
                        choice = Console.ReadLine();
                        if (CheckForDigits(choice))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Try again.");
                        }
                    }

                    // Same sort method as in ShowAllStudents() method.
                    string sort = GetSorting();                   

                    using (SqlCommand com = new SqlCommand($"SELECT * FROM StudentList WHERE Class = '{choice}' ORDER BY {sort}", connection))
                    {
                        using (SqlDataReader r = com.ExecuteReader())
                        {
                            // Using .HasRows in case the class doesn't have any students.
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

        private static string GetSorting()
        {
            Console.Write("Sort by last(l) or first(f) name (leave blank for default)? ");
            string nameSort = Console.ReadLine();
            Console.Write("Sort students ascending(a) or descending(d) (leave blank for default)? ");
            string sortInput = Console.ReadLine();

            switch (nameSort)
            {
                case "f" when sortInput == "d":
                    return "StudentFirstName DESC";
                case "f" when sortInput == "a":
                    return "StudentFirstName ASC";
                case "l" when sortInput == "d":
                    return "StudentLastName DESC";
                default:
                    return "StudentLastName ASC";
            }
        }
    }
}
