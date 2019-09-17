using ScoalaAmadeus.Models;
using ScoalaAmadeus.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoalaAmadeus.Controllers
{
    public class ProgramController : Controller
    {
        private InvoiceRepository invoiceRepository = new InvoiceRepository();

        private StudentRepository studentRepository = new StudentRepository();

        private ProgramRepository programRepository = new ProgramRepository();

        // GET: Program
        public ActionResult Index()
        {
            List<ProgramModel> programsList = new List<ProgramModel>();

            programsList = programRepository.GetAllPrograms();

            return View("Index", programsList);
        }

        // GET: Program/Details/5
        public ActionResult Details(Guid id)
        {
            ProgramModel programModel = programRepository.GetProgramById(id);
            return View("Details", programModel);
        }

        // GET: Program/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Program/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                ProgramModel programModel = new ProgramModel();

                UpdateModel(programModel);

                programRepository.Insert(programModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Create");
            }
        }

        // GET: Program/Edit/5
        public ActionResult Edit(Guid id)
        {
            ProgramModel programModel = programRepository.GetProgramById(id);
            return View("Edit", programModel);
        }

        // POST: Program/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                ProgramModel programToEdit = new ProgramModel();

                UpdateModel(programToEdit);

                programRepository.Update(programToEdit);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Edit");
            }
        }

        // GET: Program/Delete/5
        public ActionResult Delete(Guid id)
        {
            ProgramModel programModel = programRepository.GetProgramById(id);

            return View("Delete", programModel);
        }

        // POST: Program/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                List<StudentModel> studentsList = studentRepository.GetAllStudentsByProgramId(id);
                foreach (StudentModel student in studentsList)
                {
                    List<InvoiceModel> invoices = invoiceRepository.GetAllInvoicesByStudentId(student.StudentId);
                    foreach (InvoiceModel invoice in invoices)
                    {
                        invoiceRepository.Delete(invoice.InvoiceId);
                    }

                    studentRepository.Delete(student.StudentId);
                }

                programRepository.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete");
            }
        }
    }
}
