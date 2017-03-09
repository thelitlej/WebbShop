using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbshop.Models;

namespace Webbshop.Controllers
{
    public class ViewUserController : Controller
    {
        private WebbShopEntities1 db = new WebbShopEntities1();

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(ViewUser vu)
        {
            if (!String.IsNullOrEmpty(vu.Email) && !String.IsNullOrEmpty(vu.Password))
            {
                foreach (User i in db.User)
                {
                    if (vu.Email.Trim().Equals(i.Email.Trim())
                        && vu.Password.Trim().Equals(i.Password.Trim()))
                    {
                        Session["ViewUser"] = vu;
                        if (vu.Email == "admin")
                        {
                            return RedirectToAction("Index", "Users");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Shop");
                        }
                    }
                }
            }
            return View();
        }
    }
}