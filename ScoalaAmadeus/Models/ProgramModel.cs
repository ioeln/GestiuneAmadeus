using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoalaAmadeus.Models
{
    public class ProgramModel
    {
        public Guid ProgramId { get; set; }
        public string Name { get; set; }
        public int Hours_Mounth { get; set; }
    }
}