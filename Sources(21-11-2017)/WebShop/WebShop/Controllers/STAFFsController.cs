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
    
    public class STAFFsController : Controller
    {
        private dbWebShop db = new dbWebShop();
        [Authorize(Roles = "admin,nv")]
        // GET: STAFFs
        public ActionResult Index(int? page)
        {
            string filterText = Session["TempSearch1"] as string;
            if (filterText != null)
            {
                var nvs = db.STAFFs.Where(b => b.STATUS != "DE").Include(s => s.USER).ToList();
                nvs = db.STAFFs.Where(b => b.STATUS != "DE" && b.NAME.Contains(filterText)).Include(s => s.USER).ToList();
                Pager p = new Pager(nvs.Count, page);
                var goiThongTins = new IndexViewModel
                {
                    dsNV = nvs.Skip((p.CurrentPage - 1) * p.PageSize).Take(p.PageSize),
                    Pager = p
                };
                return View(goiThongTins);
            }
            var nv = db.STAFFs.Where(b => b.STATUS != "DE").Include(s => s.USER).ToList();
            Pager a = new Pager(nv.Count, page);
            var goiThongTin = new IndexViewModel
            {
                dsNV = nv.Skip((a.CurrentPage - 1) * a.PageSize).Take(a.PageSize),
                Pager = a
            };
            return View(goiThongTin);
        }

        [HttpPost]
        public ActionResult Index(int? page, FormCollection formCollection)
        {
            Session["TempSearch1"] = formCollection.Get("txtFilter");
            var nv = db.STAFFs.Where(b => b.STATUS != "DE").ToList();
            var filterText = formCollection.Get("txtFilter");
            nv = db.STAFFs.Where(b => b.STATUS != "DE" && b.NAME.Contains(filterText)).ToList();
            Pager a = new Pager(nv.Count, page);
            if (page > a.TotalPages)
                a = new Pager(nv.Count, 1);
            var goiThongTin = new IndexViewModel
            {
                dsNV = nv.Skip((a.CurrentPage - 1) * a.PageSize).Take(a.PageSize),
                Pager = a
            };
            return View(goiThongTin);
        }
        [Authorize(Roles = "admin,nv")]
        // GET: STAFFs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STAFF sTAFF = db.STAFFs.Find(id);
            if (sTAFF == null)
            {
                return HttpNotFound();
            }
            return View(sTAFF);
        }
        [Authorize(Roles = "admin,nv")]
        // GET: STAFFs/Create
        public ActionResult Create()
        {
            ViewBag.USERID = new SelectList(db.USERs, "USERID", "ROLES");
            return View();
        }

        // POST: STAFFs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "STAFFID,NAME,ADDRESS,PHONE,FAX,EMAIL,STATUS,DESCRIPTION,USERID")] STAFF sTAFF)
        {
            if (ModelState.IsValid)
            {
                var ds = db.STAFFs.OrderByDescending(a => a.STAFFID).ToList();
                if (ds.Count != 0)
                {
                    string maLN = ds[0].STAFFID.ToString();
                    int ma = Int32.Parse(maLN.Substring(5)) + 1;
                    sTAFF.STAFFID = "STAFF" + ma.ToString("0000");
                }
                else
                    sTAFF.STAFFID = "STAFF0001";
                if (sTAFF.STATUS == null)
                    sTAFF.STATUS = "AV";
                db.STAFFs.Add(sTAFF);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.USERID = new SelectList(db.USERs, "USERID", "ROLES", sTAFF.USERID);
            return View(sTAFF);
        }
        [Authorize(Roles = "admin")]
        // GET: STAFFs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STAFF sTAFF = db.STAFFs.Find(id);
            if (sTAFF == null)
            {
                return HttpNotFound();
            }
            ViewBag.USERID = new SelectList(db.USERs, "USERID", "ROLES", sTAFF.USERID);
            return View(sTAFF);
        }

        // POST: STAFFs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "STAFFID,NAME,ADDRESS,PHONE,FAX,EMAIL,STATUS,DESCRIPTION,USERID")] STAFF sTAFF)
        {
            if (ModelState.IsValid)
            {
                if (sTAFF.STATUS == null)
                    sTAFF.STATUS = "AV";
                db.Entry(sTAFF).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.USERID = new SelectList(db.USERs, "USERID", "ROLES", sTAFF.USERID);
            return View(sTAFF);
        }
        [Authorize(Roles = "admin")]
        // GET: STAFFs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STAFF sTAFF = db.STAFFs.Find(id);
            if (sTAFF == null)
            {
                return HttpNotFound();
            }
            return View(sTAFF);
        }

        // POST: STAFFs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            STAFF sTAFF = db.STAFFs.Find(id);
            sTAFF.STATUS = "DE";
            db.Entry(sTAFF).State = EntityState.Modified;
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
