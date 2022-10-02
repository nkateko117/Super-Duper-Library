using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace Super_Duper_Library.Models
{
    public class DBDataService
    {
        public static string ConnectionString = "Data Source=.;Initial Catalog=Library;Integrated Security=True"; //Connect Library Database from local server 

        /* ..........................................................................................................................................*/
        //Method for borrowing a book
        public static void BorrowBook(int borrowID, int bookID, int studentID) //Borrow Book Function
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            DateTime now = DateTime.Now;
            try
            {
                connection.Open();
                SqlCommand myInsertCommand = new SqlCommand("insert into borrows(studentId, bookId, takenDate, broughtDate) VALUES ("+studentID+","+ bookID +",'"+now+"','"+null+"')", connection);

                myInsertCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }

            finally{
                connection.Close();
            }

        }

        /* ..........................................................................................................................................*/
        public static void ReturnBook(int borrowID) //Return Book Function 
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            DateTime now = DateTime.Now;
            try
            {
                connection.Open();
                SqlCommand myInsertCommand = new SqlCommand("Update borrows Set broughtDate='"+now+"' WHERE borrowId="+borrowID, connection);
                
                myInsertCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }

            finally
            {
                connection.Close();
            }
        }

        /* ..........................................................................................................................................*/
        public static List<Books> GetBooks() //Get a list of all books Function
        {
            List<Books> books = new List<Books>();
            Books book = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("select * from books", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            book = new Books
                            {
                                bookID = Convert.ToInt32(reader["bookId"]),
                                name = Convert.ToString(reader["name"]),
                                pageCount = Convert.ToInt32(reader["pagecount"]),
                                point = Convert.ToInt32(reader["point"]),
                                authorID = Convert.ToInt32(reader["authorId"]),
                                typeID = Convert.ToInt32(reader["typeId"]),
                                status = GetStatus(Convert.ToInt32(reader["bookId"]))
                            };
                            books.Add(book);
                        }
                    }
                }
                connection.Close();
            }

            return books;
        }

        /* ..........................................................................................................................................*/
        public static string GetStatus(int bookid)
        {
            string status=null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("select * from borrows where bookId="+bookid, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string rd = reader["broughtDate"].ToString();
                            if (rd == "1900/01/01 00:00:00") //This is the default date for NULL DateTime
                            {
                                status = "Out";
                                break;
                            }

                            else
                            {
                                status = "Available";
                            }
                        }
                    }
                }
                connection.Close();
            }

            return status;
        }

        /* ..........................................................................................................................................*/
        public static List<Authors> GetAuthors()
        {
            List<Authors> authors = new List<Authors>();
            Authors author = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("select * from authors", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            author = new Authors
                            {
                                authorID = Convert.ToInt32(reader["authorId"]),
                                name = Convert.ToString(reader["name"]),
                                surname = Convert.ToString(reader["surname"])
                            };
                            authors.Add(author);
                        }
                    }
                }
                connection.Close();
            }
            return authors;
        }

        /* ..........................................................................................................................................*/
        public static List<Types> GetTypes()
        {
            List<Types> types = new List<Types>();
            Types type = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("select * from types", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            type = new Types
                            {
                                typeID = Convert.ToInt32(reader["typeId"]),
                                name = Convert.ToString(reader["name"])
                            };
                            types.Add(type);
                        }
                    }
                }
                connection.Close();
            }
            return types;
        }

        /* ..........................................................................................................................................*/
        public static List<Borrows> GetBorrows()
        {
            List<Borrows> borrows = new List<Borrows>();
            Borrows borrow = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("select * from borrows", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                               borrow = new Borrows
                                {
                                    borrowsID = Convert.ToInt32(reader["borrowId"]),
                                    studentID = Convert.ToInt32(reader["studentId"]),
                                    bookID = Convert.ToInt32(reader["bookId"]),
                                    takeDate = Convert.ToDateTime(reader["takenDate"]),
                                    broughtDate = Convert.ToDateTime(reader["broughtDate"])
                                };
                                                                               
                            borrows.Add(borrow);
                        }
                    }
                }
                connection.Close();
            }
            return borrows;
        }

        /* ..........................................................................................................................................*/
        public static List<Students> GetStudents()
        {
            List<Students> students = new List<Students>();
            Students student1 = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("select * from students", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            student1 = new Students
                            {
                                studentID = Convert.ToInt32(reader["studentId"]),
                                name = Convert.ToString(reader["name"]),
                                surname = Convert.ToString(reader["surname"]),
                                birthdate = Convert.ToDateTime(reader["birthdate"]),
                                gender = Convert.ToChar(reader["gender"]),
                                Class = Convert.ToString(reader["class"]),
                                point = Convert.ToInt32(reader["point"])

                            };
                            students.Add(student1);
                        }
                    }
                }
                connection.Close();
            }
            return students;

        }

    }
    }
