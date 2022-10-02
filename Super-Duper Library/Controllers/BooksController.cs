using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Super_Duper_Library.Models;

namespace Super_Duper_Library.Controllers
{
    public class BooksController : Controller
    {
        /*............................................................................................................................................................................*/
        // GET: Books
        //Home Page
        public ActionResult Books(string bookName, string typeName, string authorName)
        {

            List<BooksVM> booksvm = new List<BooksVM>(); //I am gonna return my Book VM as a list
            List<Books> books = DBDataService.GetBooks();
            List<Types> types = DBDataService.GetTypes();
            List<Authors> authors = DBDataService.GetAuthors();

            //I used this method to reduce code repeatation
            //Every possible search combination call this method
            BooksVM makeBooksVM(Books books1)
            {
                BooksVM bookvm = new BooksVM
                {
                    book = books1,
                    author = DBDataService.GetAuthors().Where(a => a.authorID == books1.authorID).FirstOrDefault(),
                    type = DBDataService.GetTypes().Where(a => a.typeID == books1.typeID).FirstOrDefault(),
                    status = DBDataService.GetStatus(books1.bookID)
                };
                return bookvm;
            }

            //There's 8 possible ways the user can fill the search screen, so there should be 8 if statements
            //Defaul if statement
            if (bookName == null && typeName == null && authorName == null || bookName.Length==0 && typeName.Length==0 && authorName.Length==0)
            {
                foreach (var book in books)
                {
                    booksvm.Add(makeBooksVM(book));
                }
            }

            //Second IF statement
            else if (bookName.Length > 0 && typeName.Length == 0 && authorName.Length == 0)
            {
                List<Books> books1 = books.Where(a => a.name.Contains(bookName)).ToList(); //reading from the local list instead of the Model function
                foreach (var bookz in books1) //for all the books that contain the passed string
                {
                    booksvm.Add(makeBooksVM(bookz));
                }

            }

            //Third If statement
            else if (bookName.Length > 0 && typeName.Length > 0 && authorName.Length == 0)
            {
                Types types1 = types.Where(a => a.name == typeName).FirstOrDefault();
                List<Books> books1 = books.Where(a => a.typeID == types1.typeID).Where(b => b.name.Contains(bookName)).ToList();
                foreach (var book1 in books1)
                {
                    booksvm.Add(makeBooksVM(book1));
                }

            }

            //Fourth Possible Search
            else if (bookName.Length > 0 && typeName.Length > 0 && authorName.Length > 0)
            {
                Types types1 = types.Where(a => a.name == typeName).FirstOrDefault();
                Authors authors1 = authors.Where(a => a.surname == authorName).FirstOrDefault();
                List<Books> books1 = books.Where(a => a.typeID == types1.typeID).Where(b => b.authorID == authors1.authorID).Where(c => c.name.Contains(bookName)).ToList();
                foreach (var book1 in books1)
                {
                    booksvm.Add(makeBooksVM(book1));
                }
            }

            //Fifth Possible Search
            else if (bookName.Length == 0 && typeName.Length > 0 && authorName.Length > 0)
            {
                Types types1 = types.Where(a => a.name == typeName).FirstOrDefault();
                Authors authors1 = authors.Where(a => a.surname == authorName).FirstOrDefault();
                List<Books> books1 = books.Where(a => a.typeID == types1.typeID).Where(b => b.authorID == authors1.authorID).ToList();
                foreach (var book1 in books1)
                {
                    booksvm.Add(makeBooksVM(book1));
                }
            }

            //Sixth possible search
            else if (bookName.Length == 0 && typeName.Length == 0 && authorName.Length > 0)
            {
                Authors authors1 = authors.Where(a => a.surname == authorName).FirstOrDefault();
                List<Books> books1 = books.Where(b => b.authorID == authors1.authorID).ToList();
                foreach (var book1 in books1)
                {
                    booksvm.Add(makeBooksVM(book1));
                }
            }

            //Seventh possible search
            else if (bookName.Length == 0 && typeName.Length > 0 && authorName.Length == 0)
            {
                Types types1 = types.Where(a => a.name == typeName).FirstOrDefault();
                List<Books> books1 = books.Where(a => a.typeID == types1.typeID).ToList();
                foreach (var book1 in books1)
                {
                    booksvm.Add(makeBooksVM(book1));
                }
            }

            //Eight Possible search
            else if (bookName.Length > 0 && typeName.Length == 0 && authorName.Length > 0)
            {
                Authors authors1 = authors.Where(a => a.surname == authorName).FirstOrDefault();
                List<Books> books1 = books.Where(b => b.authorID == authors1.authorID).ToList();
                foreach (var book1 in books1)
                {
                    booksvm.Add(makeBooksVM(book1));
                }
            }

            return View(booksvm);
        }

