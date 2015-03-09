using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Models
{
  
        public enum Qualification
        {
            //Select,
            Diploma,
            Bacholer, Master, PhD
        }


        public enum Major
        {
            //Select,
            [Display(Name = "Software Engneering")]
            SoftwareEngneering,
            [Display(Name = "Information System")]
            InformationSystem,
            [Display(Name = "Information Technology")]
            InformationTechnology,

            [Display(Name = "Computer Engneering")]
            ComputerEngneering,
            [Display(Name = "Computer Science")]
            ComputerScience,
            [Display(Name = "Computer Security")]
            ComputerSecurity,
            [Display(Name = "Health Informatics")]
            HealthInformatics,
            [Display(Name = "Mangement Information System")]
            MangementInformationSystem,
            [Display(Name = "Telecommuncation Engneering")]
            TelecommuncationEngneering,
            [Display(Name = "Graphic Design")]
            GraphicDesign

        }
        public class Employee
        {

            [HiddenInput]
            public string Region { get; set; }

            [Display(Name = "Badge")]
            public string id { get; set; }



            [Required]
            public string Name { get; set; }

            [Required]
            public string Position { get; set; }



            [DisplayFormat(NullDisplayText = "Select")]
            [Required]
            public Qualification? Qualification { get; set; }
            [DisplayFormat(NullDisplayText = "Select")]
            [Required]
            public Major? Major { get; set; }


            public int Experience { get; set; }

            [Display(Name = "Joined Date")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public DateTime JoinedDate { get; set; }


            public string Extension { get; set; }

            public string Mobile { get; set; }


            [Display(Name = "Office#")]

            public string OfficeNo { get; set; }


            [Display(Name = "Work Achievement")]

            [DataType(DataType.MultilineText)]
            public string Work_Achivement { get; set; }

            [NotMapped]
            [Display(Name = "C.V")]
            [DataType(DataType.Upload)]
            public HttpPostedFileBase CV { get; set; }

            public string cvName { get; set; }

            public DateTime cvUpdatedDate { get; set; }

            [NotMapped]
            [Display(Name = "Personal Photo")]
            [DataType(DataType.Upload)]
            public HttpPostedFileBase PersonalPhoto { get; set; }



        }
   
}