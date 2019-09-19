using ScoalaAmadeus.Models;
using ScoalaAmadeus.Models.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoalaAmadeus.ViewModels
{
    public class PreviewInvoiceViewModel:Invoice 
    {
        public string StudentName { get; set;}

        public string Date { get; set; }

        public string Product { get; set; }

        public decimal Value { get; set; }

        public decimal Total { get; set; }

        public PreviewInvoiceViewModel()
        {
            //Value = StudentPrice * Quantity ;

            //Total = Value;

            //Product = "Cursuri Muz - ";
        }
    }
}