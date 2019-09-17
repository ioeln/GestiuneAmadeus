using ScoalaAmadeus.Models.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoalaAmadeus.ViewModels
{
    public class InvoiceWithStudentNameViewModel:Invoice
    {
        public string StudentName { get; set; }
    }
}