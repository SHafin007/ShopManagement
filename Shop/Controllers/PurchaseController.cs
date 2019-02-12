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
    public class PurchaseController : Controller
    {
        private ShopEntities db = new ShopEntities();

        // GET: Purchase
        public ActionResult Index()
        {
            var purchases = db.Purchases.Include(p => p.Product).Include(p => p.Supplier).Include(p => p.Unit);
            return View(purchases.ToList());
        }

        // GET: Purchase/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // GET: Purchase/Create
        public ActionResult Create()
        {
            ViewBag.productId = new SelectList(db.Products, "id", "name");
            ViewBag.supplierId = new SelectList(db.Suppliers, "id", "name");
            ViewBag.unitId = new SelectList(db.Units, "id", "name");
            return View();
        }

        // POST: Purchase/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,productId,purchaseDate,unitId,price,supplierId,addedBy")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                db.Purchases.Add(purchase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productId = new SelectList(db.Products, "id", "name", purchase.productId);
            ViewBag.supplierId = new SelectList(db.Suppliers, "id", "name", purchase.supplierId);
            ViewBag.unitId = new SelectList(db.Units, "id", "name", purchase.unitId);
            return View(purchase);
        }

        // GET: Purchase/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            ViewBag.productId = new SelectList(db.Products, "id", "name", purchase.productId);
            ViewBag.supplierId = new SelectList(db.Suppliers, "id", "name", purchase.supplierId);
            ViewBag.unitId = new SelectList(db.Units, "id", "name", purchase.unitId);
            return View(purchase);
        }

        // POST: Purchase/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,productId,purchaseDate,unitId,price,supplierId,addedBy")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productId = new SelectList(db.Products, "id", "name", purchase.productId);
            ViewBag.supplierId = new SelectList(db.Suppliers, "id", "name", purchase.supplierId);
            ViewBag.unitId = new SelectList(db.Units, "id", "name", purchase.unitId);
            return View(purchase);
        }

        // GET: Purchase/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // POST: Purchase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            db.Purchases.Remove(purchase);
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
