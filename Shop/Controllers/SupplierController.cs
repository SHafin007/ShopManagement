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
    public class SupplierController : Controller
    {
        private ShopEntities db = new ShopEntities();

        // GET: Supplier
        public ActionResult Index()
        {
            var suppliers = db.Suppliers.Include(s => s.City).Include(s => s.Department).Include(s => s.Designation).Include(s => s.Gender).Include(s => s.Organization).Include(s => s.Religion);
            return View(suppliers.ToList());
        }

        // GET: Supplier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            ViewBag.cityId = new SelectList(db.Cities, "id", "name");
            ViewBag.departmentId = new SelectList(db.Departments, "id", "name");
            ViewBag.designationId = new SelectList(db.Designations, "id", "name");
            ViewBag.genderId = new SelectList(db.Genders, "id", "name");
            ViewBag.orgainzationId = new SelectList(db.Organizations, "id", "name");
            ViewBag.religionId = new SelectList(db.Religions, "id", "name");
            return View();
        }

        // POST: Supplier/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,address,email,phone,addedBy,nationalIdNumber,dateOfBirth,genderId,departmentId,designationId,religionId,cityId,orgainzationId,SupplierAddedDate,status")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Suppliers.Add(supplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cityId = new SelectList(db.Cities, "id", "name", supplier.cityId);
            ViewBag.departmentId = new SelectList(db.Departments, "id", "name", supplier.departmentId);
            ViewBag.designationId = new SelectList(db.Designations, "id", "name", supplier.designationId);
            ViewBag.genderId = new SelectList(db.Genders, "id", "name", supplier.genderId);
            ViewBag.orgainzationId = new SelectList(db.Organizations, "id", "name", supplier.orgainzationId);
            ViewBag.religionId = new SelectList(db.Religions, "id", "name", supplier.religionId);
            return View(supplier);
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            ViewBag.cityId = new SelectList(db.Cities, "id", "name", supplier.cityId);
            ViewBag.departmentId = new SelectList(db.Departments, "id", "name", supplier.departmentId);
            ViewBag.designationId = new SelectList(db.Designations, "id", "name", supplier.designationId);
            ViewBag.genderId = new SelectList(db.Genders, "id", "name", supplier.genderId);
            ViewBag.orgainzationId = new SelectList(db.Organizations, "id", "name", supplier.orgainzationId);
            ViewBag.religionId = new SelectList(db.Religions, "id", "name", supplier.religionId);
            return View(supplier);
        }

        // POST: Supplier/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,address,email,phone,addedBy,nationalIdNumber,dateOfBirth,genderId,departmentId,designationId,religionId,cityId,orgainzationId,SupplierAddedDate,status")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cityId = new SelectList(db.Cities, "id", "name", supplier.cityId);
            ViewBag.departmentId = new SelectList(db.Departments, "id", "name", supplier.departmentId);
            ViewBag.designationId = new SelectList(db.Designations, "id", "name", supplier.designationId);
            ViewBag.genderId = new SelectList(db.Genders, "id", "name", supplier.genderId);
            ViewBag.orgainzationId = new SelectList(db.Organizations, "id", "name", supplier.orgainzationId);
            ViewBag.religionId = new SelectList(db.Religions, "id", "name", supplier.religionId);
            return View(supplier);
        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier supplier = db.Suppliers.Find(id);
            db.Suppliers.Remove(supplier);
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
