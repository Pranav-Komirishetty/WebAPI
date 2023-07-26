using EmployeeRESTAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeRESTAPI.Controllers
{
    public class EmployeesController : ApiController
    {
        public HttpResponseMessage Get()
        {
            List<EMP_Table> employees = new List<EMP_Table>();

            using (ADOdemoEntities empDbContext = new ADOdemoEntities())
            {
                employees = empDbContext.EMP_Table.ToList();
                if(employees.Count == 0 ) 
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "No records found");
                }
                return Request.CreateResponse(HttpStatusCode.OK, employees);
            }
        }
        public HttpResponseMessage Get(int id)
        {
            using (ADOdemoEntities empDbContext = new ADOdemoEntities())
            {
                var employee =  empDbContext.EMP_Table.FirstOrDefault(a => a.ID == id);
                if(employee != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, employee);
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "No record found associated with ID "+id+" in the Database");
            }
        }
        public HttpResponseMessage Post(EMP_Table employee)
        {
            using(ADOdemoEntities empDbContext = new ADOdemoEntities())
            {
                if (employee != null)
                {
                    empDbContext.EMP_Table.Add(employee);
                    empDbContext.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created, employee);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Please enter values");
                }
            }
        }
        public HttpResponseMessage Put(int id,EMP_Table employee)
        {
            using (ADOdemoEntities empDbContext = new ADOdemoEntities())
            {
                var emp = empDbContext.EMP_Table.FirstOrDefault(a => a.ID == id);
                if (emp != null)
                {
                    emp.EmployeeName = employee.EmployeeName;
                    emp.Email = employee.Email;
                    emp.Age = employee.Age;
                    emp.Salary = employee.Salary;
                    empDbContext.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK,  employee);
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "No record found associated with ID " + id + " in the Database");
            }
        }
        public HttpResponseMessage Delete(int id)
        {
            using (ADOdemoEntities empDbContext = new ADOdemoEntities())
            {
                var employee = empDbContext.EMP_Table.FirstOrDefault(a => a.ID == id);
                if (employee != null)
                {
                    empDbContext.EMP_Table.Remove(employee);
                    empDbContext.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, employee);
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "No record found associated with ID " + id + " in the Database");
            }
        }
    }
}
