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
using System.IO;
using System.IO.Compression;

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
            
            string invoiceFileName = "";
            PreviewInvoiceViewModel exportModel = invoiceRepository.GetInvoicePreviewById(id);

            var invoice = invoiceRepository.GetInvoiceById(id); 

            var students = studentRepository.GetAllStudents();
            foreach (var student in students)
            {
                if (student.StudentId == invoice.StudentId)
                {
                    invoiceFileName = student.Name;
                }
            }

            return new ActionAsPdf("Export", new { id = exportModel.InvoiceId })
            {
                FileName = $"F-{invoice.InvoiceId}_{invoiceFileName}.pdf",
                PageSize = Rotativa.Options.Size.A4,
                PageOrientation = Rotativa.Options.Orientation.Portrait,
                PageMargins = { Left = 1, Right = 1 }
            };
        }

        public ActionResult SaveInvoice(int id)
        {
            string invoiceFileName = "";
            PreviewInvoiceViewModel exportModel = invoiceRepository.GetInvoicePreviewById(id);

            var invoice = invoiceRepository.GetInvoiceById(id);

            var students = studentRepository.GetAllStudents();
            foreach (var student in students)
            {
                if (student.StudentId == invoice.StudentId)
                {
                    invoiceFileName = student.Name;
                }
            }
            try
            {
                SaveHttpResponseAsFile("http://localhost:49786/Invoice/ExportPdf/?id=" + id, $"C:\\TEMP\\F-{invoice.InvoiceId}_{invoiceFileName}.pdf");
                return RedirectToAction("Index");
            }
            catch
            {
                return View("SaveInvoice");
               
            }
        }
        // PDF generator for email
        public static void SaveHttpResponseAsFile(string RequestUrl, string FilePath)
        {
            try
            {
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(RequestUrl);
                httpRequest.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows 10 Pro; Trident/5.0)";
                httpRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
                HttpWebResponse response = null;
                try
                {
                    response = (HttpWebResponse)httpRequest.GetResponse();
                }
                catch (System.Net.WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                        response = (HttpWebResponse)ex.Response;
                }

                using (Stream responseStream = response.GetResponseStream())
                {
                    Stream FinalStream = responseStream;
                    if (response.ContentEncoding.ToLower().Contains("gzip"))
                        FinalStream = new GZipStream(FinalStream, CompressionMode.Decompress);
                    else if (response.ContentEncoding.ToLower().Contains("deflate"))
                        FinalStream = new DeflateStream(FinalStream, CompressionMode.Decompress);

                    using (var fileStream = System.IO.File.Create(FilePath))
                    {
                        FinalStream.CopyTo(fileStream);
                    }

                    response.Close();
                    FinalStream.Close();
                }
            }
            catch
            { }
        }

        

        public Byte[] SaveInvoiceMail(int id)
        {
            string invoiceFileName = "";
            PreviewInvoiceViewModel exportModel = invoiceRepository.GetInvoicePreviewById(id);

            var invoice = invoiceRepository.GetInvoiceById(id);

            var students = studentRepository.GetAllStudents();
            foreach (var student in students)
            {
                if (student.StudentId == invoice.StudentId)
                {
                    invoiceFileName = student.Name;
                }
            }
           
            var pdf = new ActionAsPdf("Export", new { id = exportModel.InvoiceId })
            {
                FileName = $"F-{invoice.InvoiceId}_{invoiceFileName}.pdf",
            };

            Byte[] pdfData = pdf.BuildPdf(ControllerContext);
            return pdfData;
        }


        public ActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(string receiver, string subject, string message)
        {
            //string receiver = "ioelnechita@ymail.com";
            //string subject = "Test";
            //string message = "Acesta este mesajul testului cu pdf attach";
            int id = 32;
            string invoiceFileName = "";
            var invoice = invoiceRepository.GetInvoiceById(id);

            var students = studentRepository.GetAllStudents();
            foreach (var student in students)
            {
                if (student.StudentId == invoice.StudentId)
                {
                    invoiceFileName = student.Name;
                }
            }
            try
            {
                

                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("scoala.conta123@gmail.com", "emailScoala");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "scoala#123456";
                    var sub = subject;
                    var body = message;
                    MemoryStream stream = new MemoryStream(SaveInvoiceMail(id));
                    Attachment att1 = new Attachment(stream, $"F-{invoice.InvoiceId}_{invoiceFileName}.pdf", "application/pdf");
                    


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
                        Body = body,
                        //Attachments = new AttachmentCollection().Add(att1);
                      


                        //Attachments = 
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
