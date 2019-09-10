using ScoalaAmadeus.Models;
using ScoalaAmadeus.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoalaAmadeus.Controllers
{
    public class ScheduleController : Controller
    {
        private ScheduleRepository scheduleRepository = new ScheduleRepository();
        // GET: Schedule
        public ActionResult Index()
        {
            List<ScheduleModel> schedulesList = new List<ScheduleModel>();
            schedulesList = scheduleRepository.GetAllSchedules();
            return View("Index", schedulesList);
        }

        // GET: Schedule/Details/5
        public ActionResult Details(Guid id)
        {
            ScheduleModel scheduleModel = new ScheduleModel();
            scheduleModel = scheduleRepository.GetScheduleById(id);
            return View("Details", scheduleModel);
        }

        private StudentRepository studentRepository = new StudentRepository();
        private TeacherRepository teacherRepository = new TeacherRepository();
        private ClassRepository classRepository = new ClassRepository();

        // GET: Schedule/Create
        public ActionResult Create()
        {
            var students = studentRepository.GetAllStudents();
            SelectList studentList = new SelectList(students, "StudentId", "Name");
            ViewData["student1"] = studentList;

            var teachers = teacherRepository.GetAllTeachers();
            SelectList teacherList = new SelectList(teachers, "TeacherId", "Name");
            ViewData["teacher1"] = teacherList;

            var classes = classRepository.GetAllClasses();
            SelectList classList = new SelectList(classes, "ClassId", "Name");
            ViewData["class1"] = classList;

            return View("Create");
        }

      
        // POST: Schedule/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                ScheduleModel scheduleModel = new ScheduleModel();

                UpdateModel(scheduleModel);

                scheduleRepository.Insert(scheduleModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Create");
            }
        }

        // GET: Schedule/Edit/5
        public ActionResult Edit(Guid id)
        {
            ScheduleModel scheduleModel = new ScheduleModel();
            scheduleModel = scheduleRepository.GetScheduleById(id);
            return View("Edit", scheduleModel);
        }

        // POST: Schedule/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                ScheduleModel scheduleModel = new ScheduleModel();

                UpdateModel(scheduleModel);

                scheduleRepository.Update(scheduleModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Edit");
            }
        }

        // GET: Schedule/Delete/5
        public ActionResult Delete(Guid id)
        {
            ScheduleModel scheduleModel = new ScheduleModel();
            scheduleModel = scheduleRepository.GetScheduleById(id);
            return View("Delete", scheduleModel);
        }

        // POST: Schedule/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                scheduleRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete");
            }
        }
    }
}
