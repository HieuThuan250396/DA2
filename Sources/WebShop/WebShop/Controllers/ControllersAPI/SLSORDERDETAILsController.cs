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
    public class SLSORDERDETAILsController : ApiController
    {
        private dbWebShop db = new dbWebShop();

        // GET: api/SLSORDERDETAILs
        public IQueryable<SLSORDERDETAIL> GetSLSORDERDETAILs()
        {
            return db.SLSORDERDETAILs;
        }

        // GET: api/SLSORDERDETAILs/5
        [ResponseType(typeof(SLSORDERDETAIL))]
        public IHttpActionResult GetSLSORDERDETAIL(string id)
        {
            SLSORDERDETAIL sLSORDERDETAIL = db.SLSORDERDETAILs.Find(id);
            if (sLSORDERDETAIL == null)
            {
                return NotFound();
            }

            return Ok(sLSORDERDETAIL);
        }

        // PUT: api/SLSORDERDETAILs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSLSORDERDETAIL(string id, SLSORDERDETAIL sLSORDERDETAIL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sLSORDERDETAIL.ORDERNO)
            {
                return BadRequest();
            }

            db.Entry(sLSORDERDETAIL).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SLSORDERDETAILExists(id))
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

        // POST: api/SLSORDERDETAILs
        [ResponseType(typeof(SLSORDERDETAIL))]
        public IHttpActionResult PostSLSORDERDETAIL(SLSORDERDETAIL sLSORDERDETAIL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SLSORDERDETAILs.Add(sLSORDERDETAIL);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SLSORDERDETAILExists(sLSORDERDETAIL.ORDERNO))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = sLSORDERDETAIL.ORDERNO }, sLSORDERDETAIL);
        }

        // DELETE: api/SLSORDERDETAILs/5
        [ResponseType(typeof(SLSORDERDETAIL))]
        public IHttpActionResult DeleteSLSORDERDETAIL(string id)
        {
            SLSORDERDETAIL sLSORDERDETAIL = db.SLSORDERDETAILs.Find(id);
            if (sLSORDERDETAIL == null)
            {
                return NotFound();
            }

            db.SLSORDERDETAILs.Remove(sLSORDERDETAIL);
            db.SaveChanges();

            return Ok(sLSORDERDETAIL);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SLSORDERDETAILExists(string id)
        {
            return db.SLSORDERDETAILs.Count(e => e.ORDERNO == id) > 0;
        }
    }
}