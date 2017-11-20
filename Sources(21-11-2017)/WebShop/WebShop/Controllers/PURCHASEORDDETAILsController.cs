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
    public class PURCHASEORDDETAILsController : Controller
    {
        private dbWebShop db = new dbWebShop();

        // GET: PURCHASEORDDETAILs
        public ActionResult Index()
        {
            var pURCHASEORDDETAILs = db.PURCHASEORDDETAILs.Include(p => p.INVENTORY).Include(p => p.PURCHASEORDER);
            return View(pURCHASEORDDETAILs.ToList());
        }

        [HttpGet]
        public ActionResult Index(string id)
        {
            var pURCHASEORDDETAIL = db.PURCHASEORDDETAILs.Include(s => s.INVENTORY).Include(s => s.PURCHASEORDER).Where(s => s.ORDERNOP == id);
            return View(pURCHASEORDDETAIL.ToList());
        }

        // GET: PURCHASEORDDETAILs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PURCHASEORDDETAIL pURCHASEORDDETAIL = db.PURCHASEORDDETAILs.Find(id);
            if (pURCHASEORDDETAIL == null)
            {
                return HttpNotFound();
            }
            return View(pURCHASEORDDETAIL);
        }

        // GET: PURCHASEORDDETAILs/Create
        public ActionResult Create(string OrderNo)
        {
            ViewBag.INVTID = new SelectList(db.INVENTORies, "INVTID", "INVTNAME");
            ViewBag.ORDERNOP = new SelectList(db.PURCHASEORDERs, "ORDERNOP", "VENDORID");
            return View();
        }

        // POST: PURCHASEORDDETAILs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ORDERNOP,INVTID,ID,QTY,PURCHASEPRICE,AMOUNT")] PURCHASEORDDETAIL pURCHASEORDDETAIL)
        {
            if (ModelState.IsValid)
            {
                db.PURCHASEORDDETAILs.Add(pURCHASEORDDETAIL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.INVTID = new SelectList(db.INVENTORies, "INVTID", "INVTNAME", pURCHASEORDDETAIL.INVTID);
            ViewBag.ORDERNOP = new SelectList(db.PURCHASEORDERs, "ORDERNOP", "VENDORID", pURCHASEORDDETAIL.ORDERNOP);
            return View(pURCHASEORDDETAIL);
        }

        //// GET: PURCHASEORDDETAILs/Edit/5
        //public ActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PURCHASEORDDETAIL pURCHASEORDDETAIL = db.PURCHASEORDDETAILs.Find(id);
        //    if (pURCHASEORDDETAIL == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.INVTID = new SelectList(db.INVENTORies, "INVTID", "INVTNAME", pURCHASEORDDETAIL.INVTID);
        //    ViewBag.ORDERNOP = new SelectList(db.PURCHASEORDERs, "ORDERNOP", "VENDORID", pURCHASEORDDETAIL.ORDERNOP);
        //    return View(pURCHASEORDDETAIL);
        //}

        //// POST: PURCHASEORDDETAILs/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ORDERNOP,INVTID,ID,QTY,PURCHASEPRICE,AMOUNT")] PURCHASEORDDETAIL pURCHASEORDDETAIL)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(pURCHASEORDDETAIL).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.INVTID = new SelectList(db.INVENTORies, "INVTID", "INVTNAME", pURCHASEORDDETAIL.INVTID);
        //    ViewBag.ORDERNOP = new SelectList(db.PURCHASEORDERs, "ORDERNOP", "VENDORID", pURCHASEORDDETAIL.ORDERNOP);
        //    return View(pURCHASEORDDETAIL);
        //}

        //// GET: PURCHASEORDDETAILs/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PURCHASEORDDETAIL pURCHASEORDDETAIL = db.PURCHASEORDDETAILs.SingleOrDefault(a => a.ID == id);
        //    if (pURCHASEORDDETAIL == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(pURCHASEORDDETAIL);
        //}

        //// POST: PURCHASEORDDETAILs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    PURCHASEORDDETAIL pURCHASEORDDETAIL = db.PURCHASEORDDETAILs.SingleOrDefault(a => a.ID == id);
        //    PURCHASEORDER p = db.PURCHASEORDERs.SingleOrDefault(a => a.ORDERNOP.Equals(pURCHASEORDDETAIL.ORDERNOP));
        //    p.TOTALAMT = p.TOTALAMT - pURCHASEORDDETAIL.AMOUNT;
        //    db.Entry(p).State = EntityState.Modified;
        //    db.PURCHASEORDDETAILs.Remove(pURCHASEORDDETAIL);
        //    db.SaveChanges();
        //    return RedirectToAction("Index", "PURCHASEORDERs");
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
