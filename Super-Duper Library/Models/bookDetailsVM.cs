using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Super_Duper_Library.Models
{
    public class bookDetailsVM
    {
        public Books book { get; set; }
        public List<Borrows> borrows { get; set; }
        public Students student { get; set; }
        /*
        public static Students GetStudent(int borrowStudentid)
        {
            Students student1 = DBDataService.GetStudents().Where(a => a.studentID == borrowStudentid).FirstOrDefault();
            return student1;
        }
        */
    }
}