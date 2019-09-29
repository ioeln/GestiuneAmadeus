using ScoalaAmadeus.Models;
using ScoalaAmadeus.ViewModels;
using ScoalaAmadeus.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace ScoalaAmadeus.Controllers
{
    public class StudentController : Controller
    {
        private InvoiceRepository invoiceRepository = new InvoiceRepository();

        private StudentRepository studentRepository = new StudentRepository();

        // GET: Student
        public ActionResult Index()
        {

            List<StudentWithPropNamesViewModel> studentsList = studentRepository.GetAllStudentsWithPropNames();

            return View("Index", studentsList);
        }

        

        // GET: Student/Details/5
        public ActionResult Details(Guid id)
        {
            StudentModel studentModel = new StudentModel();

            studentModel = studentRepository.GetStudentById(id);

            return View("Details", studentModel);
        }

        private ProgramRepository programRepository = new ProgramRepository();
        private CourseRepository courseRepository = new CourseRepository();
        private TeacherRepository teacherRepository = new TeacherRepository();
        private ParentRepository parentRepository = new ParentRepository();

        // GET: Student/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var programs = programRepository.GetAllPrograms();
            SelectList programList = new SelectList(programs, "ProgramId", "Name");
            ViewData["program"] = programList;

            var teachers = teacherRepository.GetAllTeachers();
            SelectList teacherList = new SelectList(teachers, "TeacherId", "Name");
            ViewData["teacher"] = teacherList;

            var parents = parentRepository.GetAllParents();
            SelectList parentList = new SelectList(parents, "ParentId", "Name");
            ViewData["parent"] = parentList;

            return View("Create");
        }

        // POST: Student/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            

            try
            {
                // TODO: Add insert logic here
                StudentModel studentModel = new StudentModel();

                UpdateModel(studentModel);

                studentRepository.Insert(studentModel);
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Create");
            }

            
        }

        // GET: Student/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid id)
        {
            StudentModel studentModel = studentRepository.GetStudentById(id);

            return View("Edit", studentModel);
        }

        // POST: Student/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                StudentModel studentModel = new StudentModel();

                UpdateModel(studentModel);

                studentRepository.Update(studentModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Edit");
            }
        }

        // GET: Student/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id)
        {
            StudentModel studentModel = studentRepository.GetStudentById(id);

            return View("Delete", studentModel);
        }

        // POST: Student/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                List<InvoiceModel> invoicesList = invoiceRepository.GetAllInvoicesByStudentId(id);

                foreach (InvoiceModel invoice in invoicesList)
                {
                    invoiceRepository.Delete(invoice.InvoiceId);
                }

                studentRepository.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete");
            }
        }
    }
}
