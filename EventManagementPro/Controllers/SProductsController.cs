using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using EventManagementPro.API.Models;
using EventManagementPro.Models;

namespace EventManagementPro.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using EventManagementPro.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<SProduct>("SProducts");
    builder.EntitySet<StockIn>("StockIns"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class SProductsController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/SProducts
        [EnableQuery]
        public IQueryable<SProduct> GetSProducts()
        {
            return db.SProducts;
        }

        // GET: odata/SProducts(5)
        [EnableQuery]
        public SingleResult<SProduct> GetSProduct([FromODataUri] int key)
        {
            return SingleResult.Create(db.SProducts.Where(sProduct => sProduct.Id == key));
        }

        // PUT: odata/SProducts(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<SProduct> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SProduct sProduct = db.SProducts.Find(key);
            if (sProduct == null)
            {
                return NotFound();
            }

            patch.Put(sProduct);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(sProduct);
        }

        // POST: odata/SProducts
        public IHttpActionResult Post(SProduct sProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SProducts.Add(sProduct);
            db.SaveChanges();

            return Created(sProduct);
        }

        // PATCH: odata/SProducts(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<SProduct> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SProduct sProduct = db.SProducts.Find(key);
            if (sProduct == null)
            {
                return NotFound();
            }

            patch.Patch(sProduct);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(sProduct);
        }

        // DELETE: odata/SProducts(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            SProduct sProduct = db.SProducts.Find(key);
            if (sProduct == null)
            {
                return NotFound();
            }

            db.SProducts.Remove(sProduct);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/SProducts(5)/StockIns
        [EnableQuery]
        public IQueryable<StockIn> GetStockIns([FromODataUri] int key)
        {
            return db.SProducts.Where(m => m.Id == key).SelectMany(m => m.StockIns);
        }
        [HttpPost]
        public IQueryable<SProductVM> AcSProducts()
        {
            return db.SProducts.Select(p => new SProductVM
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                Price = p.Price,
                Description = p.Description,
                Picture = p.Picture,
                Stocklevel = p.Stocklevel,
                BriefDescription = p.Description.Length > 20 ? p.Description.Substring(0, 20) + "..." : p.Description
            });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SProductExists(int key)
        {
            return db.SProducts.Count(e => e.Id == key) > 0;
        }
    }
}
