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
    builder.EntitySet<Subscriber>("Subscribers");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class SubscribersController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/Subscribers
        [EnableQuery]
        public IQueryable<Subscriber> GetSubscribers()
        {
            return db.Subscribers;
        }

        // GET: odata/Subscribers(5)
        [EnableQuery]
        public SingleResult<Subscriber> GetSubscriber([FromODataUri] int key)
        {
            return SingleResult.Create(db.Subscribers.Where(subscriber => subscriber.ID == key));
        }

        // PUT: odata/Subscribers(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Subscriber> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Subscriber subscriber = db.Subscribers.Find(key);
            if (subscriber == null)
            {
                return NotFound();
            }

            patch.Put(subscriber);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubscriberExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(subscriber);
        }

        // POST: odata/Subscribers
        public IHttpActionResult Post(Subscriber subscriber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Subscribers.Add(subscriber);
            db.SaveChanges();

            return Created(subscriber);
        }

        // PATCH: odata/Subscribers(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Subscriber> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Subscriber subscriber = db.Subscribers.Find(key);
            if (subscriber == null)
            {
                return NotFound();
            }

            patch.Patch(subscriber);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubscriberExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(subscriber);
        }

        // DELETE: odata/Subscribers(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Subscriber subscriber = db.Subscribers.Find(key);
            if (subscriber == null)
            {
                return NotFound();
            }

            db.Subscribers.Remove(subscriber);
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

        private bool SubscriberExists(int key)
        {
            return db.Subscribers.Count(e => e.ID == key) > 0;
        }
    }
}
