using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Super_Duper_Library.Models
{
    public class Borrows
    {
        public int borrowsID { get; set; }
        public int studentID { get; set; }
        public int bookID { get; set; }
        public DateTime takeDate { get; set; }
        public DateTime broughtDate { get; set; }

    }
}