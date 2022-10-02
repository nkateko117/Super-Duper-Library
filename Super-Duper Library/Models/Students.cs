using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Super_Duper_Library.Models
{
    public class Students
    {
       public int studentID { get; set; }
       public string name { get; set; }
       public string surname { get; set; }
       public DateTime birthdate { get; set; }
       public char gender { get; set; }
       public string Class { get; set; }
       public int point { get; set; }
       
    }
}