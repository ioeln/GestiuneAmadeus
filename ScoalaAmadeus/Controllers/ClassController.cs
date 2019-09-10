using ScoalaAmadeus.Models;
using ScoalaAmadeus.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoalaAmadeus.Controllers
{
    public class ClassController : Controller
    {
        private ClassRepository classRepository = new ClassRepository();
        
        // GET: Class
        public ActionResult Index()
        {
            List<ClassModel> classesList = new List<ClassModel>();
            classesList = classRepository.GetAllClasses();
            return View("Index", classesList);
        }

        // GET: Class/Details/5
        public ActionResult Details(Guid id)
        {
            ClassModel classModel = new ClassModel();
            classModel = classRepository.GetClassById(id);
            return View("Details", classModel);
        }

        // GET: Class/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Class/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                ClassModel classModel = new ClassModel();

                UpdateModel(classModel);

                classRepository.Insert(classModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Create");
            }
        }

        // GET: Class/Edit/5
        public ActionResult Edit(Guid id)
        {
            ClassModel classModel = classRepository.GetClassById(id);
            return View("Edit", classModel);
        }

        // POST: Class/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                ClassModel classModel = new ClassModel();

                UpdateModel(classModel);

                classRepository.Update(classModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Edit");
            }
        }

        // GET: Class/Delete/5
        public ActionResult Delete(Guid id)
        {
            ClassModel classModel = classRepository.GetClassById(id);
            return View("Delete", classModel);
        }

        // POST: Class/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                classRepository.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete");
            }
        }
    }
}
