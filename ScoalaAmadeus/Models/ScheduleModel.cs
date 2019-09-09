using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoalaAmadeus.Models
{
    public class ScheduleModel
    {
        public Guid ScheduleId { get; set; }
        public DateTime Day { get; set; }
        public Guid StudentId { get; set; }
        public Guid TeacherId { get; set; }
        public Guid ClassId { get; set; }
    }
}