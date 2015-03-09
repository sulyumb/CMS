using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CMS.Models;

namespace CMS.Models
{
    public class SessionInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {

            var blocks = new List<Block>
            {
                new Block { Name = "CardiovascularScience" },
                new Block { Name = "Heart" },
                new Block { Name = "Neourology" }

            };


            foreach (var i in blocks)
            {
                context.Blocks.Add(i);
            }

            context.SaveChanges();


    
            var sessions = new List<Session>
            {
                 new Session {
                     Year ="2014" ,
                     ActivitySubject = "Introduction" ,
                     //ActivityT = Session.ActivityType.Lecture ,
                     Date = DateTime.Parse("2014-02-01") ,
                     Day = DayOfWeek.Sunday ,
                     StartTime = DateTime.Parse("08:00:00") , EndTime = DateTime.Parse("10:00:00"),
                     Week_no = 1,
                     Theme = "GoingDownHill",
                     BlockID = 1
                },

                   new Session {
                     Year ="2014" ,
                     ActivitySubject = "FirstLecture" ,
                     //ActivityT = Session.ActivityType.BasicandClinicalScience ,
                     Date = DateTime.Parse("2014-02-01") ,
                     Day = DayOfWeek.Sunday ,
                     StartTime = DateTime.Parse("10:00:00") , EndTime = DateTime.Parse("12:00:00"),
                     Week_no = 1,
                     Theme = "GoingDownHill",
                     BlockID = 1
                },

                  new Session {
                     Year ="2014" ,
                     ActivitySubject = "Social Health" ,
                     //ActivityT = Session.ActivityType.CaseBasedLearning ,
                     Date = DateTime.Parse("2014-03-01") ,
                     Day = DayOfWeek.Sunday ,
                     StartTime = DateTime.Parse("08:00:00") , EndTime = DateTime.Parse("10:00:00"),
                     Week_no = 1,
                     Theme = "AnotherTheme",
                     BlockID = 2
                }

            };

            foreach (var i in sessions)
            {
                context.Sessions.Add(i);
            }

            context.SaveChanges();

            var Instructors = new List<Instructor>
              {
                new Instructor { FirstName = "Mohammed" , LastName = "Ali" , JoinedDate = DateTime.Parse("2012-01-01") , Speciality = "Lab" },
                new Instructor { FirstName = "Saeed" , LastName = "Saleh" , JoinedDate = DateTime.Parse("2012-02-02") , Speciality = "Neourology" },
                new Instructor { FirstName = "Ahmed" , LastName = "Abdullah" , JoinedDate = DateTime.Parse("2012-01-01") , Speciality = "Anatomy" },
                new Instructor { FirstName = "Salim" , LastName = "Mohammed" , JoinedDate = DateTime.Parse("2012-01-01") , Speciality = "Thorax" },
                new Instructor { FirstName = "Khalid" , LastName = "Ahmed" , JoinedDate = DateTime.Parse("2012-01-01") , Speciality = "Cardiac" }

              };

            foreach (var i in Instructors)
            {
                context.Instructors.Add(i);
            }

            context.SaveChanges();

            var Halls = new List<Hall>
              {
                 new Hall {  Room = "KingFaisal" , SeatNo=156   },
                 new Hall {  Room = "Room1"  , SeatNo= 258  },
                 new Hall {  Room = "Room2" ,  SeatNo=400  }
              };

            foreach (var i in Halls)
            {
                context.Halls.Add(i);
            }

            context.SaveChanges();

        }


    }
}