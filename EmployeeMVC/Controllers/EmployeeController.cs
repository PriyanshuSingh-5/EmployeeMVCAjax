using ClosedXML.Excel;
using EmployeeMVC.Manager;
using EmployeeMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeMVC.Controllers
{
    public class EmployeeController : Controller
    {
        EmpRepository repository = new EmpRepository();
        //
        // GET: /Employee/
        public ActionResult Index()
        {
            //var emData = new EmployeeDTO()
            //{
            //    EmployeeList = repository.GetAllEmployee().ToList()
            //};
            //// ViewBag.datasource = emData;
            //// ViewBag.ddData = new string[] { "Top", "Bottom" };
            //return View(emData);
            return View();
        }

        public ActionResult AddEmployee()
        {
            var emData = new EmployeeDTO()
            {
                EmployeeList = repository.GetAllEmployee().ToList()
            };
            // ViewBag.datasource = emData;
            // ViewBag.ddData = new string[] { "Top", "Bottom" };
            return View(emData);
            //return View();
            
        }
        [HttpPost]
        public ActionResult AddEmployee([Bind] EmployeeDTO emp)
        {
            try
            {
                if (emp.EmployeeModel.EmployeeId == 0)
                {
                    repository.AddEmployee(emp.EmployeeModel);
                    return RedirectToAction("AddEmployee");
                }
                else
                {
                    repository.GetAllEmployee().Where(e => e.EmployeeId == emp.EmployeeModel.EmployeeId).FirstOrDefault();
                    repository.UpdateEmployee(emp.EmployeeModel);
                    return RedirectToAction("AddEmployee");
                }
                //return RedirectToAction("AddEmployee");
            }
            catch (Exception)
            {

                throw;
            }
        }

        //[HttpGet]
        //public ActionResult EmpUpdate(int id)
        //{

        //    var emData = new EmployeeDTO()
        //    {
        //        EmployeeList = repository.GetAllEmployee().ToList(),
        //        EmployeeModel = repository.GetAllEmployee().Where(e => e.EmployeeId == id).FirstOrDefault()
        //    };
        //    // repository.UpdateEmployee(emp.EmployeeModel);
        //    //return View("AddEmployee", emData);
        //    return RedirectToAction("AddEmployee");
        //}

       //[HttpPost]
       // public ActionResult EmpUpdate(EmployeeDTO emp)
       // {
       //     EmployeeModel employee = repository.GetAllEmployee().Where(e => e.EmployeeId == emp.EmployeeModel.EmployeeId).FirstOrDefault();
       //     //(from x in repository.GetAllEmployee() where x.EmployeeId == emp.EmployeeModel.EmployeeId select x).FirstOrDefault();
       //      //repository.GetAllEmployee().Where(e => e.EmployeeId == emp.EmployeeModel.EmployeeId).FirstOrDefault();
       //      //repository.GetAllEmployee().ToList();
       //     // repository.GetEmployeeData(emp.EmployeeModel.EmployeeId);
       //     repository.UpdateEmployee(emp.EmployeeModel);
       //     //return RedirectToAction("AddEmployee");
       //     return new EmptyResult();
       // }

        public JsonResult UpdateEmp(EmployeeModel emp)
        {
            EmployeeModel employee = repository.GetAllEmployee().Where(e => e.EmployeeId == emp.EmployeeId).FirstOrDefault();
            repository.UpdateEmployee(emp);
            return Json(emp, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        public ActionResult DeleteEmployee(int id)
        {
            try
            {
                EmployeeModel employee = repository.GetAllEmployee().Where(e => e.EmployeeId == id).FirstOrDefault();
                repository.DeleteEmployee(id);
                return RedirectToAction("AddEmployee");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public FileResult ExportFile()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[7]
            {
                new DataColumn("Name"),
                new DataColumn("ProfileImage"),
                new DataColumn("Gender"),
                new DataColumn("Department"),
                new DataColumn("Salary"),
                new DataColumn("StartDate"),
                new DataColumn("Notes")

            });
            var emps = from emp in repository.GetAllEmployee().ToList() select emp;
            foreach (var emp in emps)
            {
                dt.Rows.Add(emp.Name, emp.ProfileImage, emp.Gender, emp.Department, emp.Salary, emp.StartDate, emp.Notes);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }
	}
}