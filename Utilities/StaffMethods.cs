using Lab1_SQL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_SQL.Utilities
{
    internal class StaffMethods
    {
        public static void GetStaff()
        {
            Console.Clear();
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\.;Initial Catalog=Lab1SchoolDatabase;Integrated Security=True"))
            {
                con.Open();

                // Lets user decide if they want to view all staff, or a certain role. Then asks for sorting order.
                string whereSelection = GetSortByRole();
                string sort = GetSorting();
                
                using (SqlCommand cmd = new SqlCommand(@$"SELECT StaffFirstName, StaffLastName, SSN, Address, PhoneNo, Role, Class FROM Staff {whereSelection} ORDER BY {sort}", con))
                {

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.Clear();
                        Console.WriteLine();
                        while (reader.Read())
                        {
                            Console.WriteLine($"\t{reader.GetString(1)}, {reader.GetString(0)}, " +
                                $"{reader.GetString(2)}" +
                                $"\n\tAddress: {reader.GetString(3)}" +
                                $"\n\tPhone: {reader.GetString(4)}" +
                                $"\n\tTitle: {reader.GetString(5)}"
                                );

                            if (!reader.IsDBNull(6))
                            {
                                Console.WriteLine($"\tClass: Class{reader.GetInt32(6)}");
                            }
                            Console.WriteLine("------------------------------------");
                        }
                    }
                }
            }

            Program.EndMessage();
        }

        public static void AddStaff()
        {
            Console.Clear();
            Staff newTeacher = new Staff();
            Console.Write("First name: ");
            newTeacher.StaffFirstName = Console.ReadLine();

            Console.Write("Last name: ");
            newTeacher.StaffLastName = Console.ReadLine();

            Console.Write("SSN: ");
            newTeacher.SSN = Console.ReadLine();

            Console.Write("Address: ");
            newTeacher.Address = Console.ReadLine();

            Console.Write("Phone number: ");
            newTeacher.PhoneNo = Console.ReadLine();

            Console.Write("Role: ");
            newTeacher.Role = Console.ReadLine();

            //if (newTeacher.Role == "Teacher")
            //{
                //Console.Write("Class (leave blank if none): ");
                //string classId = Console.ReadLine();
                //if (classId != "")
                //{
                //    newTeacher.ClassId = int.Parse(classId);
                //}

                //Console.Write("Course (leave blank if none): ");
                //newTeacher.CourseId = Console.ReadLine();
                //if (newTeacher.CourseId == "")
                //{
                //    newTeacher.CourseId = null;
                //}
            //}


            using (SqlConnection connection = new SqlConnection(@"Data Source = (localdb)\.; Initial Catalog = Lab1SchoolDatabase; Integrated Security = True"))
            {
                connection.Open();
                using (SqlCommand com = new SqlCommand($"INSERT INTO Staff (StaffFirstName, StaffLastName, " +
                    $"SSN, " +
                    $"Address, " +
                    $"PhoneNo, " +
                    $"Role) " +
                    $"VALUES " +
                    $"('{newTeacher.StaffFirstName}', " +
                    $"'{newTeacher.StaffLastName}', " +
                    $"'{newTeacher.SSN}', " +
                    $"'{newTeacher.Address}', " +
                    $"'{newTeacher.PhoneNo}', " +
                    $"'{newTeacher.Role}')", connection))
                {
                    com.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Staff added to database!");
            Program.EndMessage();
        }

        private static string GetSorting()
        {
            Console.Write("Sort by last(l) or first(f) name (leave blank for default)? ");
            string nameSort = Console.ReadLine();
            Console.Write("Sort staff ascending(a) or descending(d) (leave blank for default)? ");
            string sortInput = Console.ReadLine();

            switch (nameSort)
            {
                case "f" when sortInput == "d":
                    return "StaffFirstName DESC";
                case "f" when sortInput == "a":
                    return "StaffFirstName ASC";
                case "l" when sortInput == "d":
                    return "StaffLastName DESC";
                default:
                    return "StaffLastName ASC";
            }
        }

        private static string GetSortByRole()
        {
            StringBuilder str = new StringBuilder("WHERE Role = ");
            Console.WriteLine("Select category:");
            Console.WriteLine("1. Principal" +
                "\n2. Administrator" +
                "\n3. Teacher" +
                "\n4. View all staff");
            while (true)
            {
                Console.Write("Enter choice: ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        return str + "'Principal'";
                    case "2":
                        return str + "'Administrator'";
                    case "3":
                        return str + "'Teacher'";
                    case "4":
                        return "";
                    default:
                        Console.WriteLine("Invalind input. Try again.");
                        break;
                }
            }
        }
    }
}
