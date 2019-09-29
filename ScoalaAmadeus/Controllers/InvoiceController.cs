using ScoalaAmadeus.Models;
using ScoalaAmadeus.ViewModels;
using ScoalaAmadeus.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Rotativa;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;

namespace ScoalaAmadeus.Controllers
{
    
    public class InvoiceController : Controller
    {
        private InvoiceRepository invoiceRepository = new InvoiceRepository();

        // GET: Invoice
        public ActionResult Index()
        {
            List<InvoiceWithStudentNameViewModel> invoicesList = invoiceRepository.GetAllInvoicesWithStudentNames();

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

        // GET: Invoice/Creates
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var students = studentRepository.GetAllStudents();
            SelectList studentList = new SelectList(students, "StudentId", "Name");
            ViewData["student2"] = studentList;


            return View("Create");
        }

        // POST: Invoice/Create
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            InvoiceModel invoiceModel = invoiceRepository.GetInvoiceById(id);

            return View("Edit", invoiceModel);
        }

        // POST: Invoice/Edit/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            InvoiceModel invoiceModel = invoiceRepository.GetInvoiceById(id);
            return View("Delete", invoiceModel);
        }

        // POST: Invoice/Delete/5
        [Authorize(Roles = "Admin")]
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
        public ActionResult Preview(int id)
        {
            PreviewInvoiceViewModel previewModel = invoiceRepository.GetInvoicePreviewById(id);

            return View("Preview", previewModel);
        }

        public ActionResult Export(int id)
        {
            PreviewInvoiceViewModel previewModel = invoiceRepository.GetInvoicePreviewById(id);

            return View("Export", previewModel);
        }

        public ActionResult ExportPdf(int id)
        {
            PreviewInvoiceViewModel exportModel = invoiceRepository.GetInvoicePreviewById(id);

            return new ActionAsPdf("Export",  new { id = exportModel.InvoiceId })
            {
                FileName = Server.MapPath("~/Content/Invoice.pdf")
            };
          
        }

        public ActionResult SendEmail()
        {

            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(string receiver, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("scoala.conta123@gmail.com", "emailScoala");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "scoala#123456";
                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View("SendEmail");
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return View();
        }
    }
}
