using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMS.Models;
using System.IO;

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
            if (assignedInstructors == null) return;


            if (session.SessionID != 0)
            {
                foreach (var instructor in session.Instructors.ToList())
                {
                    session.Instructors.Remove(instructor);
                }

                foreach (var instructor in assignedInstructors.Where(w => w.Assigned))
                {
                    session.Instructors.Add(db.Instructors.Find(instructor.InstructorID));
                }
            }
            else
            {
                //new hall
                foreach (var assignedInstructor in assignedInstructors.Where(w => w.Assigned))
                {
                    var instructor = new Instructor {  InstructorID = assignedInstructor.InstructorID };
                    db.Instructors.Attach(instructor);
                    session.Instructors.Add(instructor);
                }
            }


        }

        private void AddOrUpdateHalls(Session session, IEnumerable<AssignedHallData> assignedHalls)
        {
            if (assignedHalls == null) return;


            if (session.SessionID != 0)
            {
                foreach (var hall in session.Halls.ToList())
                {
                    session.Halls.Remove(hall);
                }

                foreach (var hall in assignedHalls.Where(w => w.Assigned))
                {
                    session.Halls.Add(db.Halls.Find(hall.HallID));
                }
            }
            else
            {
                //new hall
                foreach (var assignedHall in assignedHalls.Where(w => w.Assigned))
                {
                    var hall = new Hall { HallID = assignedHall.HallID };
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
                    ActivityT = sessionViewModel.ActivityT,
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

        //private void AddOrUpdateCourses(UserProfile userProfile, IEnumerable<AssignedCourseData> assignedCourses)
        //{
        //    if (assignedCourses == null) return;

        //    if (userProfile.UserProfileID != 0)
        //    {
        //        // Existing user - drop existing courses and add the new ones if any
        //        foreach (var course in userProfile.Courses.ToList())
        //        {
        //            userProfile.Courses.Remove(course);
        //        }

        //        foreach (var course in assignedCourses.Where(c => c.Assigned))
        //        {
        //            userProfile.Courses.Add(db.Courses.Find(course.CourseID));
        //        }
        //    }
        //    else
        //    {
        //        // New user
        //        foreach (var assignedCourse in assignedCourses.Where(c => c.Assigned))
        //        {
        //            var course = new Course { CourseID = assignedCourse.CourseID };
        //            db.Courses.Attach(course);
        //            userProfile.Courses.Add(course);
        //        }
        //    }
        //}

        // GET: Sessions/Edit/5
        public ActionResult Edit(int? id)
        {
            var allInstructors = db.Instructors.ToList();
            
            var allHalls = db.Halls.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Sessions.Include("Instructors").FirstOrDefault(x => x.SessionID == id);
            //session = db.Sessions.Include("Halls").FirstOrDefault(x => x.SessionID == id);
            if (session == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlockID = new SelectList(db.Blocks, "ID", "Name", session.BlockID);
            var sessionViewModel = session.ToViewModel(allInstructors,allHalls);
            //var sessionViewModel = new SessionViewModel { Instructors = PopulateInstructorData(), Halls = PopulateHallData() };
            return View(sessionViewModel);
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "SessionID,ActivitySubject,Year,Week_no,BlockID,Theme,Day,Date,StartTime,EndTime,ActivityT,StatusT,Descipline,Objectives")] Session session)
        public ActionResult Edit(SessionViewModel sessionViewModel)
        {
            if (ModelState.IsValid)
            {
                var originalsession = db.Sessions.Find(sessionViewModel.SessionID);
                AddOrUpdateInstructors(originalsession, sessionViewModel.Instructors);
                AddOrUpdateHalls(originalsession, sessionViewModel.Halls);
                //db.Entry(originalsession).State = EntityState.Modified;
                //db.Entry(originalUserProfile).CurrentValues.SetValues(userProfileViewModel);
                db.Entry(originalsession).CurrentValues.SetValues(sessionViewModel);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.BlockID = new SelectList(db.Blocks, "ID", "Name", sessionViewModel.BlockID);
            return View(sessionViewModel);
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

        public ActionResult UploadDocument()
        {
            var dir = new DirectoryInfo(Server.MapPath("~/Content/images/"));
            FileInfo[] fileNames = dir.GetFiles("*.*");
            //Directory.GetFiles("~/content/images/");
            List<string> items = new List<string>();

            foreach (var file in fileNames)
            {
                items.Add(file.Name);
            }

            return View(items);
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                file.SaveAs(path);
            }

            return RedirectToAction("UploadDocument");
        }

        /*public ActionResult List()
02
        {
03
            var uploadedFiles = new List<UploadedFile>();
04
 
05
            var files = Directory.GetFiles(Server.MapPath("~/UploadedFiles"));
06
 
07
            foreach(var file in files)
08
            {
09
                var fileInfo = new FileInfo(file);
10
 
11
                var uploadedFile = new UploadedFile() {Name = Path.GetFileName(file)};
12
                uploadedFile.Size = fileInfo.Length;
13
 
14
                uploadedFile.Path = ("~/UploadedFiles/") + Path.GetFileName(file);
15
                uploadedFiles.Add(uploadedFile);
16
            }
17
 
18
            return View(uploadedFiles);
19
        }
*/
    }
}
