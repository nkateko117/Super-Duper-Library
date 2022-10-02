using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Super_Duper_Library.Models;

namespace Super_Duper_Library.Models
{
    public class BooksVM
    {
        public Books book { get; set; }
        public Types type { get; set; }
        public Authors author { get; set; }
        public string status { get; set; }
/*
        public static List<Types> GetAllTypes()
        {
            List<Types> types = DBDataService.GetTypes().ToList();
            return types;
        }
        public static List<Authors> GetAllAuthors()
        {
            List<Authors> authors = DBDataService.GetAuthors().ToList();
            return authors;
        }
*/
}
}