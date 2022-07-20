using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeMVC.Models
{
    public class EmployeeDTO
    {
        public EmployeeModel EmployeeModel { get; set; }
        public List<EmployeeModel> EmployeeList { get; set; }
    }
}