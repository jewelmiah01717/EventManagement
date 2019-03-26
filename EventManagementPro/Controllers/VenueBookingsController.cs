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
    builder.EntitySet<VenueBooking>("VenueBookings");
    builder.EntitySet<Event>("Events"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class VenueBookingsController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/VenueBookings
        [EnableQuery]
        public IQueryable<VenueBooking> GetVenueBookings()
        {
            return db.VenueBookings;
        }

        // GET: odata/VenueBookings(5)
        [EnableQuery]
        public SingleResult<VenueBooking> GetVenueBooking([FromODataUri] int key)
        {
            return SingleResult.Create(db.VenueBookings.Where(venueBooking => venueBooking.VenueBookingID == key));
        }

        // PUT: odata/VenueBookings(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<VenueBooking> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VenueBooking venueBooking = db.VenueBookings.Find(key);
            if (venueBooking == null)
            {
                return NotFound();
            }

            patch.Put(venueBooking);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VenueBookingExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(venueBooking);
        }

        // POST: odata/VenueBookings
        public IHttpActionResult Post(VenueBooking venueBooking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VenueBookings.Add(venueBooking);
            db.SaveChanges();

            return Created(venueBooking);
        }

        // PATCH: odata/VenueBookings(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<VenueBooking> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VenueBooking venueBooking = db.VenueBookings.Find(key);
            if (venueBooking == null)
            {
                return NotFound();
            }

            patch.Patch(venueBooking);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VenueBookingExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(venueBooking);
        }

        // DELETE: odata/VenueBookings(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            VenueBooking venueBooking = db.VenueBookings.Find(key);
            if (venueBooking == null)
            {
                return NotFound();
            }

            db.VenueBookings.Remove(venueBooking);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/VenueBookings(5)/Event
        [EnableQuery]
        public SingleResult<Event> GetEvent([FromODataUri] int key)
        {
            return SingleResult.Create(db.VenueBookings.Where(m => m.VenueBookingID == key).Select(m => m.Event));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VenueBookingExists(int key)
        {
            return db.VenueBookings.Count(e => e.VenueBookingID == key) > 0;
        }
    }
}
