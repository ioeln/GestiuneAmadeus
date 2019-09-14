using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScoalaAmadeus.Models
{
    public class CourseModel
    {
        
        public Guid CourseId { get; set; }

        [Required(ErrorMessage ="Mandatory Field")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage ="Name must be min. 3 char and max. 50 char")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage ="Mandatory Field")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}