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
    public class Order_DetailsController : Controller
    {
        private WebbShopEntities1 db = new WebbShopEntities1();

        // GET: Order_Details
        public ActionResult Index()
        {
            var order_Details = db.Order_Details.Include(o => o.Color).Include(o => o.Order).Include(o => o.Product).Include(o => o.Size);
            return View(order_Details.ToList());
        }

        // GET: Order_Details/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Details order_Details = db.Order_Details.Find(id);
            if (order_Details == null)
            {
                return HttpNotFound();
            }
            return View(order_Details);
        }

        // GET: Order_Details/Create
        public ActionResult Create()
        {
            ViewBag.Color_Id = new SelectList(db.Color, "Id", "Color1");
            ViewBag.Order_Id = new SelectList(db.Order, "Id", "Order_Status");
            ViewBag.Product_Id = new SelectList(db.Product, "Id", "Name");
            ViewBag.Size_Id = new SelectList(db.Size, "Id", "Size1");
            return View();
        }

        // POST: Order_Details/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Order_Id,Product_Id,Color_Id,Size_Id,Quantity")] Order_Details order_Details)
        {
            if (ModelState.IsValid)
            {
                db.Order_Details.Add(order_Details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Color_Id = new SelectList(db.Color, "Id", "Color1", order_Details.Color_Id);
            ViewBag.Order_Id = new SelectList(db.Order, "Id", "Order_Status", order_Details.Order_Id);
            ViewBag.Product_Id = new SelectList(db.Product, "Id", "Name", order_Details.Product_Id);
            ViewBag.Size_Id = new SelectList(db.Size, "Id", "Size1", order_Details.Size_Id);
            return View(order_Details);
        }

        // GET: Order_Details/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Details order_Details = db.Order_Details.Find(id);
            if (order_Details == null)
            {
                return HttpNotFound();
            }
            ViewBag.Color_Id = new SelectList(db.Color, "Id", "Color1", order_Details.Color_Id);
            ViewBag.Order_Id = new SelectList(db.Order, "Id", "Order_Status", order_Details.Order_Id);
            ViewBag.Product_Id = new SelectList(db.Product, "Id", "Name", order_Details.Product_Id);
            ViewBag.Size_Id = new SelectList(db.Size, "Id", "Size1", order_Details.Size_Id);
            return View(order_Details);
        }

        // POST: Order_Details/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Order_Id,Product_Id,Color_Id,Size_Id,Quantity")] Order_Details order_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_Details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Color_Id = new SelectList(db.Color, "Id", "Color1", order_Details.Color_Id);
            ViewBag.Order_Id = new SelectList(db.Order, "Id", "Order_Status", order_Details.Order_Id);
            ViewBag.Product_Id = new SelectList(db.Product, "Id", "Name", order_Details.Product_Id);
            ViewBag.Size_Id = new SelectList(db.Size, "Id", "Size1", order_Details.Size_Id);
            return View(order_Details);
        }

        // GET: Order_Details/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Details order_Details = db.Order_Details.Find(id);
            if (order_Details == null)
            {
                return HttpNotFound();
            }
            return View(order_Details);
        }

        // POST: Order_Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order_Details order_Details = db.Order_Details.Find(id);
            db.Order_Details.Remove(order_Details);
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
