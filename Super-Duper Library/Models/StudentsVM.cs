using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Super_Duper_Library.Models
{
    public class StudentsVM
    {
        public Books book { get; set; }
        public List<Students> students { get; set; }
        
    }
}