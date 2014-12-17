using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMS.Models;

namespace CMS.Controllers
{
    public class SessionsController : Controller
    {
        private CMSContext db = new CMSContext();
       
        // GET: Sessions
        public ActionResult Index()
        {
            var sessions = db.Sessions.Include(s => s.Block);
            return View(sessions.ToList());
        }

        // GET: Sessions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        private ICollection<AssignedInstructorData> PopulateInstructorData()
        {
            var Instructores = db.Instructors;
            var assignedInstructors = new List<AssignedInstructorData>();

            foreach (var item in Instructores)
            {
                assignedInstructors.Add(new AssignedInstructorData
                {
                    InstructorID = item.InstructorID,

                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    JoinedDate = item.JoinedDate,
                    Speciality = item.Speciality,

                    Assigned =false 
                });
            }
            return assignedInstructors ;
        }

        private ICollection<AssignedHallData> PopulateHallData()
        {
            var Halls = db.Halls;
            var assignedHalls = new List<AssignedHallData>();

            foreach (var item in Halls)
            {
                assignedHalls.Add(new AssignedHallData
                {
                    HallID = item.HallID,
                    Room = item.Room,
                    SeatNo = item.SeatNo,
                    Assigned = false
                });
            }
            return assignedHalls;
        }

        private void AddOrUpdateInstructors(Session session, IEnumerable<AssignedInstructorData> assignedInstructors)
        {
            foreach (var assignedInstructor in assignedInstructors)
            {
                if (assignedInstructor.Assigned)
                {
                    var Instructor = new Instructor {
                        InstructorID = assignedInstructor.InstructorID,
                        FirstName = assignedInstructor.FirstName,
                        JoinedDate = assignedInstructor.JoinedDate,
                        LastName =assignedInstructor.LastName,
                        Speciality = assignedInstructor.Speciality
                    };
                    db.Instructors.Attach(Instructor);
                    session.Instructors.Add(Instructor);
                }
            }

            //foreach (var assignedHall in assignedHalls)
            //{
            //    if (assignedHall.Assigned)
            //    {
            //        var Hall = new Hall { HallID = assignedHall.HallID };
            //        db.Halls.Attach(Hall);
            //        session.Halls.Add(Hall);
            //    }
            //}

        }

        private void AddOrUpdateHalls(Session session, IEnumerable<AssignedHallData> assignedHalls)
        {
            foreach (var assignedHall in assignedHalls)
            {
                if (assignedHall.Assigned)
                {
                    var hall = new Hall
                    {
                        HallID = assignedHall.HallID,
                        SeatNo = assignedHall.SeatNo,
                        Room = assignedHall.Room
                    };
                    db.Halls.Attach(hall);
                    session.Halls.Add(hall);
                }
            }
        }


        // GET: Sessions/Create
        public ActionResult Create()
        {
            ViewBag.BlockID = new SelectList(db.Blocks, "ID", "Name");
            var sessionViewModel = new SessionViewModel { Instructors =PopulateInstructorData(), Halls = PopulateHallData() };

            return View(sessionViewModel);
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "SessionID,ActivitySubject,Year,Week_no,BlockID,Theme,Day,Date,StartTime,EndTime,ActivityT,StatusT,Descipline,Objectives")] Session session)
        public ActionResult Create(SessionViewModel sessionViewModel)
        {
            if (ModelState.IsValid)
            {
                var session = new Session
                {
                    ActivitySubject = sessionViewModel.ActivitySubject,
                    ActivityT = Models.Session.ActivityType.IntroductionTotheBlock,
                    Block = sessionViewModel.Block,
                    BlockID = sessionViewModel.BlockID,
                    Date = sessionViewModel.Date,
                    Day = sessionViewModel.Day,

                    StartTime = sessionViewModel.StartTime,
                    EndTime = sessionViewModel.EndTime,
                    Objectives = sessionViewModel.Objectives,

                    Theme = sessionViewModel.Theme,
                    Week_no = sessionViewModel.Week_no,
                    Year = sessionViewModel.Year
                };

                AddOrUpdateInstructors(session, sessionViewModel.Instructors );
                AddOrUpdateHalls(session, sessionViewModel.Halls);

                db.Sessions.Add(session);
                db.SaveChanges();

        
                return RedirectToAction("Index");
            }

            ViewBag.BlockID = new SelectList(db.Blocks, "ID", "Name", sessionViewModel.BlockID);
            return View(sessionViewModel);
        }

        // GET: Sessions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlockID = new SelectList(db.Blocks, "ID", "Name", session.BlockID);
            return View(session);
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SessionID,ActivitySubject,Year,Week_no,BlockID,Theme,Day,Date,StartTime,EndTime,ActivityT,StatusT,Descipline,Objectives")] Session session)
        {
            if (ModelState.IsValid)
            {
                db.Entry(session).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BlockID = new SelectList(db.Blocks, "ID", "Name", session.BlockID);
            return View(session);
        }

        // GET: Sessions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Session session = db.Sessions.Find(id);
            db.Sessions.Remove(session);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();

            }
            base.Dispose(disposing);
        }

     
    }
}
