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
    public class ProductController : Controller
    {
        private ShopEntities db = new ShopEntities();

        // GET: Product
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Brand).Include(p => p.Catagory).Include(p => p.Employee).Include(p => p.Unit);
            return View(products.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.brandId = new SelectList(db.Brands, "id", "name");
            ViewBag.catagoryId = new SelectList(db.Catagories, "id", "name");
            ViewBag.empid = new SelectList(db.Employees, "id", "name");
            ViewBag.unitId = new SelectList(db.Units, "id", "name");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,code,description,price,tag,catagoryId,brandId,unitId,addedBy,empid,lastUpdate,productAddedDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.brandId = new SelectList(db.Brands, "id", "name", product.brandId);
            ViewBag.catagoryId = new SelectList(db.Catagories, "id", "name", product.catagoryId);
            ViewBag.empid = new SelectList(db.Employees, "id", "name", product.empid);
            ViewBag.unitId = new SelectList(db.Units, "id", "name", product.unitId);
            return View(product);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.brandId = new SelectList(db.Brands, "id", "name", product.brandId);
            ViewBag.catagoryId = new SelectList(db.Catagories, "id", "name", product.catagoryId);
            ViewBag.empid = new SelectList(db.Employees, "id", "name", product.empid);
            ViewBag.unitId = new SelectList(db.Units, "id", "name", product.unitId);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,code,description,price,tag,catagoryId,brandId,unitId,addedBy,empid,lastUpdate,productAddedDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.brandId = new SelectList(db.Brands, "id", "name", product.brandId);
            ViewBag.catagoryId = new SelectList(db.Catagories, "id", "name", product.catagoryId);
            ViewBag.empid = new SelectList(db.Employees, "id", "name", product.empid);
            ViewBag.unitId = new SelectList(db.Units, "id", "name", product.unitId);
            return View(product);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
