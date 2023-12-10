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
