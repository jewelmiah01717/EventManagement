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
using EventManagementPro.Models;

namespace EventManagementPro.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using EventManagementPro.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<RequestProduct>("RequestProducts");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class RequestProductsController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/RequestProducts
        [EnableQuery]
        public IQueryable<RequestProduct> GetRequestProducts()
        {
            return db.RequestProducts;
        }

        // GET: odata/RequestProducts(5)
        [EnableQuery]
        public SingleResult<RequestProduct> GetRequestProduct([FromODataUri] int key)
        {
            return SingleResult.Create(db.RequestProducts.Where(requestProduct => requestProduct.Id == key));
        }

        // PUT: odata/RequestProducts(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<RequestProduct> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RequestProduct requestProduct = db.RequestProducts.Find(key);
            if (requestProduct == null)
            {
                return NotFound();
            }

            patch.Put(requestProduct);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(requestProduct);
        }

        // POST: odata/RequestProducts
        public IHttpActionResult Post(RequestProduct requestProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RequestProducts.Add(requestProduct);
            db.SaveChanges();

            return Created(requestProduct);
        }

        // PATCH: odata/RequestProducts(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<RequestProduct> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RequestProduct requestProduct = db.RequestProducts.Find(key);
            if (requestProduct == null)
            {
                return NotFound();
            }

            patch.Patch(requestProduct);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(requestProduct);
        }

        // DELETE: odata/RequestProducts(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            RequestProduct requestProduct = db.RequestProducts.Find(key);
            if (requestProduct == null)
            {
                return NotFound();
            }

            db.RequestProducts.Remove(requestProduct);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RequestProductExists(int key)
        {
            return db.RequestProducts.Count(e => e.Id == key) > 0;
        }
    }
}
