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
    builder.EntitySet<Message>("Messages");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class MessagesController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/Messages
        [EnableQuery]
        public IQueryable<Message> GetMessages()
        {
            return db.Messages;
        }

        // GET: odata/Messages(5)
        [EnableQuery]
        public SingleResult<Message> GetMessage([FromODataUri] int key)
        {
            return SingleResult.Create(db.Messages.Where(message => message.ID == key));
        }

        // PUT: odata/Messages(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Message> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Message message = db.Messages.Find(key);
            if (message == null)
            {
                return NotFound();
            }

            patch.Put(message);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(message);
        }

        // POST: odata/Messages
        public IHttpActionResult Post(Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Messages.Add(message);
            db.SaveChanges();

            return Created(message);
        }

        // PATCH: odata/Messages(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Message> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Message message = db.Messages.Find(key);
            if (message == null)
            {
                return NotFound();
            }

            patch.Patch(message);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(message);
        }

        // DELETE: odata/Messages(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Message message = db.Messages.Find(key);
            if (message == null)
            {
                return NotFound();
            }

            db.Messages.Remove(message);
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

        private bool MessageExists(int key)
        {
            return db.Messages.Count(e => e.ID == key) > 0;
        }
    }
}
