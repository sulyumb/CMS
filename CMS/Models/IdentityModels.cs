using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;
using CMS.Models;
using CMS;

namespace CMS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string FullName { get; set; }
    }

    //public class ApplicationDbContext : BaseContext<ApplicationDbContext>
    //{
    //    //public ApplicationDbContext()
    //    //    : base("DefaultConnection", throwIfV1Schema: false)
    //    //{
    //    //}

    //public static ApplicationDbContext Create()
    //{
    //    return new ApplicationDbContext();
    //}
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public string connectionString;

        public ApplicationDbContext()

        {
        }


        public ApplicationDbContext(string connectionString)
           : base(connectionString)
        {
        }

        static ApplicationDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()

        {
            ApplicationDbContext dbcon = new ApplicationDbContext("DefaultConnection");
            return dbcon;
        }

        //public override IDbSet<ApplicationUser> Users { get; set; }
        //public override DbSet<ApplicationUser> Users { get; set; }
      

        public DbSet<Session> Sessions { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Block> Blocks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //    //    //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Instructor>()
                .HasMany(c => c.Sessions).WithMany(i => i.Instructors)
                .Map(t => t.MapLeftKey("InstructorID")
                .MapRightKey("SessionID")
                .ToTable("SessionInstructor"));

            modelBuilder.Entity<Hall>()
              .HasMany(c => c.Sessions).WithMany(i => i.Halls)
              .Map(t => t.MapLeftKey("HallID")
              .MapRightKey("SessionID")
              .ToTable("SessionHall"));

            //    modelBuilder.Entity<Block>().HasMany<Session>(s => s.Sessinos)
            //        .WithRequired(s => s.Block).HasForeignKey(s => s.BlockID);

            //    //    //modelBuilder.Entity<Hall>()
            //    //    //    .HasMany(c => c.Sessions).WithMany(i => i.Halls)
            //    //    //    .Map(t => t.MapLeftKey("HallID")
            //    //    //    .MapRightKey("SessionID")
            //    //    //    .ToTable("SessionHall"));



        }
    }
}


