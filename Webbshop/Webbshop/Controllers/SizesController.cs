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
    public class SizesController : Controller
    {
        private WebbShopEntities1 db = new WebbShopEntities1();

        // GET: Sizes
        public ActionResult Index()
        {
            var size = db.Size.Include(s => s.Product);
            return View(size.ToList());
        }

        // GET: Sizes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Size size = db.Size.Find(id);
            if (size == null)
            {
                return HttpNotFound();
            }
            return View(size);
        }

        // GET: Sizes/Create
        public ActionResult Create(int id)
        {
            var prod = from p in db.Product where p.Id == id select p;
            ViewBag.ProductName = (from n in prod select n.Name).First().ToString();

            ViewBag.Product_Id = new SelectList(prod, "Id", "Name");
            ViewBag.size = TempData["Size"];
            
            return View();
        }

        // POST: Sizes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Size1,Product_Id")] Size size)
        {
            if (ModelState.IsValid)
            {
                db.Size.Add(size);
                db.SaveChanges();
                TempData["Size"] = "Storleken " + size.Size1.ToString() + " har lagts till för detta plagg";
                return RedirectToAction("Create", new { id = size.Product_Id });
            }

            ViewBag.Product_Id = new SelectList(db.Product, "Id", "Name", size.Product_Id);
            return View(size);
        }

        // GET: Sizes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Size size = db.Size.Find(id);
            if (size == null)
            {
                return HttpNotFound();
            }
            ViewBag.Product_Id = new SelectList(db.Product, "Id", "Name", size.Product_Id);
            return View(size);
        }

        // POST: Sizes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Size1,Product_Id")] Size size)
        {
            if (ModelState.IsValid)
            {
                db.Entry(size).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Product_Id = new SelectList(db.Product, "Id", "Name", size.Product_Id);
            return View(size);
        }

        // GET: Sizes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Size size = db.Size.Find(id);
            if (size == null)
            {
                return HttpNotFound();
            }
            return View(size);
        }

        // POST: Sizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Size size = db.Size.Find(id);
            db.Size.Remove(size);
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
