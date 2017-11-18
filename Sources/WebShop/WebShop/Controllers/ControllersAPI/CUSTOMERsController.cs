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
    public class CUSTOMERsController : ApiController
    {
        private dbWebShop db = new dbWebShop();

        // GET: api/CUSTOMERs
        public IEnumerable<CUSTOMER> GetCUSTOMERs()
        {
            IList<CUSTOMER> lst = new List<CUSTOMER>();
            var test = db.CUSTOMERs.Where(i => i.STATUS != "DE").ToList();
            foreach (var item in test)
            {
                lst.Add(new CUSTOMER
                {
                    CUSTID = item.CUSTID,

                    CUSTNAME = item.CUSTNAME,
                    ADDRESS = item.ADDRESS,
                    PHONE = item.PHONE,
                    FAX = item.FAX,
                    EMAIL = item.EMAIL,
                    STATUS = item.STATUS,
                    PASS = item.PASS,
                    DESCRIPTION  = item.DESCRIPTION,
                    SALESORDERs = null
                });


            }
            return lst;
        }

        // GET: api/CUSTOMERs/5
        [ResponseType(typeof(CUSTOMER))]
        public IHttpActionResult GetCUSTOMER(string id)
        {
            CUSTOMER cUSTOMER = db.CUSTOMERs.Find(id);
            if (cUSTOMER == null)
            {
                return NotFound();
            }
            IList<CUSTOMER> lst = new List<CUSTOMER>();
                lst.Add(new CUSTOMER
                {
                    CUSTID = cUSTOMER.CUSTID,

                    CUSTNAME = cUSTOMER.CUSTNAME,
                    ADDRESS = cUSTOMER.ADDRESS,
                    PHONE = cUSTOMER.PHONE,
                    FAX = cUSTOMER.FAX,
                    EMAIL = cUSTOMER.EMAIL,
                    STATUS = cUSTOMER.STATUS,
                    PASS = cUSTOMER.PASS,
                    DESCRIPTION = cUSTOMER.DESCRIPTION,
                    SALESORDERs = null
                });
            return Ok(lst);
        }

        // PUT: api/CUSTOMERs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCUSTOMER(string id, CUSTOMER cUSTOMER)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cUSTOMER.CUSTID)
            {
                return BadRequest();
            }

            db.Entry(cUSTOMER).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CUSTOMERExists(id))
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

        // POST: api/CUSTOMERs
        [ResponseType(typeof(CUSTOMER))]
        public IHttpActionResult PostCUSTOMER(CUSTOMER cUSTOMER)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CUSTOMERs.Add(cUSTOMER);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CUSTOMERExists(cUSTOMER.CUSTID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cUSTOMER.CUSTID }, cUSTOMER);
        }

        // DELETE: api/CUSTOMERs/5
        [ResponseType(typeof(CUSTOMER))]
        public IHttpActionResult DeleteCUSTOMER(string id)
        {
            CUSTOMER cUSTOMER = db.CUSTOMERs.Find(id);
            if (cUSTOMER == null)
            {
                return NotFound();
            }

            db.CUSTOMERs.Remove(cUSTOMER);
            db.SaveChanges();

            return Ok(cUSTOMER);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CUSTOMERExists(string id)
        {
            return db.CUSTOMERs.Count(e => e.CUSTID == id) > 0;
        }
    }
}