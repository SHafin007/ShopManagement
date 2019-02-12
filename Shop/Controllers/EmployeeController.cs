using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Shop.Models;

namespace Shop.Controllers
{
    public class EmployeeController : Controller
    {
        private ShopEntities db = new ShopEntities();

        // GET: Employee
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.City).Include(e => e.Department).Include(e => e.Designation).Include(e => e.Gender).Include(e => e.Religion);
            return View(employees.ToList());
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            ViewBag.cityId = new SelectList(db.Cities, "id", "name");
            ViewBag.departmentId = new SelectList(db.Departments, "id", "name");
            ViewBag.designationId = new SelectList(db.Designations, "id", "name");
            ViewBag.genderId = new SelectList(db.Genders, "id", "name");
            ViewBag.religionId = new SelectList(db.Religions, "id", "name");
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,address,email,phone,slary,addedBy,nationalIdNumber,dateOfBirth,genderId,departmentId,designationId,religionId,cityId,joinDate,employeeAddedDate,status")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cityId = new SelectList(db.Cities, "id", "name", employee.cityId);
            ViewBag.departmentId = new SelectList(db.Departments, "id", "name", employee.departmentId);
            ViewBag.designationId = new SelectList(db.Designations, "id", "name", employee.designationId);
            ViewBag.genderId = new SelectList(db.Genders, "id", "name", employee.genderId);
            ViewBag.religionId = new SelectList(db.Religions, "id", "name", employee.religionId);
            return View(employee);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.cityId = new SelectList(db.Cities, "id", "name", employee.cityId);
            ViewBag.departmentId = new SelectList(db.Departments, "id", "name", employee.departmentId);
            ViewBag.designationId = new SelectList(db.Designations, "id", "name", employee.designationId);
            ViewBag.genderId = new SelectList(db.Genders, "id", "name", employee.genderId);
            ViewBag.religionId = new SelectList(db.Religions, "id", "name", employee.religionId);
            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,address,email,phone,slary,addedBy,nationalIdNumber,dateOfBirth,genderId,departmentId,designationId,religionId,cityId,joinDate,employeeAddedDate,status")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cityId = new SelectList(db.Cities, "id", "name", employee.cityId);
            ViewBag.departmentId = new SelectList(db.Departments, "id", "name", employee.departmentId);
            ViewBag.designationId = new SelectList(db.Designations, "id", "name", employee.designationId);
            ViewBag.genderId = new SelectList(db.Genders, "id", "name", employee.genderId);
            ViewBag.religionId = new SelectList(db.Religions, "id", "name", employee.religionId);
            return View(employee);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
