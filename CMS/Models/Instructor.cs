using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class Instructor
    {
        
        public int InstructorID { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Family Name")]
        public string LastName { get; set; }

        public string Speciality { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Joined Date")]
        public DateTime JoinedDate { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + ", " + LastName;
            }       
        }

        //[DisplayColumn(Session.ActivitySubject.ToString)]
        public virtual ICollection<Session> Sessions { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
      
    }
}