using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class AssignedCoursesData
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }

        public string CourseCode { get; set; }

        public bool Assigned { get; set; }
    }
}