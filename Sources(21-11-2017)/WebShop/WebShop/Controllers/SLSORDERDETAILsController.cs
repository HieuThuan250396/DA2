using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebShop.Models.Db;
using WebShop.Models;

namespace WebShop.Controllers
{
    [Authorize(Roles = "admin,nv")]
    public class SLSORDERDETAILsController : Controller
    {
        private dbWebShop db = new dbWebShop();

        // GET: SLSORDERDETAILs
        public ActionResult Index()
        {
            var sLSORDERDETAILs = db.SLSORDERDETAILs.Include(s => s.INVENTORY).Include(s => s.SALESORDER);
            return View(sLSORDERDETAILs.ToList());
        }

        [HttpGet]
        public ActionResult Index(string id)
        {
            var sLSORDERDETAILs = db.SLSORDERDETAILs.Include(s => s.INVENTORY).Include(s => s.SALESORDER).Where(s => s.ORDERNO == id);
            return View(sLSORDERDETAILs.ToList());
        }

        // GET: SLSORDERDETAILs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SLSORDERDETAIL sLSORDERDETAIL = db.SLSORDERDETAILs.Find(id);
            if (sLSORDERDETAIL == null)
            {
                return HttpNotFound();
            }
            return View(sLSORDERDETAIL);
        }

        // GET: SLSORDERDETAILs/Create
        public ActionResult Create(string OrderNo)
        {
            ViewData["OrderNo"] = OrderNo;
            ViewBag.INVTID = new SelectList(db.INVENTORies, "INVTID", "INVTNAME");
            ViewBag.ORDERNO = new SelectList(db.SALESORDERs, "ORDERNO", "CUSTID");
            return View();
        }

        // POST: SLSORDERDETAILs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ORDERNO,INVTID,ID,QTY,AMOUNT")] SLSORDERDETAIL sLSORDERDETAIL)
        {

            if (ModelState.IsValid)
            {
                db.SLSORDERDETAILs.Add(sLSORDERDETAIL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.INVTID = new SelectList(db.INVENTORies, "INVTID", "INVTNAME", sLSORDERDETAIL.INVTID);
            ViewBag.ORDERNO = new SelectList(db.SALESORDERs, "ORDERNO", "CUSTID", sLSORDERDETAIL.ORDERNO);
            return View(sLSORDERDETAIL);
        }

        //// GET: SLSORDERDETAILs/Edit/5
        //public ActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SLSORDERDETAIL sLSORDERDETAIL = db.SLSORDERDETAILs.Find(id);
        //    if (sLSORDERDETAIL == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.INVTID = new SelectList(db.INVENTORies, "INVTID", "INVTNAME", sLSORDERDETAIL.INVTID);
        //    ViewBag.ORDERNO = new SelectList(db.SALESORDERs, "ORDERNO", "CUSTID", sLSORDERDETAIL.ORDERNO);
        //    return View(sLSORDERDETAIL);
        //}

        //// POST: SLSORDERDETAILs/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ORDERNO,INVTID,ID,QTY,AMOUNT")] SLSORDERDETAIL sLSORDERDETAIL)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(sLSORDERDETAIL).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.INVTID = new SelectList(db.INVENTORies, "INVTID", "INVTNAME", sLSORDERDETAIL.INVTID);
        //    ViewBag.ORDERNO = new SelectList(db.SALESORDERs, "ORDERNO", "CUSTID", sLSORDERDETAIL.ORDERNO);
        //    return View(sLSORDERDETAIL);
        //}

        //// GET: SLSORDERDETAILs/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SLSORDERDETAIL sLSORDERDETAIL = db.SLSORDERDETAILs.Find(id);
        //    if (sLSORDERDETAIL == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(sLSORDERDETAIL);
        //}

        //// POST: SLSORDERDETAILs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    SLSORDERDETAIL sLSORDERDETAIL = db.SLSORDERDETAILs.SingleOrDefault(a => a.ID == id);
        //    SALESORDER p = db.SALESORDERs.SingleOrDefault(a => a.ORDERNO.Equals(sLSORDERDETAIL.ORDERNO));
        //    p.TOTALAMT = p.TOTALAMT - sLSORDERDETAIL.AMOUNT;
        //    db.Entry(p).State = EntityState.Modified;
        //    db.SLSORDERDETAILs.Remove(sLSORDERDETAIL);
        //    db.SaveChanges();
        //    return RedirectToAction("Index", "SALESORDERs");
        //}

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
