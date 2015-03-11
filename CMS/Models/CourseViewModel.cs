using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class CourseViewModel
    {
        public CourseViewModel()
        {
            Instructors = new Collection<AssignedInstructorData>();
        }

        public int CourseID { get; set; }

        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        public string CourseCode { get; set; }

        public virtual ICollection<AssignedInstructorData> Instructors { get; set; }
    }
}