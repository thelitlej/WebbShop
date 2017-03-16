using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Webbshop.Models;

namespace Webbshop.Controllers
{
    public class ColorsController : Controller
    {
        private WebbShopEntities1 db = new WebbShopEntities1();

        // GET: Colors
        public ActionResult Index()
        {
            var color = db.Color.Include(c => c.Product);
            return View(color.ToList());
        }

        // GET: Colors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Color color = db.Color.Find(id);
            if (color == null)
            {
                return HttpNotFound();
            }
            return View(color);
        }

        // GET: Colors/Create
        public ActionResult Create(int id)
        {
            var prod = from p in db.Product where p.Id == id select p;
            ViewBag.ProductName = (from n in prod select n.Name).First().ToString();
            ViewBag.ID = id;
            ViewBag.fileError = TempData["fileError"];
            ViewBag.color = TempData["color"];
            ViewBag.Product_Id = new SelectList(prod, "Id", "Name");
            return View();
        }

        // POST: Colors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Product_Id,Color1")] Color color, HttpPostedFileBase file)
        {
            //accepterade filformat
            var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };


            if (file != null)
            {
                //hämtar url
                string url = file.ToString();
                //hämtar filnamn
                var fileName = Path.GetFileName(file.FileName);
                //hämtar filnamnformat
                var ext = Path.GetExtension(file.FileName);
                string img_name = fileName.ToString();
                color.Img_Name = img_name;

                if (ModelState.IsValid && allowedExtensions.Contains(ext))
                {
                    string name = Path.GetFileNameWithoutExtension(fileName);
                    string myfile = name + ext;

                    var path = Path.Combine(Server.MapPath("~/App_Data"), myfile);

                    db.Color.Add(color);
                    db.SaveChanges();
                    file.SaveAs(path);


                    TempData["color"] = "Färgen " + color.Color1.ToLower() + " har lagts till";
                  
                }
                else
                {
                    
                    TempData["fileError"] = "Välj en bild med rätt format (.png .jpg .Jpeg .jpeg)";
                    
                }
                
            }
            else
            {
                TempData["fileError"] = "Välj en bild";
                
            }
            return RedirectToAction("Create", new { id = color.Product_Id });

        }

        // GET: Colors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Color color = db.Color.Find(id);
            if (color == null)
            {
                return HttpNotFound();
            }
            ViewBag.Product_Id = new SelectList(db.Product, "Id", "Name", color.Product_Id);
            return View(color);
        }

        // POST: Colors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Product_Id,Color1,Img_Name")] Color color)
        {
            if (ModelState.IsValid)
            {
                db.Entry(color).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Product_Id = new SelectList(db.Product, "Id", "Name", color.Product_Id);
            return View(color);
        }

        // GET: Colors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Color color = db.Color.Find(id);
            if (color == null)
            {
                return HttpNotFound();
            }
            return View(color);
        }

        // POST: Colors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Color color = db.Color.Find(id);
            db.Color.Remove(color);
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
