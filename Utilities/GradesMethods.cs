using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_SQL.Utilities
{
    internal class GradesMethods
    {
        public static void ShowAllGradesFromLastMonth()
        {
            Console.Clear();
            using (SqlConnection con = new SqlConnection(@"Data Source = (localdb)\.; Initial Catalog = Lab1SchoolDatabase; Integrated Security = True"))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM ShowGradesFromLastMonth", con))
                {
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            Console.WriteLine($"\tCourse: {r.GetString(2)}" +
                                $"\n\tStudent: {r.GetString(1)}, {r.GetString(0)}" +
                                $"\n\tGrade: {r.GetInt32(3)}" +
                                $"\n\tGrade set on: {r.GetDateTime(4).ToString().Substring(0, 10)}");
                            Console.WriteLine("\t---------------------------");
                        }
                    }
                }
            }
            Program.EndMessage();
        }

        public static void GradeStatisticsByCourse()
        {
            Console.Clear();
            using (SqlConnection con = new SqlConnection(@"Data Source = (localdb)\.; Initial Catalog = Lab1SchoolDatabase; Integrated Security = True"))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM GradesStatistics", con))
                {
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            Console.WriteLine($"\tCourse: {r.GetString(0)}" +
                                $"\n\tAverage grade: {r.GetInt32(1)}" +
                                $"\n\tHighest grade: {r.GetInt32(2)}" +
                                $"\n\tLowest grade: {r.GetInt32(3)}");
                            Console.WriteLine("\t-------------------------");
                        }
                    }
                }
            }
            Program.EndMessage();
        }
    }
}
