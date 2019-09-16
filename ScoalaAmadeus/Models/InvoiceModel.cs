using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScoalaAmadeus.Models
{
    public class InvoiceModel
    {
        public int InvoiceId { get; set; }

        [Required(ErrorMessage = "Series is mandatory")]
        [StringLength(10, MinimumLength =3, ErrorMessage = "Must be min. 3 char and max. 10 char")]
        [DataType(DataType.Text)]
        public string Invoice_Series { get; set; }

        [Required(ErrorMessage = "Date is mandatory")]
        [DataType(DataType.Date)]
        public DateTime Create_Date { get; set; }

        [Required(ErrorMessage = "Contractor is mandatory")]
        [StringLength(100, ErrorMessage ="Too long max. 100 char")]
        [DataType(DataType.Text)]
        public string Contractor { get; set; }

        public Guid StudentId { get; set; }

        [Required(ErrorMessage = "Course is mandatory")]
        [StringLength(50, ErrorMessage = "Too long max. 50 char")]
        [DataType(DataType.Text)]
        public string Course { get; set; }

        [Required(ErrorMessage = "Program is mandatory")]
        [StringLength(50, ErrorMessage = "Too long max. 50 char")]
        [DataType(DataType.Text)]
        public string Program { get; set; }
    }
}