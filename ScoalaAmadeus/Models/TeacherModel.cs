using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoalaAmadeus.Models
{
    public class TeacherModel
    {
        public Guid TeacherId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Guid CourseId { get; set; }
    }
}