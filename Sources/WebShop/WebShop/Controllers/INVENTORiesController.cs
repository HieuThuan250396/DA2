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
using System.IO;

namespace WebShop.Controllers
{
    [Authorize(Roles = "admin,nv")]
    public class INVENTORiesController : Controller
    {
        private dbWebShop db = new dbWebShop();

        // GET: INVENTORies
        public ActionResult Index(int? page)
        {
            string filterText = Session["TempSearch1"] as string;
            if (filterText != null)
            {
                var sps = db.INVENTORies.Where(b => b.STATUS != "DE").Include(i => i.CATEGORY).ToList();
                sps = db.INVENTORies.Where(b => b.STATUS != "DE" && b.INVTNAME.Contains(filterText)).ToList();
                Pager p = new Pager(sps.Count, page);
                var goiThongTins = new IndexViewModel
                {
                    dsSP = sps.Skip((p.CurrentPage - 1) * p.PageSize).Take(p.PageSize),
                    Pager = p

                };
                return View(goiThongTins);
            }
            var sp = db.INVENTORies.Where(b => b.STATUS != "DE").Include(i => i.CATEGORY).ToList();
            Pager a = new Pager(sp.Count, page);
            var goiThongTin = new IndexViewModel
            {
                dsSP = sp.Skip((a.CurrentPage - 1) * a.PageSize).Take(a.PageSize),
                Pager = a

            };

            return View(goiThongTin);
        }

        [HttpPost]
        public ActionResult Index(int? page, FormCollection formCollection)
        {

            Session["TempSearch1"] = formCollection.Get("txtFilter");
            var sp = db.INVENTORies.Where(b => b.STATUS != "DE").Include(i => i.CATEGORY).ToList();
            var filterText = formCollection.Get("txtFilter");
            sp = db.INVENTORies.Where(b => b.STATUS != "DE" && b.INVTNAME.Contains(filterText)).ToList();
            Pager a = new Pager(sp.Count(), page);
            if (page > a.TotalPages)
                a = new Pager(sp.Count, 1);
            var goiThongTin = new IndexViewModel
            {
                dsSP = sp.Skip((a.CurrentPage - 1) * a.PageSize).Take(a.PageSize),
                Pager = a

            };
            return View(goiThongTin);

        }
        // GET: INVENTORies/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INVENTORY iNVENTORY = db.INVENTORies.Find(id);
            if (iNVENTORY == null)
            {
                return HttpNotFound();
            }
            return View(iNVENTORY);
        }

        // GET: INVENTORies/Create
        public ActionResult Create()
        {
            ViewBag.IDCATEGORY = new SelectList(db.CATEGORies, "ID", "NAME");
            return View();
        }

        // POST: INVENTORies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IMAGE,INVTID,INVTNAME,IDCATEGORY,SALESPRICE,STATUS,QTY,DESCRIPTION")] INVENTORY iNVENTORY, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                var filename = Path.GetFileName(fileUpload.FileName);
                // luu duong dan 
                var path = Path.Combine(Server.MapPath("~/Img"), filename);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    //load anh
                    fileUpload.SaveAs(path);
                }

                iNVENTORY.IMAGE = filename;

                var ds = db.INVENTORies.OrderByDescending(a => a.INVTID).ToList();
                if (ds.Count != 0)
                {
                    string maLN = ds[0].INVTID.ToString();
                    int ma = Int32.Parse(maLN.Substring(2)) + 1; 
                    iNVENTORY.INVTID = "SP" + ma.ToString("00000");
                }
                else
                    iNVENTORY.INVTID = "SP00001";
                if (iNVENTORY.STATUS == null)
                    iNVENTORY.STATUS = "AV";
                db.INVENTORies.Add(iNVENTORY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCATEGORY = new SelectList(db.CATEGORies, "ID", "NAME", iNVENTORY.IDCATEGORY);
            return View(iNVENTORY);
        }

        // GET: INVENTORies/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INVENTORY iNVENTORY = db.INVENTORies.Find(id);
            if (iNVENTORY == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCATEGORY = new SelectList(db.CATEGORies, "ID", "NAME", iNVENTORY.IDCATEGORY);
            return View(iNVENTORY);
        }

        // POST: INVENTORies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "INVTID,INVTNAME,IDCATEGORY,SALESPRICE,STATUS,DESCRIPTION,IMAGE,QTY")] INVENTORY iNVENTORY, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                var filename = Path.GetFileName(fileUpload.FileName);
                // luu duong dan 
                var path = Path.Combine(Server.MapPath("~/Img"), filename);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    //load anh
                    fileUpload.SaveAs(path);
                }

                iNVENTORY.IMAGE = filename;

                if (iNVENTORY.STATUS == null)
                    iNVENTORY.STATUS = "AV";
                db.Entry(iNVENTORY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCATEGORY = new SelectList(db.CATEGORies, "ID", "NAME", iNVENTORY.IDCATEGORY);
            return View(iNVENTORY);
        }

        // GET: INVENTORies/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INVENTORY iNVENTORY = db.INVENTORies.Find(id);
            if (iNVENTORY == null)
            {
                return HttpNotFound();
            }
            return View(iNVENTORY);
        }

        // POST: INVENTORies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            INVENTORY iNVENTORY = db.INVENTORies.Find(id);
            iNVENTORY.STATUS = "DE";
            db.Entry(iNVENTORY).State = EntityState.Modified;
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
