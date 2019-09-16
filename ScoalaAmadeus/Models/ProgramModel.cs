using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScoalaAmadeus.Models
{
    public class ProgramModel
    {
        public Guid ProgramId { get; set; }

        [Required(ErrorMessage ="Name is mandatory")]
        [StringLength(50, MinimumLength =3,ErrorMessage ="length must be min. 3 char and max. 50 char")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage ="Mandatory field")]
        public int Hours_Mounth { get; set; }
    }
}