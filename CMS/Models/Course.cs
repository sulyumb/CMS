using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class Course
    {
        public Course()
        {
            Instructors = new List<Instructor>();
        }

        public int CourseID { get; set; }

        [Display(Name="Course Name")]
        public string CourseName { get; set; }

        public string CourseCode { get; set; }

        public virtual ICollection<Instructor> Instructors { get; set; }

    }
}