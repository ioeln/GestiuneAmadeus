using ScoalaAmadeus.Models;
using ScoalaAmadeus.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoalaAmadeus.Controllers
{
    public class CourseController : Controller
    {
        private CourseRepository courseRepository = new CourseRepository();

        // GET: Course
        public ActionResult Index()
        {
            List<CourseModel> coursesList = new List<CourseModel>();

            coursesList = courseRepository.GetAllCourses();

            return View("Index", coursesList);
        }

        // GET: Course/Details/5
        public ActionResult Details(Guid id)
        {
            CourseModel courseModel = new CourseModel();

            courseModel = courseRepository.GetCourseById(id);

            return View("Details", courseModel);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Course/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Models.DBObjects.Course courseModel = new Models.DBObjects.Course();

                UpdateModel(courseModel);

                courseRepository.InsertCourse(courseModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Create");
            }
        }

        // GET: Course/Edit/5
        public ActionResult Edit(Guid id)
        {
            CourseModel courseToEdit = courseRepository.GetCourseById(id);

            return View("Edit", courseToEdit);
        }

        // POST: Course/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                CourseModel courseModel = new CourseModel();
                
                UpdateModel(courseModel);

                courseRepository.UpdateCourse(courseModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Edit");
            }
        }

        // GET: Course/Delete/5
        public ActionResult Delete(Guid id)
        {
            CourseModel courseToDelete = courseRepository.GetCourseById(id);

            return View("Delete", courseToDelete);
        }

        // POST: Course/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                courseRepository.DeleteCourse(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete");
            }
        }
    }
}
