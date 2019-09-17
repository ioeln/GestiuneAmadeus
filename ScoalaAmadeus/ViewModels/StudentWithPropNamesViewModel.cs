using ScoalaAmadeus.Models.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoalaAmadeus.ViewModels
{
    public class StudentWithPropNamesViewModel:Student
    {
        public string ProgramName { get; set; }
        public string TeacherName { get; set; }
        public string CourseName { get; set; }
        public string ParentName { get; set; }
    }
}