        /*............................................................................................................................................................................*/

        //Clear Book Search
        public ActionResult homeClear() 
        {
            return RedirectToAction("Books");
        }

        /*............................................................................................................................................................................*/

        //Book Details Page
        public ActionResult BookDetails(int bookID)
        {

            bookDetailsVM booksDetailsVM = new bookDetailsVM //I am passing a single view model to the view
            {
                book = DBDataService.GetBooks().Where(a => a.bookID == bookID).FirstOrDefault(),
                borrows = DBDataService.GetBorrows().Where(a => a.bookID == bookID).ToList(),

            };

            //Book Status displayed next to the book name
            if (DBDataService.GetStatus(bookID) == "Out")
            {
                ViewBag.Message = "Book Out";
            }

            else if (DBDataService.GetStatus(bookID) == "Available")
            {
                ViewBag.Message = "Book Available";
            }

            return View(booksDetailsVM);
        }
        /*............................................................................................................................................................................*/

        //View Students Page
        public ActionResult ViewStudents(string studentName, string className, int bookID)
        {
            List<Students> students = DBDataService.GetStudents();
            Books book1 = DBDataService.GetBooks().Where(a => a.bookID == bookID).FirstOrDefault();
            StudentsVM studentsVM1 = null;

            if (studentName == null && className == null || studentName.Length==0 && className.Length==0)
            {
                studentsVM1 = new StudentsVM
                {
                    book = book1,
                    students = DBDataService.GetStudents()
                };
            }

            else if (studentName.Length > 0 && className.Length == 0)
            {
                studentsVM1 = new StudentsVM
                {
                    book = book1,
                    students = DBDataService.GetStudents().Where(a => a.name.Contains(studentName) || a.surname.Contains(studentName)).ToList()
                };
            }

            else if (studentName.Length == 0 && className.Length > 0)
            {
                studentsVM1 = new StudentsVM
                {
                    book = book1,
                    students = DBDataService.GetStudents().Where(a => a.Class == className).ToList()
                };
            }

            else if (studentName.Length > 0 && className.Length > 0)
            {
                studentsVM1 = new StudentsVM
                {
                    book = book1,
                    students = DBDataService.GetStudents().Where(a => a.Class == className).Where(a => a.name.Contains(studentName) || a.surname.Contains(studentName)).ToList()
                };
            }
            return View(studentsVM1);
        }

        /*............................................................................................................................................................................*/

        //Clear Student Search
        public ActionResult studentClear(int bookID)
        {
            return RedirectToAction("ViewStudents", new { bookID});
        }
        /*............................................................................................................................................................................*/

        //Borrow Book Action
        public ActionResult DoBorrowBook(int bookID, int studentID)
        {
            int borrow = DBDataService.GetBorrows().Select(a => a.borrowsID).Max() + 1;
            DBDataService.BorrowBook(borrow, bookID, studentID);
            return RedirectToAction("Books");
        }

        /*............................................................................................................................................................................*/
        //Return Book Actions
        public ActionResult DoReturnBook(int borrowID)
        {
           // int bookID = DBDataService.GetBorrows().Where(a => a.borrowsID == borrowID).Select(b => b.bookID).FirstOrDefault();
            DBDataService.ReturnBook(borrowID);
            return RedirectToAction("Books");
        }



    }
}