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
    public class ReligionController : Controller
    {
        private ShopEntities db = new ShopEntities();

        // GET: Religion
        public ActionResult Index()
        {
            return View(db.Religions.ToList());
        }

        // GET: Religion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Religion religion = db.Religions.Find(id);
            if (religion == null)
            {
                return HttpNotFound();
            }
            return View(religion);
        }

        // GET: Religion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Religion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] Religion religion)
        {
            if (ModelState.IsValid)
            {
                db.Religions.Add(religion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(religion);
        }

        // GET: Religion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Religion religion = db.Religions.Find(id);
            if (religion == null)
            {
                return HttpNotFound();
            }
            return View(religion);
        }

        // POST: Religion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] Religion religion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(religion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(religion);
        }

        // GET: Religion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Religion religion = db.Religions.Find(id);
            if (religion == null)
            {
                return HttpNotFound();
            }
            return View(religion);
        }

        // POST: Religion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Religion religion = db.Religions.Find(id);
            db.Religions.Remove(religion);
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
