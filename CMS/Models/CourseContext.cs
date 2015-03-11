using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class CourseContext : ApplicationDbContext
    {
        //public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Instructor>()
                  .HasMany(c => c.Courses).WithMany(i => i.Instructors)
                  .Map(t => t.MapLeftKey("Instructor_InstructorID")
                  .MapRightKey("Course_CourseID")
                  .ToTable("CourseInstructors"));

        }


    }
}