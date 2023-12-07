using Lab1_SQL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_SQL
{
    internal class AddToDb
    {
        public static void AddStaff()
        {
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

            using (SqlConnection connection = new SqlConnection(@"Data Source = (localdb)\.; Initial Catalog = Lab1SchoolDatabase; Integrated Security = True"))
            {
                connection.Open();
                using (SqlCommand com = new SqlCommand($"INSERT INTO Staff (StaffFirstName, StaffLastName, " +
                    $"SSN, " +
                    $"Address, " +
                    $"PhoneNo) " +
                    $"VALUES " +
                    $"('{newTeacher.StaffFirstName}', " +
                    $"'{newTeacher.StaffLastName}', " +
                    $"'{newTeacher.SSN}', " +
                    $"'{newTeacher.Address}', " +
                    $"'{newTeacher.PhoneNo}')", connection))
                {
                    com.ExecuteNonQuery();
                }
            }
        }
    }
}
