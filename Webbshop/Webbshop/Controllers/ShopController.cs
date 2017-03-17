using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbshop.Models;

namespace Webbshop.Controllers
{
    public class ShopController : Controller
    {
        private WebbShopEntities1 db = new WebbShopEntities1();
        // GET: Shop
        /*public ActionResult Index()
        {
            ProductAndDetails pad = new ProductAndDetails();
            pad.Product = (from p in db.Product select p).ToList();
            pad.Color = (from c in db.Color select c).ToList();
            pad.Size = (from s in db.Size select s).ToList();

            return View(pad);
        }*/
        public ActionResult Index()
        {
            List<Product> prod = db.Product.ToList();
            List<Color> color = db.Color.ToList();
            List<Size> size = db.Size.ToList();
            return View(Tuple.Create(prod, color, size));
        }


        public ActionResult ProductView(int? id)
        {
            Product prod = db.Product.Find(id);
            List<Color> color = (from i in db.Color where i.Product_Id == id select i).ToList();
            List<Size> size = (from i in db.Size where i.Product_Id == id select i).ToList();
            return View(Tuple.Create(prod, color, size));
        }
    }
}