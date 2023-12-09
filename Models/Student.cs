using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_SQL.Models
{
    internal class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public int Class { get; set; }
        public Student()
        {
            
        }
        public Student(string firstName, string lastName, string ssn, string address, string phoneNo)
        {
            FirstName = firstName;
            LastName = lastName;
            SSN = ssn;
            Address = address;
            PhoneNo = phoneNo;
        }
        public Student(string firstName, string lastName, string ssn, string address, string phoneNo, int c)
        {
            FirstName = firstName;
            LastName = lastName;
            SSN = ssn;
            Address = address;
            PhoneNo = phoneNo;
            Class = c;
        }
    }
}
