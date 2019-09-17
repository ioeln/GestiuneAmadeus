using ScoalaAmadeus.Models;
using ScoalaAmadeus.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoalaAmadeus.Controllers
{
    public class ParentController : Controller
    {
        private InvoiceRepository invoiceRepository = new InvoiceRepository();

        private StudentRepository studentRepository = new StudentRepository();

        private ParentRepository parentRepository = new ParentRepository();
        // GET: Parent
        public ActionResult Index()
        {
            List<ParentModel> parentsList = new List<ParentModel>();

            parentsList = parentRepository.GetAllParents();

            return View("Index", parentsList);
        }

        // GET: Parent/Details/5
        public ActionResult Details(Guid id)
        {
            ParentModel parentModel = parentRepository.GetParentById(id);
            return View("Details", parentModel);
        }

        // GET: Parent/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Parent/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                ParentModel parentModel = new ParentModel();

                UpdateModel(parentModel);

                parentRepository.Insert(parentModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Create");
            }
        }

        // GET: Parent/Edit/5
        public ActionResult Edit(Guid id)
        {
            ParentModel parentModel = parentRepository.GetParentById(id);

            return View("Edit", parentModel);
        }

        // POST: Parent/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                ParentModel parentModel = new ParentModel();

                UpdateModel(parentModel);

                parentRepository.Update(parentModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Edit");
            }
        }

        // GET: Parent/Delete/5
        public ActionResult Delete(Guid id)
        {
            ParentModel parentModel = parentRepository.GetParentById(id);

            return View("Delete", parentModel);
        }

        // POST: Parent/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                List<StudentModel> studentsList = studentRepository.GetAllStudentsByParentId(id);
                foreach (StudentModel student in studentsList)
                {
                    List<InvoiceModel> invoices = invoiceRepository.GetAllInvoicesByStudentId(student.StudentId);
                    foreach (InvoiceModel invoice in invoices)
                    {
                        invoiceRepository.Delete(invoice.InvoiceId);
                    }

                    studentRepository.Delete(student.StudentId);
                }

                parentRepository.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete");
            }
        }
    }
}
