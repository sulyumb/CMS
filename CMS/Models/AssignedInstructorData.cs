using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class AssignedInstructorData
    {
        public int InstructorID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Speciality { get; set; }
        public DateTime JoinedDate { get; set; }

        public bool Assigned { get; set; }
  

        
    }
}