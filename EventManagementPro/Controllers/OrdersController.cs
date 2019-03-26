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
    builder.EntitySet<Order>("Orders");
    builder.EntitySet<OrderDetail>("OrderDetails"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class OrdersController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/Orders
        [EnableQuery]
        public IQueryable<Order> GetOrders()
        {
            return db.Orders;
        }

        // GET: odata/Orders(5)
        [EnableQuery]
        public SingleResult<Order> GetOrder([FromODataUri] int key)
        {
            return SingleResult.Create(db.Orders.Where(order => order.OrderId == key));
        }

        // PUT: odata/Orders(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Order> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Order order = db.Orders.Find(key);
            if (order == null)
            {
                return NotFound();
            }

            patch.Put(order);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(order);
        }

        // POST: odata/Orders
        public IHttpActionResult Post(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orders.Add(order);
            db.SaveChanges();

            return Created(order);
        }

        // PATCH: odata/Orders(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Order> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Order order = db.Orders.Find(key);
            if (order == null)
            {
                return NotFound();
            }

            patch.Patch(order);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(order);
        }

        // DELETE: odata/Orders(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Order order = db.Orders.Find(key);
            if (order == null)
            {
                return NotFound();
            }

            db.Orders.Remove(order);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Orders(5)/OrderDetails
        [EnableQuery]
        public IQueryable<OrderDetail> GetOrderDetails([FromODataUri] int key)
        {
            return db.Orders.Where(m => m.OrderId == key).SelectMany(m => m.OrderDetails);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int key)
        {
            return db.Orders.Count(e => e.OrderId == key) > 0;
        }
    }
}
