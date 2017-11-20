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
    public class PURCHASEORDERsController : Controller
    {
        static int ID = 0;
        static private List<PURCHASEORDDETAIL> dsChiTiet = new List<PURCHASEORDDETAIL>();
        private dbWebShop db = new dbWebShop();

        // GET: PURCHASEORDERs
        public ActionResult Index()
        {
            var pURCHASEORDERs = db.PURCHASEORDERs.Include(p => p.VENDOR);
            return View(pURCHASEORDERs.ToList());
        }

        // GET: PURCHASEORDERs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PURCHASEORDER pURCHASEORDER = db.PURCHASEORDERs.Find(id);
            if (pURCHASEORDER == null)
            {
                return HttpNotFound();
            }
            return View(pURCHASEORDER);
        }

        // GET: PURCHASEORDERs/Create
        public ActionResult Create()
        {
            ViewBag.VENDORID = new SelectList(db.VENDORs, "VENDORID", "VENDORNAME");
            return View();
        }

        // POST: PURCHASEORDERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ORDERNOP,VENDORID,ORDERDATE,TOTALAMT")] PURCHASEORDER pURCHASEORDER)
        {
            if (ModelState.IsValid)
            {
                db.PURCHASEORDERs.Add(pURCHASEORDER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VENDORID = new SelectList(db.VENDORs, "VENDORID", "VENDORNAME", pURCHASEORDER.VENDORID);
            return View(pURCHASEORDER);
        }

        // GET: PURCHASEORDERs/Edit/5
        //public ActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PURCHASEORDER pURCHASEORDER = db.PURCHASEORDERs.Find(id);
        //    if (pURCHASEORDER == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.VENDORID = new SelectList(db.VENDORs, "VENDORID", "VENDORNAME", pURCHASEORDER.VENDORID);
        //    return View(pURCHASEORDER);
        //}

        //// POST: PURCHASEORDERs/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ORDERNOP,VENDORID,ORDERDATE,TOTALAMT")] PURCHASEORDER pURCHASEORDER)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(pURCHASEORDER).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.VENDORID = new SelectList(db.VENDORs, "VENDORID", "VENDORNAME", pURCHASEORDER.VENDORID);
        //    return View(pURCHASEORDER);
        //}

        //// GET: PURCHASEORDERs/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PURCHASEORDER pURCHASEORDER = db.PURCHASEORDERs.Find(id);
        //    if (pURCHASEORDER == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(pURCHASEORDER);
        //}

        //// POST: PURCHASEORDERs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    PURCHASEORDER pURCHASEORDER = db.PURCHASEORDERs.Find(id);
        //    db.PURCHASEORDERs.Remove(pURCHASEORDER);
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
            ViewBag.VENDORID = new SelectList(db.VENDORs, "VENDORID", "VENDORNAME");
            ViewBag.INVTID = new SelectList(db.INVENTORies, "INVTID", "INVTNAME");
            ViewBag.Total = dsChiTiet.Sum(s => s.AMOUNT);
            ViewData["dsChiTiet"] = dsChiTiet;

            return View();
        }
        [Authorize(Roles = "admin,nv")]
        public ActionResult AddDetail([Bind(Include = "INVTID,QTY,STOCKID")] PURCHASEORDDETAIL pURCHASEORDDETAIL)
        {
            if (ModelState.IsValid)
            {

                int id = dsChiTiet.Count();
                id += 1;
                pURCHASEORDDETAIL.ID = id;
                bool isContain = false;
                foreach(PURCHASEORDDETAIL i in dsChiTiet)
                {
                    if (i.INVTID == pURCHASEORDDETAIL.INVTID)
                    {
                        isContain = true;
                        break;
                    }
                }

                if (!isContain)
                {
                    pURCHASEORDDETAIL.INVENTORY = db.INVENTORies.Single(s => s.INVTID.Equals(pURCHASEORDDETAIL.INVTID));
                    pURCHASEORDDETAIL.AMOUNT = pURCHASEORDDETAIL.INVENTORY.SALESPRICE * pURCHASEORDDETAIL.QTY;
                    dsChiTiet.Add(pURCHASEORDDETAIL);
                }
 
            }

            return RedirectToAction("InsertOrder");

        }
        [Authorize(Roles = "admin,nv")]
        [HttpPost]
        public ActionResult SaveDetails([Bind(Include = "VENDORID")] PURCHASEORDER pURCHASEORDER)
        {
            if (ModelState.IsValid)
            {
                var Orders = db.PURCHASEORDERs.ToList();
                int Id = Orders.Count == 0 ? 0 : Int32.Parse(Orders.Max(s => s.ORDERNOP).Substring(5)) + 1;
                string ma = "ORDER" + Id.ToString("0000");
                pURCHASEORDER.ORDERNOP = ma;
                pURCHASEORDER.TOTALAMT = dsChiTiet.Sum(s => s.AMOUNT);
                pURCHASEORDER.ORDERDATE = DateTime.Now;
                db.PURCHASEORDERs.Add(pURCHASEORDER);
                db.SaveChanges();
                foreach (var item in dsChiTiet)
                {

                    PURCHASEORDDETAIL tam = new PURCHASEORDDETAIL();
                    tam.INVTID = item.INVTID;
                    tam.ORDERNOP = ma;
                    tam.PURCHASEPRICE = item.INVENTORY.SALESPRICE;
                    tam.QTY = item.QTY;
                    tam.AMOUNT = tam.QTY * tam.PURCHASEPRICE;
                    tam.ID = ID;
                    ID++;
                    tam.PURCHASEORDER = db.PURCHASEORDERs.Single(s => s.ORDERNOP == ma);
                    db.PURCHASEORDDETAILs.Add(tam);
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
