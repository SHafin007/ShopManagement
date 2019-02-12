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
    public class ProductImageController : Controller
    {
        private ShopEntities db = new ShopEntities();

        // GET: ProductImage
        public ActionResult Index()
        {
            var productImages = db.ProductImages.Include(p => p.Product);
            return View(productImages.ToList());
        }

        // GET: ProductImage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImage = db.ProductImages.Find(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            return View(productImage);
        }

        // GET: ProductImage/Create
        public ActionResult Create()
        {
            ViewBag.productId = new SelectList(db.Products, "id", "name");
            return View();
        }

        // POST: ProductImage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( ProductImage productImage, HttpPostedFileBase image)
        {
            productImage.image = System.IO.Path.GetFileName(image.FileName);
            if (ModelState.IsValid)
            {
                db.ProductImages.Add(productImage);
                db.SaveChanges();
                image.SaveAs(Server.MapPath("../Photos/Product/" + productImage.id.ToString() + "_" + productImage.image));
                return RedirectToAction("Index");
            }

            ViewBag.productId = new SelectList(db.Products, "id", "name", productImage.productId);
            return View(productImage);
        }

        // GET: ProductImage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImage = db.ProductImages.Find(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.productId = new SelectList(db.Products, "id", "name", productImage.productId);
            return View(productImage);
        }

        // POST: ProductImage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,productId,image,dateTime")] ProductImage productImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productId = new SelectList(db.Products, "id", "name", productImage.productId);
            return View(productImage);
        }

        // GET: ProductImage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImage = db.ProductImages.Find(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            return View(productImage);
        }

        // POST: ProductImage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductImage productImage = db.ProductImages.Find(id);
            db.ProductImages.Remove(productImage);
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
