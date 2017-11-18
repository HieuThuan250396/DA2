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
    public class SALESORDERsController : ApiController
    {
        private dbWebShop db = new dbWebShop();

        // GET: api/SALESORDERs
        public IEnumerable<SALESORDER> GetSALESORDERs()
        {
            IList<SALESORDER> lst = new List<SALESORDER>();
            var test = db.SALESORDERs.ToList();
            foreach (var item in test)
            {
                lst.Add(new SALESORDER
                {
                    ORDERNO = item.ORDERNO,

                    CUSTID = item.CUSTID,
                    ORDERDATE = item.ORDERDATE,
                    TOTALAMT = item.TOTALAMT,
                    FLAG = item.FLAG,
                    DESCRIPTION = item.DESCRIPTION,
                    CUSTOMER = (new CUSTOMER
                    {
                        CUSTID = item.CUSTID,
                        CUSTNAME = db.CUSTOMERs.Single(s => s.CUSTID.Equals(item.CUSTID)).CUSTNAME,
                        
                    }),
                    //SLSORDERDETAILs = new (SLSORDERDETAIL
                    //{

                    //}),


                });


            }
            return lst;
        }

        // GET: api/SALESORDERs/5
        [ResponseType(typeof(SALESORDER))]
        public IHttpActionResult GetSALESORDER(string id)
        {
            SALESORDER sALESORDER = db.SALESORDERs.Find(id);
            if (sALESORDER == null)
            {
                return NotFound();
            }

            return Ok(sALESORDER);
        }

        // PUT: api/SALESORDERs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSALESORDER(string id, SALESORDER sALESORDER)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sALESORDER.ORDERNO)
            {
                return BadRequest();
            }

            db.Entry(sALESORDER).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SALESORDERExists(id))
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

        // POST: api/SALESORDERs
        [ResponseType(typeof(SALESORDER))]
        public IHttpActionResult PostSALESORDER(SALESORDER sALESORDER)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SALESORDERs.Add(sALESORDER);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SALESORDERExists(sALESORDER.ORDERNO))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = sALESORDER.ORDERNO }, sALESORDER);
        }

        // DELETE: api/SALESORDERs/5
        [ResponseType(typeof(SALESORDER))]
        public IHttpActionResult DeleteSALESORDER(string id)
        {
            SALESORDER sALESORDER = db.SALESORDERs.Find(id);
            if (sALESORDER == null)
            {
                return NotFound();
            }

            db.SALESORDERs.Remove(sALESORDER);
            db.SaveChanges();

            return Ok(sALESORDER);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SALESORDERExists(string id)
        {
            return db.SALESORDERs.Count(e => e.ORDERNO == id) > 0;
        }
    }
}