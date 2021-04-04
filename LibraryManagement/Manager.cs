using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LibraryManagement 
{
    public class Manager : IManager
    {
        List<Book> books;
        List<User> users;

        public Manager ()
        {
            if (!File.Exists("books.json"))
            {
                books = new List<Book>();
            }
            else
            {
                string json = File.ReadAllText("books.json");
                if (json != "")
                {
                    books = JsonConvert.DeserializeObject<List<Book>>(json);
                }
                else
                {
                    books = new List<Book>();
                }
            }

            if (!File.Exists("users.json"))
            {
                users = new List<User>();
            } else
            {
                string json = File.ReadAllText("users.json");
                if (json != "")
                {
                    users = JsonConvert.DeserializeObject<List<User>>(json);
                } else
                {
                    users = new List<User>();
                }
            }
           
        }

        public int addBook(Book book)
        {
            if (books.Count == 0)
                book.id = 0;
            else
            {
                book.id = books[(books.Count - 1)].id + 1;
            }
            books.Add(book);
            saveToFiles();
            //Everything is fine, book is added
            return 1;
        }
        
        public int deleteBook(int bookId)
        {
            Book bookToRemove = books.Find(n => n.id == bookId);
            if (bookToRemove == null)
            {
                //Book does not exist
                return 0;
            }
            if (bookToRemove.isTaken == true)
            {
                //Book is taken
                return -1;
            }
            books.Remove(bookToRemove);
            saveToFiles();
            //Book is deleted
            return 1;
        }

        public int takeBook(string name, int bookId, int periodInDays)
        {
            if ((DateTime.Now.AddMonths(2) - DateTime.Now).Days < periodInDays)
            {
                //Period is too long
                return -4;
            }
            Book neededBook = books.Find(n => n.id == bookId);
            if (neededBook == null)
            {
                //Book does not exist
                return -2;
            }

            if (neededBook.isTaken)
            {
                //Book is already taken
                return -3;
            }

            User userToTakeBook = users.Find(n => n.name == name);
            if (userToTakeBook == null)
            {
                userToTakeBook = new User(name);
                users.Add(userToTakeBook);
            } 

            if (userToTakeBook.takeBook(bookId) == -1)
            {
                //User has to many books
                return -1;
            }
            //Book is taken
           
            neededBook.isTaken = true;
            neededBook.returnDate = DateTime.Now.AddDays(periodInDays);
            //Saving data
            saveToFiles();
            return 0;
        }

        public int returnBook (string name, int bookId)
        {
            User userReturningBook = users.Find(n => n.name == name);
            if (userReturningBook == null)
            {
                //User does not exists
                return -1;
            }
            Book bookToReturn = books.Find(n => n.id == bookId);
            if (bookToReturn == null)
            {
                //Book doesnt exists
                return -2;
            }
            
            if (userReturningBook.returnBook(bookId) == -1)
            {
                //User doesnt have this book
                return -3;
            }

            
            bool isTooLate = false;
            if (bookToReturn.returnDate < DateTime.Now)
            {
                isTooLate = true;
            }

            bookToReturn.isTaken = false;
            bookToReturn.returnDate = null;
            //Saving data
            saveToFiles();
            if (isTooLate)
                //book returned too late
                return 1;
            //book is returned on time
            return 0;
             
        }

        public void printUserBooks(string name)
        {
            User user = users.Find(n => n.name == name);
            if (user == null)
            {
                Console.WriteLine("User does not have any books");
                return;
            }
            user.takenBookIds.ForEach(bookId => 
            {
                Console.WriteLine(books.Find(n => n.id == bookId).ToString());
            });
        }

        public void printBooks(List<Book> booksToPrint = null)
        {
            if (booksToPrint == null)
            {
                booksToPrint = books;
            }
            for (int i = 0; i < booksToPrint.Count; i++)
            {
                Console.Write(booksToPrint[i].ToString());
            }
        }

        public void filterByAuthor(string author)
        {
            List<Book> byAuthor = books.FindAll(e => e.author == author);
            printBooks(byAuthor);
        }
        public void filterByCategory(string category)
        {
            List<Book> byAuthor = books.FindAll(e => e.category == category);
            printBooks(byAuthor);
        }
        public void filterByLanguage(string language)
        {
            List<Book> byAuthor = books.FindAll(e => e.language == language);
            printBooks(byAuthor);
        }
        public void filterByISBN(string isbn)
        {
            List<Book> byAuthor = books.FindAll(e => e.isbn == isbn);
            printBooks(byAuthor);
        }
        public void filterByName(string name)
        {
            List<Book> byAuthor = books.FindAll(e => e.name == name);
            printBooks(byAuthor);
        }
        public void filterByAvailability(bool isNotAvailable)
        {
            List<Book> byAuthor = books.FindAll(e => e.isTaken == isNotAvailable);
            printBooks(byAuthor);
        }

        public void saveToFiles()
        {
            string json = JsonConvert.SerializeObject(books, Formatting.Indented);
            File.WriteAllText("books.json", json);
            json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText("users.json", json);
        }

    }
}
