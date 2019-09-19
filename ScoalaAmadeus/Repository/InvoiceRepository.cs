using ScoalaAmadeus.Models;
using ScoalaAmadeus.ViewModels;
using ScoalaAmadeus.Models.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoalaAmadeus.Repository
{
    public class InvoiceRepository
    {
        private CourseRepository courseRepository = new CourseRepository();

        private TeacherRepository teacherRepository = new TeacherRepository();

        private StudentRepository studentRepository = new StudentRepository();

        private Models.DBObjects.SchoolsModelsDataContext dbContext;

        public InvoiceRepository()
        {
            dbContext = new Models.DBObjects.SchoolsModelsDataContext();
        }
        public InvoiceRepository(Models.DBObjects.SchoolsModelsDataContext dataContext)
        {
            dbContext = dataContext;
        }
        public void Insert(InvoiceModel invoiceModel)
        {
            
            if (invoiceModel != null)
            {
                Models.DBObjects.Invoice dbInvoiceModel = new Models.DBObjects.Invoice();

                dbInvoiceModel.InvoiceId = invoiceModel.InvoiceId;
                dbInvoiceModel.Invoice_Series = invoiceModel.Invoice_Series;
                dbInvoiceModel.Create_Date = invoiceModel.Create_Date;
                dbInvoiceModel.Contractor = invoiceModel.Contractor;
                dbInvoiceModel.StudentId = invoiceModel.StudentId;
                dbInvoiceModel.Quantity = invoiceModel.Quantity;

                List<StudentModel> students = studentRepository.GetAllStudents();
                foreach (StudentModel student in students)
                {
                    if (dbInvoiceModel.StudentId == student.StudentId)
                    {
                        List<TeacherWithCourseNameViewModel> teachers = teacherRepository.GetAllTeachersWithCourseNames();
                        foreach (TeacherWithCourseNameViewModel teacher in teachers)
                        {
                            if (student.TeacherId == teacher.TeacherId)
                            {
                                dbInvoiceModel.StudentCourse = teacher.CourseName;

                                List<CourseModel> courses = courseRepository.GetAllCourses();
                                foreach (CourseModel course in courses)
                                {
                                    if (dbInvoiceModel.StudentCourse == course.Name)
                                    {
                                        dbInvoiceModel.StudentPrice = course.Price;
                                    }
                                }
                            }
                        }

                       
                    }
            
                }


                dbContext.Invoices.InsertOnSubmit(dbInvoiceModel);
                dbContext.SubmitChanges();
            }
        }
        public List<InvoiceModel> GetAllInvoices()
        {
            List<InvoiceModel> invoicesList = new List<InvoiceModel>();

            foreach (Models.DBObjects.Invoice dbInvoiceModel in dbContext.Invoices)
            {
                InvoiceModel invoiceModel = new InvoiceModel();

                invoiceModel.InvoiceId = dbInvoiceModel.InvoiceId;
                invoiceModel.Invoice_Series = dbInvoiceModel.Invoice_Series;
                invoiceModel.Create_Date = dbInvoiceModel.Create_Date;
                invoiceModel.Contractor = dbInvoiceModel.Contractor;
                invoiceModel.StudentId = dbInvoiceModel.StudentId;
                invoiceModel.Quantity = dbInvoiceModel.Quantity;
                invoiceModel.StudentCourse = dbInvoiceModel.StudentCourse;
                invoiceModel.StudentPrice = dbInvoiceModel.StudentPrice;

                invoicesList.Add(invoiceModel);
            }
            return invoicesList;
        }
        public List<InvoiceWithStudentNameViewModel> GetAllInvoicesWithStudentNames()
        {
            List<InvoiceWithStudentNameViewModel> invoicesList = new List<InvoiceWithStudentNameViewModel>();
            List<StudentModel> studentsList = studentRepository.GetAllStudents();

            foreach (Models.DBObjects.Invoice dbInvoiceModel in dbContext.Invoices)
            {
                InvoiceWithStudentNameViewModel invoiceModel = new InvoiceWithStudentNameViewModel();

                invoiceModel.InvoiceId = dbInvoiceModel.InvoiceId;
                invoiceModel.Invoice_Series = dbInvoiceModel.Invoice_Series;
                invoiceModel.Create_Date = dbInvoiceModel.Create_Date;
                invoiceModel.Contractor = dbInvoiceModel.Contractor;
                invoiceModel.StudentId = dbInvoiceModel.StudentId;
                invoiceModel.Quantity = dbInvoiceModel.Quantity;
                invoiceModel.StudentCourse = dbInvoiceModel.StudentCourse;
                invoiceModel.StudentPrice = dbInvoiceModel.StudentPrice;

                foreach (StudentModel student in studentsList)
                {
                    if (student.StudentId == dbInvoiceModel.StudentId)
                    {
                        invoiceModel.StudentName = student.Name;
                    }
                }


                invoicesList.Add(invoiceModel);
            }
            return invoicesList;
        }

        public InvoiceModel GetInvoiceById(int id)
        {
            Models.DBObjects.Invoice dbInvoiceModel = dbContext.Invoices.FirstOrDefault(m => m.InvoiceId == id);
            InvoiceModel invoiceModel = new InvoiceModel();

            if (dbInvoiceModel != null)
            {
                invoiceModel.InvoiceId = dbInvoiceModel.InvoiceId;
                invoiceModel.Invoice_Series = dbInvoiceModel.Invoice_Series;
                invoiceModel.Create_Date = dbInvoiceModel.Create_Date;
                invoiceModel.Contractor = dbInvoiceModel.Contractor;
                invoiceModel.StudentId = dbInvoiceModel.StudentId;
                invoiceModel.Quantity = dbInvoiceModel.Quantity;
                invoiceModel.StudentCourse = dbInvoiceModel.StudentCourse;
                invoiceModel.StudentPrice = dbInvoiceModel.StudentPrice;
            }
            return invoiceModel;
        }
        public void Update(InvoiceModel invoiceModel)
        {
            Models.DBObjects.Invoice dbInvoiceModel = dbContext.Invoices.FirstOrDefault(m => m.InvoiceId == invoiceModel.InvoiceId);

            if (invoiceModel != null)
            {
                dbInvoiceModel.InvoiceId = invoiceModel.InvoiceId;
                dbInvoiceModel.Invoice_Series = invoiceModel.Invoice_Series;
                dbInvoiceModel.Create_Date = invoiceModel.Create_Date;
                dbInvoiceModel.Contractor = invoiceModel.Contractor;
                dbInvoiceModel.StudentId = invoiceModel.StudentId;
                dbInvoiceModel.Quantity = invoiceModel.Quantity;
                dbInvoiceModel.StudentCourse = invoiceModel.StudentCourse;
                dbInvoiceModel.StudentPrice = invoiceModel.StudentPrice;

                dbContext.SubmitChanges();
            }

        }
        public void Delete(int id)
        {
            Models.DBObjects.Invoice dbInvoiceModel = dbContext.Invoices.FirstOrDefault(m => m.InvoiceId == id);

            dbContext.Invoices.DeleteOnSubmit(dbInvoiceModel);

            dbContext.SubmitChanges();
        }

        public List<InvoiceModel> GetAllInvoicesByStudentId(Guid id)
        {
            List<InvoiceModel> invoicesList = new List<InvoiceModel>();
            List<Invoice> invoices = dbContext.Invoices.Where(x => x.StudentId == id).ToList();

            foreach (Models.DBObjects.Invoice dbInvoiceModel in invoices)
            {
                
                    InvoiceModel invoiceModel = new InvoiceModel();

                    invoiceModel.InvoiceId = dbInvoiceModel.InvoiceId;
                    invoiceModel.Invoice_Series = dbInvoiceModel.Invoice_Series;
                    invoiceModel.Create_Date = dbInvoiceModel.Create_Date;
                    invoiceModel.Contractor = dbInvoiceModel.Contractor;
                    invoiceModel.StudentId = dbInvoiceModel.StudentId;
                    invoiceModel.Quantity = dbInvoiceModel.Quantity;
                invoiceModel.StudentCourse = dbInvoiceModel.StudentCourse;
                invoiceModel.StudentPrice = dbInvoiceModel.StudentPrice;

                invoicesList.Add(invoiceModel);
                            
            }
            return invoicesList;
        }

        public PreviewInvoiceViewModel GetInvoicePreviewById(int id)
        {
            PreviewInvoiceViewModel previewModel = new PreviewInvoiceViewModel();

            Invoice dbInvoiceModel = dbContext.Invoices.FirstOrDefault(m => m.InvoiceId == id);

            if (dbInvoiceModel != null)
            {
                previewModel.InvoiceId = dbInvoiceModel.InvoiceId;
                previewModel.Invoice_Series = dbInvoiceModel.Invoice_Series;
                previewModel.Date = dbInvoiceModel.Create_Date.Day.ToString() +"."+ dbInvoiceModel.Create_Date.Month.ToString() +"."+ dbInvoiceModel.Create_Date.Year.ToString();
                previewModel.Create_Date = dbInvoiceModel.Create_Date;
                previewModel.Contractor = dbInvoiceModel.Contractor;
                previewModel.StudentId = dbInvoiceModel.StudentId;
                previewModel.Quantity = dbInvoiceModel.Quantity;
                previewModel.StudentCourse = dbInvoiceModel.StudentCourse;
                previewModel.StudentPrice = dbInvoiceModel.StudentPrice;

                var students = studentRepository.GetAllStudents();
                foreach (var student in students)
                {
                    if (previewModel.StudentId == student.StudentId)
                    {
                        previewModel.StudentName = student.Name;
                        
                    }
                    
                }
                previewModel.Product = "Cursuri Muzica - " + dbInvoiceModel.StudentCourse;
                previewModel.Value = dbInvoiceModel.StudentPrice * dbInvoiceModel.Quantity;
                previewModel.Total = previewModel.Value;
            }
            return previewModel;
        }
    }
}