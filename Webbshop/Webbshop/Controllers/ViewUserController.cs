using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
           if (Session["ViewUser"] != null)
            {
                ViewBag.logout = "Du har loggat ut";
                Session["ViewUser"] = null;
               
            }
            
            return View();
        }


        [HttpPost]
        public ActionResult Index(ViewUser vu)
        {
            String pass = vu.Password;
            vu.Password = GenerateSHA256Hash(pass).Substring(14);

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

        public string GenerateSHA256Hash(String password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            System.Security.Cryptography.SHA256Managed sha256hashstring = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hashstring.ComputeHash(bytes);

            StringBuilder hexString = new StringBuilder(hash.Length * 2);
            foreach (byte b in hash)
            {
                hexString.AppendFormat("{0:x2}", b);
            }

            return hexString.ToString();
        }
        public ActionResult LogOut()
        {
            return RedirectToAction("Index");
        }
    }
}