using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScoalaAmadeus.Models
{
    public class TeacherModel
    {
        public Guid TeacherId { get; set; }

        [Required(ErrorMessage ="Name is mandatory")]
        [StringLength(50, MinimumLength =3, ErrorMessage ="Length must be min. 3 char. and max. 50 char")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone is mandatory")]
        [StringLength(20, ErrorMessage ="Too long max. 20 char")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public Guid CourseId { get; set; }
    }
}