using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_SQL.Models
{
    internal class Staff
    {
        public string StaffFirstName { get; set; }
        public string StaffLastName { get; set; }
        public string SSN { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string Role { get; set; }
        public int? ClassId { get; set; }
        public string CourseId { get; set; }
    }
}
