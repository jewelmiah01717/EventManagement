using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using EventManagementPro.API.Models;

namespace EventManagementPro.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public string Name { get; set; }

        public string Address { get; set; }
        public string img { get; set; }
        public string Gender { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<IdentityUser>().ToTable("Users"); // Won't work if the table name is the same.
            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers")
                .Property(p => p.Name).IsOptional();

            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers")
              .Property(p => p.Address).IsOptional();
            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers")
             .Property(p => p.img).IsOptional();
            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers")
             .Property(p => p.Gender).IsOptional();
        }

        //public DbSet<Product> Book { get; set; }

        //public DbSet<Category> Category { get; set; }

        //public DbSet<Rack> Rack { get; set; }

        //public DbSet<Member> Member { get; set; }
        //public DbSet<Purchase> Purchase { get; set; }
        //public DbSet<IssueBookReq> IssueBookReq { get; set; }
        //public DbSet<Message> Messages { get; set; }
        //public DbSet<Subscriber> Subscribers { get; set; }

        //public DbSet<SProduct> SBooks { get; set; }
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderDetail> OrderDetails { get; set; }
        //public DbSet<StockIn> StockIns { get; set; }

      


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<EventManagementPro.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<EventManagementPro.Models.Event> Events { get; set; }

        public System.Data.Entity.DbSet<EventManagementPro.Models.Member> Members { get; set; }

        public System.Data.Entity.DbSet<EventManagementPro.Models.Message> Messages { get; set; }

        public System.Data.Entity.DbSet<EventManagementPro.Models.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<EventManagementPro.Models.OrderDetail> OrderDetails { get; set; }

        public System.Data.Entity.DbSet<EventManagementPro.Models.SProduct> SProducts { get; set; }

        public System.Data.Entity.DbSet<EventManagementPro.Models.Product> Products { get; set; }

        public System.Data.Entity.DbSet<EventManagementPro.Models.Purchase> Purchases { get; set; }

        public System.Data.Entity.DbSet<EventManagementPro.Models.RequestProduct> RequestProducts { get; set; }

        public System.Data.Entity.DbSet<EventManagementPro.Models.StockIn> StockIns { get; set; }

        public System.Data.Entity.DbSet<EventManagementPro.Models.Subscriber> Subscribers { get; set; }

        public System.Data.Entity.DbSet<EventManagementPro.Models.Venue> Venues { get; set; }

        public System.Data.Entity.DbSet<EventManagementPro.Models.VenueBooking> VenueBookings { get; set; }
    }
}