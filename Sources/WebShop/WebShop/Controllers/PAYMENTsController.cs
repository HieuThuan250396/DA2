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
    public class PAYMENTsController : Controller
    {
        private dbWebShop db = new dbWebShop();

        // GET: PAYMENTs
        public ActionResult Index()
        {
            var pAYMENTs = db.PAYMENTs.Include(p => p.SALESORDER).Include(p => p.STAFF);
            return View(pAYMENTs.ToList());
        }

        // GET: PAYMENTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAYMENT pAYMENT = db.PAYMENTs.Find(id);
            if (pAYMENT == null)
            {
                return HttpNotFound();
            }
            return View(pAYMENT);
        }

        // GET: PAYMENTs/Create
        public ActionResult Create()
        {
            ViewBag.ORDERNO = new SelectList(db.SALESORDERs, "ORDERNO", "CUSTID");
            ViewBag.SALESPERSONSID = new SelectList(db.STAFFs, "STAFFID", "NAME");
            return View();
        }

        // POST: PAYMENTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PAYMENTID,ORDERNO,SALESPERSONSID,PAYMENTNO,PAYMENTDATE,PAYMENTAMT,DESCRIPTION")] PAYMENT pAYMENT)
        {
            if (ModelState.IsValid)
            {
                db.PAYMENTs.Add(pAYMENT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ORDERNO = new SelectList(db.SALESORDERs, "ORDERNO", "CUSTID", pAYMENT.ORDERNO);
            ViewBag.SALESPERSONSID = new SelectList(db.STAFFs, "STAFFID", "NAME", pAYMENT.SALESPERSONSID);
            return View(pAYMENT);
        }

        // GET: PAYMENTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAYMENT pAYMENT = db.PAYMENTs.Find(id);
            if (pAYMENT == null)
            {
                return HttpNotFound();
            }
            ViewBag.ORDERNO = new SelectList(db.SALESORDERs, "ORDERNO", "CUSTID", pAYMENT.ORDERNO);
            ViewBag.SALESPERSONSID = new SelectList(db.STAFFs, "STAFFID", "NAME", pAYMENT.SALESPERSONSID);
            return View(pAYMENT);
        }

        // POST: PAYMENTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PAYMENTID,ORDERNO,SALESPERSONSID,PAYMENTNO,PAYMENTDATE,PAYMENTAMT,DESCRIPTION")] PAYMENT pAYMENT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pAYMENT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ORDERNO = new SelectList(db.SALESORDERs, "ORDERNO", "CUSTID", pAYMENT.ORDERNO);
            ViewBag.SALESPERSONSID = new SelectList(db.STAFFs, "STAFFID", "NAME", pAYMENT.SALESPERSONSID);
            return View(pAYMENT);
        }

        // GET: PAYMENTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAYMENT pAYMENT = db.PAYMENTs.Find(id);
            if (pAYMENT == null)
            {
                return HttpNotFound();
            }
            return View(pAYMENT);
        }

        // POST: PAYMENTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PAYMENT pAYMENT = db.PAYMENTs.Find(id);
            db.PAYMENTs.Remove(pAYMENT);
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
