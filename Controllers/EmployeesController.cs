using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PersonnelInformationWebAPI.Models;

namespace PersonnelInformationWebAPI.Controllers
{
    public class EmployeesController : ApiController
    {
        private AvaNyadeEmployee db = new AvaNyadeEmployee();

        // GET: api/Employees
        public IQueryable<Employees> GetEmployees()
        {
            return db.Employees;
        }

        // GET: api/Employees/5
        [ResponseType(typeof(Employees))]
        public IHttpActionResult GetEmployees(int id)
        {
            Employees employees = null;
            try
            {
                employees = db.Employees.Find(id);
            }
            catch (Exception ex)
            {
               
                return InternalServerError(ex);
            }

            if (employees == null)
            {
                return NotFound();
            }

            return Ok(employees);
        }


        [HttpGet]
        public IHttpActionResult GetEmployeesWithDetails(string searchTerm)
        {
            var employees = from e in db.Employees
                            join b in db.BelongTo on e.EmployeeID equals b.EmployeeID
                            join d in db.Departments on b.DepartmentID equals d.DepartmentID
                            where string.IsNullOrEmpty(searchTerm) || e.EmployeeName.Contains(searchTerm) // 社員名で検索
                            select new
                            {
                                EmployeeID = e.EmployeeID,
                                EmployeeName = e.EmployeeName,
                                Email = e.Email,
                                Height = e.Height,
                                Weight = e.Weight,
                                HireFiscalYear = e.HireFiscalYear,
                                Birthday = e.Birthday,
                                BloodType = e.BloodType,
                                Password = e.PassWords,
                                DepartmentName = d.DepartmentName,
                                EmploymentId = b.BelongID,
                                EntryDate = b.StartDate,
                                RetirementDate = b.EndDate
                            };

            return Ok(employees);
        }

        [HttpGet]
        public IHttpActionResult GetEmployeesWithDetails()
        {
            return GetEmployeesWithDetails(string.Empty);
        }



        [HttpPost]
        public IHttpActionResult Login(LoginModel model)
        {
            var employee = db.Employees.FirstOrDefault(e => e.Email == model.Email);
            if (employee == null)
            {
                return Content(HttpStatusCode.BadRequest, new { message = "Invalid email" });
            }

            if (employee.PassWords != model.Password)  // dangerous password validation
            {
                return Content(HttpStatusCode.BadRequest, new { message = "Invalid password" });
            }

            var belong = db.BelongTo.FirstOrDefault(b => b.EmployeeID == employee.EmployeeID);
            bool isHR = belong != null && belong.DepartmentID == 3;

            return Ok(new { message = "Login successful", IsHR = isHR });
        }



        public class LoginModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }


        // PUT: api/Employees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployees(int id, Employees employees)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employees.EmployeeID)
            {
                return BadRequest();
            }

            db.Entry(employees).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Employees
        [ResponseType(typeof(Employees))]
        public IHttpActionResult PostEmployees(Employees employees)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employees.Add(employees);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EmployeesExists(employees.EmployeeID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = employees.EmployeeID }, employees);
        }

        // DELETE: api/Employees/5
        [ResponseType(typeof(Employees))]
        public IHttpActionResult DeleteEmployees(int id)
        {
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employees);
            db.SaveChanges();

            return Ok(employees);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeesExists(int id)
        {
            return db.Employees.Count(e => e.EmployeeID == id) > 0;
        }
    }
}