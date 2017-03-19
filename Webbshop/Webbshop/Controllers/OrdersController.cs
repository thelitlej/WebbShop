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
    public class OrdersController : Controller
    {
        private WebbShopEntities1 db = new WebbShopEntities1();

        // GET: Orders
        public ActionResult Index()
        {
            var order = db.Order.Include(o => o.User);
            return View(order.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.User_Id = new SelectList(db.User, "Id", "First_Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,User_Id,Order_Status,Order_Number,Total_Price,Address,Postal_Code,City,Country")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Order.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.User_Id = new SelectList(db.User, "Id", "First_Name", order.User_Id);
            return View(order);
        }
        //
        //create order auto
        public ActionResult CreateOrderFromShop(int colorId, int sizeId, int productId)
        {
            var user = (Session["User"] as User);
            int userId = user.Id;

            if (user == null) {
                TempData["LogInNeeded"] = "Du måste logga in för att handla";
                return RedirectToAction("ProductView", "Shop", new { id = productId, colorId = colorId });
            }
            var currentOrder = from co in db.Order where co.User_Id == userId && (co.Order_Status != "Betald" || co.Order_Status != "betald") select co;
            if (currentOrder.Count() == 0)
            {
                var userInfo = (from ui in db.User where ui.Id == userId select ui).FirstOrDefault();

                return RedirectToAction("CreateNewOrder", new
                {
                    User_Id = user.Id,
                    Order_Status = "Inte betald",
                    Order_Number = "0",
                    Total_Price = (float)(from p in db.Product where p.Id == productId select p.Price).FirstOrDefault(),
                    Address = userInfo.Address,
                    Postal_Code = userInfo.Postal_Code,
                    City = userInfo.City,
                    Country = userInfo.Country,
                    colorId,
                    sizeId,
                    productId,
                    userId
                });
            }
            else
            {
                int orderId = currentOrder.FirstOrDefault().Id;
                return RedirectToAction("CreateNewOrder_Detail", "Order_Details", new
                {
                    Order_Id = orderId,
                    Product_Id = productId,
                    Color_Id = colorId,
                    Size_Id = sizeId,
                    Quantity = 1
                });
            }

        }
        //test
        public ActionResult CreateNewOrder([Bind(Include = "Id,User_Id,Order_Status,Order_Number,Total_Price,Address,Postal_Code,City,Country")] Order order, int colorId, int sizeId, int productId, int userId)
        {
            if (ModelState.IsValid)
            {
                db.Order.Add(order);
                db.SaveChanges();
                return RedirectToAction("CreateOrderFromShop", "Orders", new { colorId,  sizeId,  productId });
            }
            return RedirectToAction("Index", "Shop");
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_Id = new SelectList(db.User, "Id", "First_Name", order.User_Id);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,User_Id,Order_Status,Order_Number,Total_Price,Address,Postal_Code,City,Country")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_Id = new SelectList(db.User, "Id", "First_Name", order.User_Id);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Order.Find(id);
            db.Order.Remove(order);
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
