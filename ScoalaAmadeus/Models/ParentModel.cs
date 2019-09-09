using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoalaAmadeus.Models
{
    public class ParentModel
    {
        public Guid ParentId { get; set; }
        public string Name { get; set; }
        public string Phone_Number { get; set; }
        public string Email { get; set; }
    }
}