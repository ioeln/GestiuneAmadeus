using ScoalaAmadeus.Models;
using ScoalaAmadeus.ViewModels;
using ScoalaAmadeus.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoalaAmadeus.Controllers
{
    public class TeacherController : Controller
    {
        private InvoiceRepository invoiceRepository = new InvoiceRepository();

        private StudentRepository studentRepository = new StudentRepository();

        private TeacherRepository teacherRepository = new TeacherRepository();

        private CourseRepository courseRepository = new CourseRepository();
        // GET: Teacher
        public ActionResult Index()
        {
            List<TeacherModel> teachersList = new List<TeacherModel>();

            teachersList = teacherRepository.GetAllTeachers();

            return View("Index", teachersList);
        }

        public ActionResult TeacherWithCourseName()
        {
            List<TeacherWithCourseNameViewModel> teachersList = new List<TeacherWithCourseNameViewModel>();

            teachersList = teacherRepository.GetAllTeachersWithCourseNames();

            return View("TeacherWithCourseName", teachersList);
        }

        // GET: Teacher/Details/5
        public ActionResult Details(Guid id)
        {
            TeacherModel teacherModel = teacherRepository.GetTeacherById(id);

            return View("Details", teacherModel);
        }

        // GET: Teacher/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var courses = courseRepository.GetAllCourses();
            SelectList lst = new SelectList(courses, "CourseId", "Name");
            ViewData["course"] = lst;

            return View("Create");
        }

        // POST: Teacher/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                TeacherModel teacherModel = new TeacherModel();

                UpdateModel(teacherModel);

                teacherRepository.InsertTeacher(teacherModel);

                return RedirectToAction("TeacherWithCourseName");
            }
            catch
            {
                return View("Create");
            }
        }

        // GET: Teacher/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid id)
        {
            TeacherModel teacherModel = teacherRepository.GetTeacherById(id);

            return View("Edit", teacherModel);
        }

        // POST: Teacher/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                TeacherModel teacherModel = new TeacherModel();

                UpdateModel(teacherModel);

                teacherRepository.UpdateTeacher(teacherModel);

                return RedirectToAction("TeacherWithCourseName");
            }
            catch
            {
                return View("Edit");
            }
        }

        // GET: Teacher/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id)
        {
            TeacherModel teacherModel = teacherRepository.GetTeacherById(id);

            return View("Delete", teacherModel);
        }

        // POST: Teacher/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
               
                List<StudentModel> studentsList = studentRepository.GetAllStudentsByTeacherId(id);
                foreach (StudentModel student in studentsList)
                {
                    List<InvoiceModel> invoicesList = invoiceRepository.GetAllInvoicesByStudentId(student.StudentId);
                    foreach (InvoiceModel invoice in invoicesList)
                    {
                        invoiceRepository.Delete(invoice.InvoiceId);
                    }
                    

                    studentRepository.Delete(student.StudentId);
                }

                teacherRepository.DeleteTeacher(id);

                return RedirectToAction("TeacherWithCourseName");
            }
            catch
            {
                return View("Delete");
            }
        }
        [HttpGet]
        public ActionResult StudentsByTeacherId(Guid Id)
        {
            List<StudentWithPropNamesViewModel> studentsByTeacherIdList = studentRepository.GetStudentsByTeacherId(Id);

            return View("StudentsByTeacherId",  studentsByTeacherIdList);
        }
    }
}
