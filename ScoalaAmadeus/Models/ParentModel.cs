using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScoalaAmadeus.Models
{
    public class ParentModel
    {
        public Guid ParentId { get; set; }

        [Required(ErrorMessage = "Name is mandatory")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Name must be min.3 char and max. 50 char")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage ="Phone is mandatory")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(50, ErrorMessage ="Too long max. 50 char")]
        public string Phone { get; set; }

        [Required(ErrorMessage ="E-mail is mandatory")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, ErrorMessage ="Too long max. 50 char")]
        public string Email { get; set; }
    }
}