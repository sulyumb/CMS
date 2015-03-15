using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CMS.Models;
using CMS.AssignedDataM2M;

namespace CMS.ViewModels
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