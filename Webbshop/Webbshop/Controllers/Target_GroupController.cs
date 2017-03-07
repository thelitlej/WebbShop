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
    public class Target_GroupController : Controller
    {
        private WebbShopEntities1 db = new WebbShopEntities1();

        // GET: Target_Group
        public ActionResult Index()
        {
            return View(db.Target_Group.ToList());
        }

        // GET: Target_Group/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Target_Group target_Group = db.Target_Group.Find(id);
            if (target_Group == null)
            {
                return HttpNotFound();
            }
            return View(target_Group);
        }

        // GET: Target_Group/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Target_Group/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Target_Group1,Product_Id")] Target_Group target_Group)
        {
            if (ModelState.IsValid)
            {
                db.Target_Group.Add(target_Group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(target_Group);
        }

        // GET: Target_Group/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Target_Group target_Group = db.Target_Group.Find(id);
            if (target_Group == null)
            {
                return HttpNotFound();
            }
            return View(target_Group);
        }

        // POST: Target_Group/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Target_Group1,Product_Id")] Target_Group target_Group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(target_Group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(target_Group);
        }

        // GET: Target_Group/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Target_Group target_Group = db.Target_Group.Find(id);
            if (target_Group == null)
            {
                return HttpNotFound();
            }
            return View(target_Group);
        }

        // POST: Target_Group/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Target_Group target_Group = db.Target_Group.Find(id);
            db.Target_Group.Remove(target_Group);
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
