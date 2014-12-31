using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class Session
    {
      
        //public enum ActivityType
        //{
        //    Lecture = 0,
        //    IntroductionTotheBlock =1,
        //    BasicandClinicalScience=2,
        //    ProblemBasedLearning=3,
        //    Other=4,
        //    Laboratory=5,
        //    ClinicalDiagnosticSkills=6,
        //    ProceduralSkills=7,
        //    ClinicalSkillsTraining=8,
        //    PracticalSkills=9,
        //    CaseBasedLearning=10,
        //    CompensationComputation=11,
        //    CommunityAndDoctor=12,
        //    EvidenceBasedMedicine=13,
        //    PatientAndDoctor=14,
        //    PersonalAndProfessionalDevelopment=15,
        //    IntegrativeTheme=16,
        //    OSCE=17,
        //    OSPE=18,
        //    Exam=19,
        //    Tutorial=20
        //}

        //public enum StatusType
        //{
        //    GQ=0,
        //    YS=1,
        //    RS=2,
        //    YQ=3,
        //    RQ=4
        //}

        public enum Disciplines
        {
            Anatomy=0,
            Histology=1,
            Cardiology=2
        }

        public Session()
        {
            Instructors = new List<Instructor>();
            Halls = new List<Hall>();
        }


        public int SessionID { get; set; }

        [DisplayName("Activity Subject")]
        public string ActivitySubject { get; set; }

        [DisplayName("Year")]
        public string Year { get; set; }

        [DisplayName("Week No")]
        public int Week_no { get; set; }

        
        public int BlockID { get; set; }
        public virtual Block Block { get; set; }

        public string Theme { get; set; }

        public DayOfWeek Day { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }

        [DisplayName("Start Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:t}")]
        public DateTime StartTime { get; set; }

        [DisplayName("EndTime")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:t}")]
        public DateTime EndTime { get; set; }

        [DisplayName("Activity Type")]
        public string ActivityT { get; set; }

        [DisplayName("Status")]
        public string StatusT { get; set; }

        [DisplayName("Discipline")]
        public Disciplines? Descipline { get; set; }

        //to make string optional
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Objectives { get; set; }

        public virtual ICollection<Instructor> Instructors { get; set; }

        public virtual ICollection<Hall> Halls { get; set; }
  
    }
}