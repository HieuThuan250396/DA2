using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebShop.Models.Db;

namespace WebShop.Controllers
{
    public class VENDORsController : Controller
    {
        private dbWebShop db = new dbWebShop();

        // GET: VENDORs
        public ActionResult Index()
        {
            return View(db.VENDORs.ToList());
        }

        // GET: VENDORs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VENDOR vENDOR = db.VENDORs.Find(id);
            if (vENDOR == null)
            {
                return HttpNotFound();
            }
            return View(vENDOR);
        }

        // GET: VENDORs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VENDORs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VENDORID,VENDORNAME,ADDRESS,EMAIL,PHONE,FAX,STATUS,DESCRIPTION")] VENDOR vENDOR)
        {
            if (ModelState.IsValid)
            {
                db.VENDORs.Add(vENDOR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vENDOR);
        }

        // GET: VENDORs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VENDOR vENDOR = db.VENDORs.Find(id);
            if (vENDOR == null)
            {
                return HttpNotFound();
            }
            return View(vENDOR);
        }

        // POST: VENDORs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VENDORID,VENDORNAME,ADDRESS,EMAIL,PHONE,FAX,STATUS,DESCRIPTION")] VENDOR vENDOR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vENDOR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vENDOR);
        }

        // GET: VENDORs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VENDOR vENDOR = db.VENDORs.Find(id);
            if (vENDOR == null)
            {
                return HttpNotFound();
            }
            return View(vENDOR);
        }

        // POST: VENDORs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            VENDOR vENDOR = db.VENDORs.Find(id);
            db.VENDORs.Remove(vENDOR);
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
