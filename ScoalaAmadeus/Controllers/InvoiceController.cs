using ScoalaAmadeus.Models;
using ScoalaAmadeus.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoalaAmadeus.Controllers
{
    public class InvoiceController : Controller
    {
        private InvoiceRepository invoiceRepository = new InvoiceRepository();
        // GET: Invoice
        public ActionResult Index()
        {
            List<InvoiceModel> invoicesList = invoiceRepository.GetAllInvoices();
            return View("Index", invoicesList);
        }

        // GET: Invoice/Details/5
        public ActionResult Details(int id)
        {
            InvoiceModel invoiceModel = invoiceRepository.GetInvoiceById(id);
            return View("Details", invoiceModel);
        }

        private StudentRepository studentRepository = new StudentRepository();
        private CourseRepository courseRepository = new CourseRepository();
        private ProgramRepository programRepository = new ProgramRepository();

        // GET: Invoice/Create
        public ActionResult Create()
        {
            var students = studentRepository.GetAllStudents();
            SelectList studentList = new SelectList(students, "StudentId", "Name");
            ViewData["student2"] = studentList;


            return View("Create");
        }

        // POST: Invoice/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                InvoiceModel invoiceModel = new InvoiceModel();

                UpdateModel(invoiceModel);

                invoiceRepository.Insert(invoiceModel);

                return RedirectToAction("Index");
            }
            catch 
            {               
                return View("Create");
            }
        }

        // GET: Invoice/Edit/5
        public ActionResult Edit(int id)
        {
            InvoiceModel invoiceModel = invoiceRepository.GetInvoiceById(id);
            return View("Edit", invoiceModel);
        }

        // POST: Invoice/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                InvoiceModel invoiceModel = new InvoiceModel();

                UpdateModel(invoiceModel);

                invoiceRepository.Update(invoiceModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Edit");
            }
        }

        // GET: Invoice/Delete/5
        public ActionResult Delete(int id)
        {
            InvoiceModel invoiceModel = invoiceRepository.GetInvoiceById(id);
            return View("Delete", invoiceModel);
        }

        // POST: Invoice/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                invoiceRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete");
            }
        }
    }
}
