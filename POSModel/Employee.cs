using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSModel
{
    public class Employee
    {
        public int EmpID { get; set; }
        public int EmployeePositionID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string IDNo { get; set; }
        public string Address { get; set; }
        public int ReportTo { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }


    }
}
