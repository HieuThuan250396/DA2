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
    public class SALESORDERsController : Controller
    {
        static int ID = 0;
        static private List<SLSORDERDETAIL> dsChiTiet = new List<SLSORDERDETAIL>();
        private dbWebShop db = new dbWebShop();
        [Authorize(Roles = "admin,nv")]
        // GET: SALESORDERs
        public ActionResult Index()
        {
            var sALESORDERs = db.SALESORDERs.Include(s => s.CUSTOMER);
            return View(sALESORDERs.ToList());
        }

        // GET: SALESORDERs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SALESORDER sALESORDERs = db.SALESORDERs.Find(id);
            if (sALESORDERs == null)
            {
                return HttpNotFound();
            }
            return View(sALESORDERs);
        }
        [Authorize(Roles = "admin,nv")]
        // GET: SALESORDERs/Create
        public ActionResult Create()
        {
            ViewBag.CUSTID = new SelectList(db.CUSTOMERs, "CUSTID", "CUSTNAME");
            return View();
        }

        // POST: SALESORDERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ORDERNO,CUSTID,ORDERDATE,TOTALAMT,FLAG,DESCRIPTION")] SALESORDER sALESORDER)
        {

            if (ModelState.IsValid)
            {
                db.SALESORDERs.Add(sALESORDER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CUSTID = new SelectList(db.CUSTOMERs, "CUSTID", "CUSTNAME", sALESORDER.CUSTID);
            return View(sALESORDER);
        }
        //[Authorize(Roles = "admin,nv")]
        //// GET: SALESORDERs/Edit/5
        //public ActionResult Edit(string id)
        //{

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SALESORDER sALESORDER = db.SALESORDERs.Find(id);
        //    if (sALESORDER == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CUSTID = new SelectList(db.CUSTOMERs, "CUSTID", "CUSTNAME", sALESORDER.CUSTID);
        //    return View(sALESORDER);
        //}

        //// POST: SALESORDERs/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ORDERNO,CUSTID,ORDERDATE,TOTALAMT,FLAG,DESCRIPTION")] SALESORDER sALESORDER)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(sALESORDER).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CUSTID = new SelectList(db.CUSTOMERs, "CUSTID", "CUSTNAME", sALESORDER.CUSTID);
        //    return View(sALESORDER);
        //}
        //[Authorize(Roles = "admin")]
        //// GET: SALESORDERs/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SALESORDER sALESORDER = db.SALESORDERs.Find(id);
        //    if (sALESORDER == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(sALESORDER);
        //}

        //// POST: SALESORDERs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    SALESORDER sALESORDER = db.SALESORDERs.Find(id);
        //    db.SALESORDERs.Remove(sALESORDER);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        [Authorize(Roles = "admin,nv")]
        public ActionResult InsertOrder()
        {
            ViewBag.CUSTID = new SelectList(db.CUSTOMERs, "CUSTID", "CUSTNAME");
            ViewBag.INVTID = new SelectList(db.INVENTORies, "INVTID", "INVTNAME");
            ViewBag.Total = dsChiTiet.Sum(s => s.AMOUNT);
            ViewData["dsChiTiet"] = dsChiTiet;

            return View();
        }
        [Authorize(Roles = "admin,nv")]
        public ActionResult AddDetail([Bind(Include = "INVTID,QTY,STOCKID")] SLSORDERDETAIL sLSORDERDETAIL)
        {
            if (ModelState.IsValid)
            {

                int id = dsChiTiet.Count();
                id += 1;
                sLSORDERDETAIL.ID = id;
                bool isContain = false;
                foreach (SLSORDERDETAIL i in dsChiTiet)
                {
                    if (i.INVTID == sLSORDERDETAIL.INVTID)
                    {
                        isContain = true;
                        break;
                    }
                }

                if (!isContain)
                {
                    sLSORDERDETAIL.INVENTORY = db.INVENTORies.Single(s => s.INVTID.Equals(sLSORDERDETAIL.INVTID));
                    sLSORDERDETAIL.AMOUNT = sLSORDERDETAIL.INVENTORY.SALESPRICE * sLSORDERDETAIL.QTY;
                    dsChiTiet.Add(sLSORDERDETAIL);
                }

            }

            return RedirectToAction("InsertOrder");

        }
        [Authorize(Roles = "admin,nv")]
        [HttpPost]
        public ActionResult SaveDetails([Bind(Include = "CUSTID")] SALESORDER sALESORDER)
        {
            if (ModelState.IsValid)
            {
                var Orders = db.SALESORDERs.ToList();
                int Id = Orders.Count == 0 ? 0 : Int32.Parse(Orders.Max(s => s.ORDERNO).Substring(5)) + 1;
                string ma = "ORDER" + Id.ToString("0000");
                sALESORDER.ORDERNO = ma;
                sALESORDER.TOTALAMT = dsChiTiet.Sum(s => s.AMOUNT);
                sALESORDER.ORDERDATE = DateTime.Now;
                db.SALESORDERs.Add(sALESORDER);
                db.SaveChanges();
                foreach (var item in dsChiTiet)
                {

                    SLSORDERDETAIL tam = new SLSORDERDETAIL();
                    tam.INVTID = item.INVTID;
                    tam.ORDERNO = ma;
                    tam.QTY = item.QTY;
                    tam.AMOUNT = tam.QTY * item.INVENTORY.SALESPRICE;
                    tam.ID = ID;
                    ID++;
                    tam.SALESORDER = db.SALESORDERs.Single(s => s.ORDERNO == ma);
                    tam.SALESORDER.CUSTID = sALESORDER.CUSTID;
                    db.SLSORDERDETAILs.Add(tam);
                    db.SaveChanges();
                }
                dsChiTiet.Clear();

                return RedirectToAction("Index");
            }

            return RedirectToAction("InsertOrder");
        }
        [Authorize(Roles = "admin,nv")]
        public ActionResult RemoveDetail(int id)
        {
            dsChiTiet.Remove(dsChiTiet.Single(s => s.ID == id));
            return RedirectToAction("InsertOrder");
        }
    }
}
