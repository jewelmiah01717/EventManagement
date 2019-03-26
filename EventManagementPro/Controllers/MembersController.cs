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
    builder.EntitySet<Member>("Members");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class MembersController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/Members
        [EnableQuery]
        public IQueryable<Member> GetMembers()
        {
            return db.Members;
        }

        // GET: odata/Members(5)
        [EnableQuery]
        public SingleResult<Member> GetMember([FromODataUri] int key)
        {
            return SingleResult.Create(db.Members.Where(member => member.ID == key));
        }

        // PUT: odata/Members(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Member> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Member member = db.Members.Find(key);
            if (member == null)
            {
                return NotFound();
            }

            patch.Put(member);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(member);
        }

        // POST: odata/Members
        public IHttpActionResult Post(Member member)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Members.Add(member);
            db.SaveChanges();

            return Created(member);
        }

        // PATCH: odata/Members(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Member> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Member member = db.Members.Find(key);
            if (member == null)
            {
                return NotFound();
            }

            patch.Patch(member);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(member);
        }

        // DELETE: odata/Members(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Member member = db.Members.Find(key);
            if (member == null)
            {
                return NotFound();
            }

            db.Members.Remove(member);
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

        private bool MemberExists(int key)
        {
            return db.Members.Count(e => e.ID == key) > 0;
        }
    }
}
