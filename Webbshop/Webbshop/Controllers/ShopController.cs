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
            if (user != null)
            {
                return RedirectToAction("Index", "Shop");

            }

            var cart = db.Order_Details.Include(o => o.Color).Include(o => o.Order).Include(o => o.Product).Include(o => o.Size); 
           
            return View(cart.ToList());
        }
    }
}