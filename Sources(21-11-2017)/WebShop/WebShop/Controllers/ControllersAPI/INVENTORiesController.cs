using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebShop.Models.Db;

namespace WebShop.Controllers.ControllersAPI
{
    public class INVENTORiesController : ApiController
    {
        private dbWebShop db = new dbWebShop();


        // GET: api/INVENTORies
        public IEnumerable<INVENTORY> GetINVENTORies()
        {
            IList<INVENTORY> lst = new List<INVENTORY>();
            IList<CATEGORY> lstCAT = new List<CATEGORY>();
            var test = db.INVENTORies.Where(i => i.STATUS != "DE").Include(i => i.CATEGORY).ToList();
            foreach (var item in test)
            {
                lst.Add(new INVENTORY
                {
                    INVTID = item.INVTID,
                    
                    INVTNAME = item.INVTNAME,
                    IDCATEGORY = item.IDCATEGORY,     
                    SALESPRICE = item.SALESPRICE,
                    STATUS = item.STATUS,
                    DESCRIPTION = item.DESCRIPTION,
                    IMAGE = item.IMAGE,
                    PURCHASEORDDETAILs = null,
                    SLSORDERDETAILs = null,
                    CATEGORY = (new CATEGORY
                    {
                        ID = item.IDCATEGORY,
                        NAME = db.CATEGORies.Single(s => s.ID.Equals(item.IDCATEGORY)).NAME,
                        INVENTORies = null
                    }),
                });
                

            }
            return lst;

        }
        // Nam
        public IEnumerable<INVENTORY> GetINVENTORiesNAM()
        {
            IList<INVENTORY> lst = new List<INVENTORY>();
            IList<CATEGORY> lstCAT = new List<CATEGORY>();
            var test = db.INVENTORies.Where(i => i.STATUS != "DE" && i.IDCATEGORY ==  8).Include(i => i.CATEGORY).ToList();
            foreach (var item in test)
            {
                lst.Add(new INVENTORY
                {
                    INVTID = item.INVTID,

                    INVTNAME = item.INVTNAME,
                    IDCATEGORY = item.IDCATEGORY,
                    SALESPRICE = item.SALESPRICE,
                    STATUS = item.STATUS,
                    DESCRIPTION = item.DESCRIPTION,
                    IMAGE = item.IMAGE,
                    PURCHASEORDDETAILs = null,
                    SLSORDERDETAILs = null,
                    CATEGORY = (new CATEGORY
                    {
                        ID = item.IDCATEGORY,
                        NAME = db.CATEGORies.Single(s => s.ID.Equals(item.IDCATEGORY)).NAME,
                        INVENTORies = null
                    }),
                });
            }
            return lst;

        }
        // Nu
        public IEnumerable<INVENTORY> GetINVENTORiesNU()
        {
            IList<INVENTORY> lst = new List<INVENTORY>();
            IList<CATEGORY> lstCAT = new List<CATEGORY>();
            var test = db.INVENTORies.Where(i => i.STATUS != "DE" && i.IDCATEGORY == 9 ).Include(i => i.CATEGORY).ToList();
            foreach (var item in test)
            {
                lst.Add(new INVENTORY
                {
                    INVTID = item.INVTID,

                    INVTNAME = item.INVTNAME,
                    IDCATEGORY = item.IDCATEGORY,
                    SALESPRICE = item.SALESPRICE,
                    STATUS = item.STATUS,
                    DESCRIPTION = item.DESCRIPTION,
                    IMAGE = item.IMAGE,
                    PURCHASEORDDETAILs = null,
                    SLSORDERDETAILs = null,
                    CATEGORY = (new CATEGORY
                    {
                        ID = item.IDCATEGORY,
                        NAME = db.CATEGORies.Single(s => s.ID.Equals(item.IDCATEGORY)).NAME,
                        INVENTORies = null
                    }),
                });


            }
            return lst;

        }
        // Kid
        public IEnumerable<INVENTORY> GetINVENTORiesKID()
        {
            IList<INVENTORY> lst = new List<INVENTORY>();
            IList<CATEGORY> lstCAT = new List<CATEGORY>();
            var test = db.INVENTORies.Where(i => i.STATUS != "DE" && i.IDCATEGORY == 10).Include(i => i.CATEGORY).ToList();
            foreach (var item in test)
            {
                lst.Add(new INVENTORY
                {
                    INVTID = item.INVTID,

                    INVTNAME = item.INVTNAME,
                    IDCATEGORY = item.IDCATEGORY,
                    SALESPRICE = item.SALESPRICE,
                    STATUS = item.STATUS,
                    DESCRIPTION = item.DESCRIPTION,
                    IMAGE = item.IMAGE,
                    PURCHASEORDDETAILs = null,
                    SLSORDERDETAILs = null,
                    CATEGORY = (new CATEGORY
                    {
                        ID = item.IDCATEGORY,
                        NAME = db.CATEGORies.Single(s => s.ID.Equals(item.IDCATEGORY)).NAME,
                        INVENTORies = null
                    }),
                });


            }
            return lst;

        }
        // GET: api/INVENTORies/5
        [ResponseType(typeof(INVENTORY))]
        public IHttpActionResult GetINVENTORY(string id)
        {
            INVENTORY iNVENTORY = db.INVENTORies.Find(id);
            if (iNVENTORY == null)
            {
                return NotFound();
            }
            IList<INVENTORY> lst = new List<INVENTORY>();
            IList<CATEGORY> lstCAT = new List<CATEGORY>();
                lst.Add(new INVENTORY
                {
                    INVTID = iNVENTORY.INVTID,

                    INVTNAME = iNVENTORY.INVTNAME,
                    IDCATEGORY = iNVENTORY.IDCATEGORY,
                    SALESPRICE = iNVENTORY.SALESPRICE,
                    STATUS = iNVENTORY.STATUS,
                    DESCRIPTION = iNVENTORY.DESCRIPTION,
                    IMAGE = iNVENTORY.IMAGE,
                    PURCHASEORDDETAILs = null,
                    SLSORDERDETAILs = null,
                    CATEGORY = (new CATEGORY
                    {
                        ID = iNVENTORY.IDCATEGORY,
                        NAME = db.CATEGORies.Single(s => s.ID.Equals(iNVENTORY.IDCATEGORY)).NAME,
                        INVENTORies = null
                    }),
                });


            return Ok(lst);
        }

        // PUT: api/INVENTORies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutINVENTORY(string id, INVENTORY iNVENTORY)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            iNVENTORY = db.INVENTORies.Find(id);
            if (id != iNVENTORY.INVTID)
            {
                return BadRequest();
            }
            iNVENTORY.STATUS = "NA";
            db.Entry(iNVENTORY).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!INVENTORYExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/INVENTORies
        [ResponseType(typeof(INVENTORY))]
        public IHttpActionResult PostINVENTORY(INVENTORY iNVENTORY)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.INVENTORies.Add(iNVENTORY);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (INVENTORYExists(iNVENTORY.INVTID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = iNVENTORY.INVTID }, iNVENTORY);
        }

        // DELETE: api/INVENTORies/5
        [ResponseType(typeof(INVENTORY))]
        public IHttpActionResult DeleteINVENTORY(string id)
        {
            INVENTORY iNVENTORY = db.INVENTORies.Find(id);
            if (iNVENTORY == null)
            {
                return NotFound();
            }

            db.INVENTORies.Remove(iNVENTORY);
            db.SaveChanges();

            return Ok(iNVENTORY);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool INVENTORYExists(string id)
        {
            return db.INVENTORies.Count(e => e.INVTID == id) > 0;
        }
    }
}