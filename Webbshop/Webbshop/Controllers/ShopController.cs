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
            ProductAndDetails pad = new ProductAndDetails();
            //Product prod = new Product();

            //prod = (from p in db.Product select p).FirstOrDefault();
            List<Product> prod = db.Product.ToList();
            List<Color> color = db.Color.ToList();
            List<Size> size = db.Size.ToList();

            return View(Tuple.Create(prod, color, size));
        }
    }
}