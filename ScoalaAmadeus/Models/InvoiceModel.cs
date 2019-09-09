using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoalaAmadeus.Models
{
    public class InvoiceModel
    {
        public int InvoiceId { get; set; }
        public string Invoice_Series { get; set; }
        public DateTime Create_Date { get; set; }
        public string Contractor { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public Guid ProgramId { get; set; }
    }
}