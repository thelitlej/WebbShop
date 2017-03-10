using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Webbshop.Models;

namespace Webbshop.Controllers
{
    public class ProductsController : Controller
    {
        private WebbShopEntities1 db = new WebbShopEntities1();

        // GET: Products
        public ActionResult Index(Product prdct)
        {
           
            var product = db.Product.Include(p => p.Category).Include(p => p.Target_Group);
            return View(product.ToList());
            //return View();
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.Category_Id = new SelectList(db.Category, "Id", "Category1");
            ViewBag.Target_Group_Id = new SelectList(db.Target_Group, "Id", "Target_Group1");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Category_Id,Target_Group_Id,Price,Name,Product_Status,Quantity")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Create", "Colors");
            }

            ViewBag.Category_Id = new SelectList(db.Category, "Id", "Category1", product.Category_Id);
            ViewBag.Target_Group_Id = new SelectList(db.Target_Group, "Id", "Target_Group1", product.Target_Group_Id);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category_Id = new SelectList(db.Category, "Id", "Category1", product.Category_Id);
            ViewBag.Target_Group_Id = new SelectList(db.Target_Group, "Id", "Target_Group1", product.Target_Group_Id);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Category_Id,Target_Group_Id,Price,Name,Product_Status,Quantity")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category_Id = new SelectList(db.Category, "Id", "Category1", product.Category_Id);
            ViewBag.Target_Group_Id = new SelectList(db.Target_Group, "Id", "Target_Group1", product.Target_Group_Id);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
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
