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
    builder.EntitySet<Venue>("Venues");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class VenuesController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/Venues
        [EnableQuery]
        public IQueryable<Venue> GetVenues()
        {
            return db.Venues;
        }

        // GET: odata/Venues(5)
        [EnableQuery]
        public SingleResult<Venue> GetVenue([FromODataUri] int key)
        {
            return SingleResult.Create(db.Venues.Where(venue => venue.Id == key));
        }

        // PUT: odata/Venues(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Venue> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Venue venue = db.Venues.Find(key);
            if (venue == null)
            {
                return NotFound();
            }

            patch.Put(venue);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VenueExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(venue);
        }

        // POST: odata/Venues
        public IHttpActionResult Post(Venue venue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Venues.Add(venue);
            db.SaveChanges();

            return Created(venue);
        }

        // PATCH: odata/Venues(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Venue> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Venue venue = db.Venues.Find(key);
            if (venue == null)
            {
                return NotFound();
            }

            patch.Patch(venue);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VenueExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(venue);
        }

        // DELETE: odata/Venues(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Venue venue = db.Venues.Find(key);
            if (venue == null)
            {
                return NotFound();
            }

            db.Venues.Remove(venue);
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

        private bool VenueExists(int key)
        {
            return db.Venues.Count(e => e.Id == key) > 0;
        }
    }
}
