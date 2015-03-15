using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMS.Models;
using CMS.CustomContext;
using CMS.ViewModels;
using CMS.AssignedDataM2M;

namespace CMS.Controllers
{
    public class CoursesController : Controller
    {
        private CourseContext db = new CourseContext();

        // GET: Courses
        public ActionResult Index()
        {
            return View(db.Courses.ToList());
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
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

                    Assigned = false
                });
            }
            return assignedInstructors;
        }

        private void AddOrUpdateInstructors(Course course, IEnumerable<AssignedInstructorData> assignedInstructors)
        {
            if (assignedInstructors == null) return;


            if (course.CourseID != 0)
            {
                foreach (var instructor in course.Instructors.ToList())
                {
                    course.Instructors.Remove(instructor);
                }

                foreach (var instructor in assignedInstructors.Where(w => w.Assigned))
                {
                    course.Instructors.Add(db.Instructors.Find(instructor.InstructorID));
                }
            }
            else
            {
                foreach (var assignedInstructor in assignedInstructors.Where(w => w.Assigned))
                {
                    var instructor = new Instructor {  InstructorID = assignedInstructor.InstructorID };
                    db.Instructors.Attach(instructor);
                    course.Instructors.Add(instructor);
                }
            }


        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            var courseViewModel = new CourseViewModel { Instructors = PopulateInstructorData() };
            return View(courseViewModel);
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "CourseID,CourseName,CourseCode")] Course course)
        public ActionResult Create(CourseViewModel courseViewModel)
        {

            if (ModelState.IsValid)
            {
                var course = new Course
                {
                    CourseID = courseViewModel.CourseID,
                    CourseName = courseViewModel.CourseName,
                    CourseCode = courseViewModel.CourseCode,

                };

                AddOrUpdateInstructors(course, courseViewModel.Instructors);
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(courseViewModel);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            var allInstructors = db.Instructors.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Include("Instructors").FirstOrDefault(x => x.CourseID == id);
            if (course == null)
            {
                return HttpNotFound();
            }
            var courseViewModel = course.ToCViewModel(allInstructors);
            return View(courseViewModel);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "CourseID,CourseName,CourseCode")] Course course)
        public ActionResult Edit(CourseViewModel courseViewModel)
        {
            if (ModelState.IsValid)
            {
                var originalCourse = db.Courses.Find(courseViewModel.CourseID);
                AddOrUpdateInstructors(originalCourse, courseViewModel.Instructors);
                //db.Entry(course).State = EntityState.Modified;
                db.Entry(originalCourse).CurrentValues.SetValues(courseViewModel);
                db.SaveChanges(); 

                return RedirectToAction("Index");
            }
            return View(courseViewModel);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
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
