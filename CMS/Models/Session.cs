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
      
        public enum ActivityType
        {
            Lecture,
            IntroductionTotheBlock,
            BasicandClinicalScience,
            ProblemBasedLearning,
            Other,
            Laboratory,
            ClinicalDiagnosticSkills,
            ProceduralSkills,
            ClinicalSkillsTraining,
            PracticalSkills,
            CaseBasedLearning,
            CompensationComputation,
            CommunityAndDoctor,
            EvidenceBasedMedicine,
            PatientAndDoctor,
            PersonalAndProfessionalDevelopment,
            IntegrativeTheme,
            OSCE,
            OSPE,
            Exam,
            Tutorial
        }

        public enum StatusType
        {
            GQ,
            YS,
            RS,
            YQ,
            RQ
        }

        public enum Disciplines
        {
            Anatomy,
            Histology,
            Cardiology
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
        public ActivityType ActivityT { get; set; }

        [DisplayName("Status")]
        public StatusType StatusT { get; set; }

        [DisplayName("Discipline")]
        public Disciplines? Descipline { get; set; }

        //to make string optional
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Objectives { get; set; }

        public virtual ICollection<Instructor> Instructors { get; set; }

        public virtual ICollection<Hall> Halls { get; set; }
  
    }
}