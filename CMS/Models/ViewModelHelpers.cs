using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public static class ViewModelHelpers
    {
        public static SessionViewModel ToViewModel(this Session session)
        {
            var sessionViewModel = new SessionViewModel
            {
                Day = session.Day,
                ActivitySubject = session.ActivitySubject,
                ActivityT = session.ActivityT ,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                SessionID = session.SessionID,
                BlockID = session.BlockID,
                Block = session.Block,
                Date = session.Date,
                Year = session.Year,
                Week_no = session.Week_no,
                Theme = session.Theme
                             
            };

            foreach (var instructor in session.Instructors)
            {
                sessionViewModel.Instructors.Add(new AssignedInstructorData
                {
                     FirstName = instructor.FirstName,
                     LastName = instructor.LastName,
                     InstructorID = instructor.InstructorID,
                     JoinedDate = instructor.JoinedDate,
                     Speciality = instructor.Speciality,
                     Assigned = true
                });

            };

            foreach (var hall in session.Halls)
            {
                sessionViewModel.Halls.Add(new AssignedHallData
                {
                 HallID = hall.HallID,
                 Room = hall.Room,
                 SeatNo = hall.SeatNo,
                 Assigned = true
                });
            };

            return sessionViewModel;

        }

        public static SessionViewModel ToViewModel(this Session session, ICollection<Instructor> allDbInstructors, ICollection<Hall> allDbHalls)
        {
            var sessionViewModel = new SessionViewModel
            {
                Day = session.Day,
                ActivitySubject = session.ActivitySubject,
                ActivityT = session.ActivityT,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                SessionID = session.SessionID,
                StatusT = session.StatusT,
                BlockID = session.BlockID,
                Block = session.Block,
                Date = session.Date,
                Year = session.Year,
                Week_no = session.Week_no,
                Theme = session.Theme
            };

            ICollection<AssignedInstructorData> allInstructors = new List<AssignedInstructorData>();
            ICollection<AssignedHallData> allHalls = new List<AssignedHallData>();

            foreach (var c in allDbInstructors)
            {
                // Create new AssignedCourseData for each course and set Assigned = true if user already has course
                var assignedInstructor = new AssignedInstructorData 
                {
                    InstructorID = c.InstructorID,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    JoinedDate = c.JoinedDate,
                    Speciality = c.Speciality,
                    Assigned = session.Instructors.FirstOrDefault(x => x.InstructorID  == c.InstructorID) != null
                };

                allInstructors.Add(assignedInstructor);
            }

            foreach (var c in allDbHalls)
            {
                var assignedHall = new AssignedHallData
                {
                    HallID = c.HallID,
                    Room = c.Room,
                    SeatNo = c.SeatNo,
                    Assigned = session.Halls.FirstOrDefault(x => x.HallID == c.HallID) != null
                };
                allHalls.Add(assignedHall);
            }

            sessionViewModel.Instructors = allInstructors;
            sessionViewModel.Halls = allHalls;

            return sessionViewModel ;
        }

        public static SessionViewModel ToDomainModel(this SessionViewModel sessionViewModel)
        {
            var session = new Session
            {
                ActivityT = sessionViewModel.ActivityT,
                Day = sessionViewModel.Day,
                ActivitySubject = sessionViewModel.ActivitySubject,
                StartTime = sessionViewModel.StartTime,
                EndTime = sessionViewModel.EndTime,
                SessionID = sessionViewModel.SessionID,
                BlockID = sessionViewModel.BlockID,
                Block = sessionViewModel.Block,
                Date = sessionViewModel.Date,
                Year = sessionViewModel.Year,
                Week_no = sessionViewModel.Week_no,
                Theme = sessionViewModel.Theme
            };
            return sessionViewModel;

        }
    }
}