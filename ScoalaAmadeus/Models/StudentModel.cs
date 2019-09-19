using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScoalaAmadeus.Models
{

    public class StudentModel
    {
        public Guid StudentId { get; set; }

        [Required(ErrorMessage = "Name is mandatory")]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be min. 3 char and max. 50 char")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public int? Age { get; set; }

        [Required(ErrorMessage = "Address is mandatory")]
        [DataType(DataType.Text)]
        [StringLength(200, ErrorMessage = "Too long max. 200 char")]
        public string Address { get; set; }

        [Required(ErrorMessage = "E-mail is mandatory")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, ErrorMessage = "Too long max. 50 char")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is mandatory")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(20, ErrorMessage = "Too long max. 20 char")]
        public string Phone { get; set; }

        public Guid TeacherId { get; set; }

        public Guid ProgramId { get; set; }

        public Guid ParentId { get; set; }
    }
  
}