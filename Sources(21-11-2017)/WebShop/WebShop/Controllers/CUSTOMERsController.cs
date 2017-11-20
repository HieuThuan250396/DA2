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
    public class CUSTOMERsController : Controller
    {
        private dbWebShop db = new dbWebShop();

        // GET: CUSTOMERs
        public ActionResult Index(int? page)
        {
            string filterText = Session["TempSearch1"] as string;
            if (filterText != null)
            {
                var khs = db.CUSTOMERs.Where(b => b.STATUS != "DE").ToList();
                khs = db.CUSTOMERs.Where(b => b.STATUS != "DE" && b.CUSTNAME.Contains(filterText)).ToList();
                Pager p = new Pager(khs.Count, page);
                var goiThongTins = new IndexViewModel
                {
                    dsKH = khs.Skip((p.CurrentPage - 1) * p.PageSize).Take(p.PageSize),
                    Pager = p
                };
                return View(goiThongTins);
            }
            else
            {
                var kh = db.CUSTOMERs.Where(b => b.STATUS != "DE").ToList();
                Pager a = new Pager(kh.Count, page);
                var goiThongTin = new IndexViewModel
                {
                    dsKH = kh.Skip((a.CurrentPage - 1) * a.PageSize).Take(a.PageSize),
                    Pager = a
                };
                return View(goiThongTin);
            }
        }

        [HttpPost]
        public ActionResult Index(int? page, FormCollection formCollection)
        {
            Session["TempSearch1"] = formCollection.Get("txtFilter");
            var kh = db.CUSTOMERs.Where(b => b.STATUS != "DE").ToList();
            var filterText = formCollection.Get("txtFilter");
            kh = db.CUSTOMERs.Where(b => b.STATUS != "DE" && b.CUSTNAME.Contains(filterText)).ToList();
            Pager a = new Pager(kh.Count, page);
            if (page > a.TotalPages)
                a = new Pager(kh.Count, 1);
            var goiThongTin = new IndexViewModel
            {
                dsKH = kh.Skip((a.CurrentPage - 1) * a.PageSize).Take(a.PageSize),
                Pager = a
            };
            return View(goiThongTin);

        }
        // GET: CUSTOMERs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUSTOMER cUSTOMER = db.CUSTOMERs.Find(id);
            if (cUSTOMER == null)
            {
                return HttpNotFound();
            }
            return View(cUSTOMER);
        }

        // GET: CUSTOMERs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CUSTOMERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CUSTID,CUSTNAME,ADDRESS,PHONE,FAX,EMAIL,AMOUNT,STATUS,PASS,DESCRIPTION")] CUSTOMER cUSTOMER)
        {
            if (ModelState.IsValid)
            {
                var ds = db.CUSTOMERs.OrderByDescending(a => a.CUSTID).ToList();
                if (ds.Count != 0)
                {
                    string maLN = ds[0].CUSTID.ToString();
                    int ma = Int32.Parse(maLN.Substring(8)) + 1;
                    cUSTOMER.CUSTID = "CUSTOMER" + ma.ToString("00000");
                }
                else
                    cUSTOMER.CUSTID = "CUSTOMER00001";
                if (cUSTOMER.STATUS == null)
                    cUSTOMER.STATUS = "AV";
                db.CUSTOMERs.Add(cUSTOMER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cUSTOMER);
        }

        // GET: CUSTOMERs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUSTOMER cUSTOMER = db.CUSTOMERs.Find(id);
            if (cUSTOMER == null)
            {
                return HttpNotFound();
            }
            return View(cUSTOMER);
        }

        // POST: CUSTOMERs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CUSTID,CUSTNAME,ADDRESS,PHONE,FAX,EMAIL,AMOUNT,STATUS,PASS,DESCRIPTION")] CUSTOMER cUSTOMER)
        {
            if (ModelState.IsValid)
            {
                if (cUSTOMER.STATUS == null)
                    cUSTOMER.STATUS = "AV";
                db.Entry(cUSTOMER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cUSTOMER);
        }

        // GET: CUSTOMERs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUSTOMER cUSTOMER = db.CUSTOMERs.Find(id);
            if (cUSTOMER == null)
            {
                return HttpNotFound();
            }
            return View(cUSTOMER);
        }

        // POST: CUSTOMERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CUSTOMER cUSTOMER = db.CUSTOMERs.Find(id);
            cUSTOMER.STATUS = "DE";
            db.Entry(cUSTOMER).State = EntityState.Modified;
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
        public ActionResult ClearAll()
        {
            Session["TempSearch1"] = null;
            return RedirectToAction("Index");
        }
    }
}
