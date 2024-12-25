using EmployeeCVUpload.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCVUpload.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeContext _db;
        public EmployeeController(EmployeeContext db)
        {
            _db = db;
        }
        public IActionResult Employeelist()
        {
            try
            {
                var employeelist = from a in _db.tbl_Employee
                                   join b in _db.tbl_Departments
                                   on a.DeptId equals b.DeptId
                                   into tbl_Departments
                                   from b in tbl_Departments.DefaultIfEmpty()

                                   select new Employee
                                   {
                                       Id = a.Id,
                                       Name = a.Name,
                                       Mobile = a.Mobile,
                                       Email = a.Mobile,
                                       Description = a.Description,
                                       DeptId = a.DeptId,
                                       Department = b == null ? "No Department" : b.Department // Default to "No Department" if b is null
                                   };
                return View(employeelist);
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            loadDDl();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Employee emp)
        {
            try
            {
                if (emp.Id == 0)
                {
                    _db.tbl_Employee.Add(emp);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    _db.Entry(emp).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                }
                return RedirectToAction("Employeelist");
            }
            catch (Exception)
            {
                return RedirectToAction("Employeelist");
            }
        }
        [HttpGet]
        [Route("Employee/Edit/{id}")]
        public ActionResult Edit(int id)
        {
            var employee = _db.tbl_Employee.Find(id);
            if(employee==null)
            {
                //return HttpNotFound();

            }
            loadDDl();
            return View(employee);
        }
        public async Task<ActionResult> Edit(Employee employee)
        {
            if(ModelState.IsValid)
            {
                _db.Entry(employee).State = EntityState.Modified;
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Employeelist");
        }


        private void loadDDl()
        {
            try
            {
                List<Depe> depelist = new List<Depe>();
                depelist = _db.tbl_Departments.ToList();
                depelist.Insert(0, new Depe { DeptId = -1, Department = "Select" });
                ViewBag.depelist = depelist;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Delete(int id)
        {
            var employee = _db.tbl_Employee.Find(id);

            if(employee!=null)
            {
                _db.tbl_Employee.Remove(employee);
                _db.SaveChanges();
            }
            
            return RedirectToAction("Employeelist");
        }



    }
}
