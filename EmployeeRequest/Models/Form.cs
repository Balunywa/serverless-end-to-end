using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRequest.Models
{
    class Form
    {
        public Documents docs { get; set; }
        public Employee employee { get; set; }
    }
    class Documents
    {
        public string SocialSecurityCard { get; set; }
        public string DriversLicense { get; set; }
    }
    class Employee
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public DateTime startDate { get; set; }
        public string alias { get; set; }
        public string managerEmail { get; set; }
        public string managerId { get; set; }
        public string department { get; set; }
        public string driversImage { get; set; }
        public string socialImage { get; set; }

        public string tempPassword { get; set; }
    }
}
