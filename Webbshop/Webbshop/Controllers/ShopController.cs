using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Webbshop.Models;
using System.Net;

namespace Webbshop.Controllers
{
    public class ShopController : Controller
    {
        private WebbShopEntities1 db = new WebbShopEntities1();
      
        public ActionResult Index()
        {
            List<Product> prod = db.Product.ToList();
            List<Color> color = db.Color.ToList();
            List<Size> size = db.Size.ToList();
            return View(Tuple.Create(prod, color, size));
        }


        public ActionResult ProductView(int? id, int? colorId)
        {
            ViewBag.LogInNeeded = (String)TempData["LogInNeeded"];

            List<Color> sje = (from i in db.Color where i.Product_Id == id && i.Id == colorId select i).ToList();
            List<Color> sje2 = (from i in db.Color where i.Product_Id == id && i.Id != colorId select i).ToList();
            sje.AddRange(sje2);

            ViewBag.ColorId = new SelectList(sje, "Id", "Color1");
            ViewBag.SizeId = new SelectList((from i in db.Size where i.Product_Id == id select i).ToList(), "Id", "Size1");

            Color col = db.Color.Find(colorId);
            Product prod = db.Product.Find(id);
            List<Color> color = (from i in db.Color where i.Product_Id == id select i).ToList();
            List<Size> size = (from i in db.Size where i.Product_Id == id select i).ToList();
            return View(Tuple.Create(prod, color, size, col));
        }
        public ActionResult Cart()
        {
            var user = (Session["User"] as User);
            int userId = user.Id;
            if (user == null)
            {
                return RedirectToAction("Index", "Shop");

            }
            var tmp = (from o in db.Order
                       join o_d in db.Order_Details on o.Id equals o_d.Order_Id
                       where o.User_Id == userId && (o.Order_Status != "Betald" || o.Order_Status != "betald")
                       select o.Id).FirstOrDefault();

            var cart = db.Order_Details.Include(o => o.Order).Where(o => o.Order.Id == tmp);
               cart = cart.Include(o => o.Product).Include(o => o.Size).Include(o => o.Color);

            return View(cart.ToList());
        }
        public ActionResult Delete(int id)
        {
            Order_Details order_Details = db.Order_Details.Find(id);
            db.Order_Details.Remove(order_Details);
            db.SaveChanges();

            var userId = (Session["User"] as User).Id;
            Session["Cart"] = (from o in db.Order
                               join o_d in db.Order_Details on o.Id equals o_d.Order_Id
                               where o.User_Id == userId && (o.Order_Status != "Betald" || o.Order_Status != "betald")
                               select o_d.Id).Count();

            return RedirectToAction("Cart");
        }
        public ActionResult Payed()
        {
            var userId = (Session["User"] as User).Id;
            int nonPayed = (from o in db.Order
                            where o.User_Id == userId && (o.Order_Status != "Betald" || o.Order_Status != "betald")
                            select o).Count();

            if(nonPayed > 0)
            {
                Order orderToUpdate = (from o in db.Order
                                       where o.User_Id == userId && (o.Order_Status != "Betald" || o.Order_Status != "betald")
                                       select o).FirstOrDefault();

                orderToUpdate.Order_Status = "Betald";
                db.SaveChanges();

                Session["Cart"] = (from o in db.Order
                                   join o_d in db.Order_Details on o.Id equals o_d.Order_Id
                                   where o.User_Id == userId && (o.Order_Status != "Betald" || o.Order_Status != "betald")
                                   select o_d.Id).Count();
            }
            
            return RedirectToAction("Cart");
        }

    }
}