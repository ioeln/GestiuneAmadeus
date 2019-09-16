using ScoalaAmadeus.Models.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoalaAmadeus.ViewModels
{
    public class TeacherWithCourseNameViewModel:Teacher
    {
        public string CourseName { get; set; }
    }
}