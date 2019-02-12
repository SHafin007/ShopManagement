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
    public class EmployeetImageController : Controller
    {
        private ShopEntities db = new ShopEntities();

        // GET: EmployeetImage
        public ActionResult Index()
        {
            var employeetImages = db.EmployeetImages.Include(e => e.Employee);
            return View(employeetImages.ToList());
        }

        // GET: EmployeetImage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeetImage employeetImage = db.EmployeetImages.Find(id);
            if (employeetImage == null)
            {
                return HttpNotFound();
            }
            return View(employeetImage);
        }

        // GET: EmployeetImage/Create
        public ActionResult Create()
        {
            ViewBag.empId = new SelectList(db.Employees, "id", "name");
            return View();
        }

        // POST: EmployeetImage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeetImage employeetImage, HttpPostedFileBase image)
        {
            employeetImage.image = System.IO.Path.GetFileName(image.FileName);
            if (ModelState.IsValid)
            {
                db.EmployeetImages.Add(employeetImage);
                db.SaveChanges();

                image.SaveAs(Server.MapPath("../Photos/Employee/" + employeetImage.id.ToString()+"_" + employeetImage.image));
                return RedirectToAction("Index");
            }

            ViewBag.empId = new SelectList(db.Employees, "id", "name", employeetImage.empId);
            return View(employeetImage);
        }

        // GET: EmployeetImage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeetImage employeetImage = db.EmployeetImages.Find(id);
            if (employeetImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.empId = new SelectList(db.Employees, "id", "name", employeetImage.empId);
            return View(employeetImage);
        }

        // POST: EmployeetImage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,empId,image")] EmployeetImage employeetImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeetImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.empId = new SelectList(db.Employees, "id", "name", employeetImage.empId);
            return View(employeetImage);
        }

        // GET: EmployeetImage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeetImage employeetImage = db.EmployeetImages.Find(id);
            if (employeetImage == null)
            {
                return HttpNotFound();
            }
            return View(employeetImage);
        }

        // POST: EmployeetImage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeetImage employeetImage = db.EmployeetImages.Find(id);
            db.EmployeetImages.Remove(employeetImage);
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
