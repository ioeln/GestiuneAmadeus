using ScoalaAmadeus.Models;
using ScoalaAmadeus.Models.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoalaAmadeus.Repository
{
    public class InvoiceRepository
    {
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
                   

                    invoicesList.Add(invoiceModel);
                            
            }
            return invoicesList;
        }
    }
}