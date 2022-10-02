using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Super_Duper_Library.Models
{
    public class Books
    {
        public int bookID { get; set; }
        public string name { get; set; }
        public int pageCount { get; set; }
        public int point { get; set; }
        public int authorID { get; set; }
        public int typeID { get; set; }
        public string status { get; set; }
    }
}