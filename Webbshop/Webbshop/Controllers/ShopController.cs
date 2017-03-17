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

        public ActionResult Index()
        {
            List<Product> prod = db.Product.ToList();
            List<Color> color = db.Color.ToList();
            List<Size> size = db.Size.ToList();

            return View(Tuple.Create(prod, color, size));
        }
    }
}