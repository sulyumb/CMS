using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class CMSContext : DbContext
    {
        public CMSContext() : base("CMSContext")
        { }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Block> Blocks { get; set; }

 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Instructor>()
                .HasMany(c => c.Sessions).WithMany(i => i.Instructors)
                .Map(t => t.MapLeftKey("InstructorID")
                .MapRightKey("SessionID")
                .ToTable("SessionInstructor"));

            //modelBuilder.Entity<Block>().HasMany<Session>(s => s.Sessinos)
            //    .WithRequired(s => s.Block).HasForeignKey(s => s.BlockID);

            //modelBuilder.Entity<Hall>()
            //    .HasMany(c => c.Sessions).WithMany(i => i.Halls)
            //    .Map(t => t.MapLeftKey("HallID")
            //    .MapRightKey("SessionID")
            //    .ToTable("SessionHall"));

            base.OnModelCreating(modelBuilder);

        }
    }
}