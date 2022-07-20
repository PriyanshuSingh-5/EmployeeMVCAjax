using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeMVC.Models
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string ProfileImage { get; set; }
        public string Gender { get; set; }
        public string Department { get; set; }
        public int Salary { get; set; }
        public DateTime StartDate { get; set; }
        public string Notes { get; set; }
    }
}