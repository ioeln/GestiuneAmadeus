﻿using ScoalaAmadeus.Models;
using ScoalaAmadeus.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoalaAmadeus.Controllers
{
    public class StudentController : Controller
    {
        private StudentRepository studentRepository = new StudentRepository();

        // GET: Student
        public ActionResult Index()
        {
            List<StudentModel> studentsList = new List<StudentModel>();

            studentsList = studentRepository.GetAllStudents();

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
        public ActionResult Create()
        {
            var programs = programRepository.GetAllPrograms();
            SelectList programList = new SelectList(programs, "ProgramId", "Name");
            ViewData["program"] = programList;

            var courses = courseRepository.GetAllCourses();
            SelectList courseList = new SelectList(courses, "CourseId", "Name");
            ViewData["course"] = courseList;

            var teachers = teacherRepository.GetAllTeachers();
            SelectList teacherList = new SelectList(teachers, "TeacherId", "Name");
            ViewData["teacher"] = teacherList;

            var parents = parentRepository.GetAllParents();
            SelectList parentList = new SelectList(parents, "ParentId", "Name");
            ViewData["parent"] = parentList;

            return View("Create");
        }

        // POST: Student/Create
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
        public ActionResult Edit(Guid id)
        {
            StudentModel studentModel = studentRepository.GetStudentById(id);

            return View("Edit", studentModel);
        }

        // POST: Student/Edit/5
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
        public ActionResult Delete(Guid id)
        {
            StudentModel studentModel = studentRepository.GetStudentById(id);

            return View("Delete", studentModel);
        }

        // POST: Student/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
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
