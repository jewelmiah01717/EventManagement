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
    builder.EntitySet<OrderDetail>("OrderDetails");
    builder.EntitySet<Order>("Orders"); 
    builder.EntitySet<SProduct>("SProducts"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class OrderDetailsController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/OrderDetails
        [EnableQuery]
        public IQueryable<OrderDetail> GetOrderDetails()
        {
            return db.OrderDetails;
        }

        // GET: odata/OrderDetails(5)
        [EnableQuery]
        public SingleResult<OrderDetail> GetOrderDetail([FromODataUri] int key)
        {
            return SingleResult.Create(db.OrderDetails.Where(orderDetail => orderDetail.OrderId == key));
        }

        // PUT: odata/OrderDetails(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<OrderDetail> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            OrderDetail orderDetail = db.OrderDetails.Find(key);
            if (orderDetail == null)
            {
                return NotFound();
            }

            patch.Put(orderDetail);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(orderDetail);
        }

        // POST: odata/OrderDetails
        public IHttpActionResult Post(OrderDetail orderDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrderDetails.Add(orderDetail);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OrderDetailExists(orderDetail.OrderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(orderDetail);
        }

        // PATCH: odata/OrderDetails(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<OrderDetail> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            OrderDetail orderDetail = db.OrderDetails.Find(key);
            if (orderDetail == null)
            {
                return NotFound();
            }

            patch.Patch(orderDetail);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(orderDetail);
        }

        // DELETE: odata/OrderDetails(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(key);
            if (orderDetail == null)
            {
                return NotFound();
            }

            db.OrderDetails.Remove(orderDetail);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/OrderDetails(5)/Order
        [EnableQuery]
        public SingleResult<Order> GetOrder([FromODataUri] int key)
        {
            return SingleResult.Create(db.OrderDetails.Where(m => m.OrderId == key).Select(m => m.Order));
        }

        // GET: odata/OrderDetails(5)/SProduct
        [EnableQuery]
        public SingleResult<SProduct> GetSProduct([FromODataUri] int key)
        {
            return SingleResult.Create(db.OrderDetails.Where(m => m.OrderId == key).Select(m => m.SProduct));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderDetailExists(int key)
        {
            return db.OrderDetails.Count(e => e.OrderId == key) > 0;
        }
    }
}
