using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement
{
    public class ConsoleUI
    {
        private readonly IManager _libManager;
        public ConsoleUI(IManager manager)
        {
            _libManager = manager;
        }
        public void startUI()
        {
            while (true)
            {
                Console.Write("1.Add a book\n2.Delete a book\n3.Take a book\n4.Return a book\n5.List all books\n6.Exit\n");
                Console.WriteLine("Enter choice: ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        addBook();
                        break;
                    case "2":
                        deleteBook();
                        break;
                    case "3":
                        takeBook();
                        break;
                    case "4":
                        returnBook();
                        break;
                    case "5":
                        listBooks();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("This choice does not exists!");
                        break;
                }
            }
        }
        void addBook()
        {
            Console.Clear();
            string name, author, category, language, ISBN;
            DateTime pubDate;
            Console.WriteLine("Enter name:");
            name = Console.ReadLine();
            Console.WriteLine("Enter author:");
            author = Console.ReadLine();
            Console.WriteLine("Enter category:");
            category = Console.ReadLine();
            Console.WriteLine("Enter language:");

            language = Console.ReadLine();

            Console.WriteLine("Enter publication date (YYYY-MM-DD):");
            while (true)
            {
                if (DateTime.TryParse(Console.ReadLine(), out pubDate))
                {
                    break;
                }
                Console.WriteLine("Date is wrong, enter again:");
            }
            Console.WriteLine("Enter ISBN:");
            ISBN = Console.ReadLine();
            _libManager.addBook(new Book(name, author, category, language, pubDate, ISBN));
            Console.WriteLine("Book \"" + name + "\"" + " added to library.\n");
        }

        void deleteBook()
        {
            Console.Clear();
            _libManager.printBooks();
            Console.WriteLine("Enter id of which book you want to delete: ");
            int bookId;
            while (true)
            {
                if (Int32.TryParse(Console.ReadLine(), out bookId))
                {
                    break;
                }
                Console.WriteLine("Enter a whole number!");
            }
            int response = _libManager.deleteBook(bookId);
            if (response == 0)
            {
                Console.WriteLine("This book does not exists!\n");
            }
            if (response == -1)
            {
                Console.WriteLine("This book is taken by someone!\n");
            }
            if (response == 1)
            {
                Console.WriteLine("Book is deleted successfully\n");
            }
        }

        void takeBook()
        {
            Console.Clear();
            _libManager.printBooks();
            Console.WriteLine("Enter name of user who wants to take a book:");
            string name = Console.ReadLine();
            Console.WriteLine("User books: ");
            _libManager.printUserBooks(name);
            Console.WriteLine("Enter id of wanted book: ");
            int bookId;
            while (true)
            {
                if (Int32.TryParse(Console.ReadLine(), out bookId))
                    break;
                Console.WriteLine("Enter a whole number!");
            }
            Console.WriteLine("Enter how long you want to keep the book in days (max 2 months):");
            int period;
            while (true)
            {
                if (Int32.TryParse(Console.ReadLine(), out period))
                    break;
                Console.WriteLine("Enter whole number for days!");
            }
            int response = _libManager.takeBook(name, bookId, period);
            if (response == -4)
            {
                Console.WriteLine("Book is not taken. Period is to long!\n");
            }
            if (response == -3)
            {
                Console.WriteLine("Book is not taken. It is already taken by someone else!\n");
            }
            if (response == -2)
            {
                Console.WriteLine("Book is not taken. Book with this id does not exists!\n");
            }

            if (response == -1)
            {
                Console.WriteLine("Book is not taken. User already has to many books (3 is maximum)\n");
            }
            if (response == 0)
            {
                Console.WriteLine("Book is taken succesfully!\n");
            }
        }

        void returnBook()
        {
            Console.Clear();
            Console.WriteLine("Enter name of user who wants to return a book:");
            string name = Console.ReadLine();
            Console.WriteLine("User books: ");
            _libManager.printUserBooks(name);
            Console.WriteLine("Enter id of book to return");
            int bookId;
            while (true)
            {
                if (Int32.TryParse(Console.ReadLine(), out bookId))
                    break;
                Console.WriteLine("Enter a whole number!");
            }
            int response = _libManager.returnBook(name, bookId);
            if (response == -1)
            {
                Console.WriteLine("User with this name does not exists!\n");
                return;
            }
            if (response == -2)
            {
                Console.WriteLine("This book does not even exists!\n");
                return;
            }
            if (response == -3)
            {
                Console.WriteLine("User does not have this book!\n");
                return;
            }
            if (response == 1)
            {
                Console.WriteLine("Oh darling, you are a bit to late, but is okey, I won't tell anyone\n");
                return;
            }
            if (response == 0)
            {
                Console.WriteLine("Book is returned successfuly\n");
                return;
            }
        }

        void listBooks()
        {
            Console.Clear();
            _libManager.printBooks();
            Console.Write("Filter by: 1. Author 2.Category 3.Language 4.ISBN 5.Name 6.Availability | 7.List everything 8.Go back\n");
            Console.Write("Enter choice: ");
            string input;
            string tempForFiltering;
            while (true)
            {
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Enter Authors full name:");
                        tempForFiltering = Console.ReadLine();
                        _libManager.filterByAuthor(tempForFiltering);
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Enter category:");
                        tempForFiltering = Console.ReadLine();
                        _libManager.filterByCategory(tempForFiltering);
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Enter language:");
                        tempForFiltering = Console.ReadLine();
                        _libManager.filterByLanguage(tempForFiltering);
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("Enter ISBN:");
                        tempForFiltering = Console.ReadLine();
                        _libManager.filterByISBN(tempForFiltering);
                        break;
                    case "5":
                        Console.Clear();
                        Console.WriteLine("Enter books name:");
                        tempForFiltering = Console.ReadLine();
                        _libManager.filterByName(tempForFiltering);
                        break;
                    case "6":
                        Console.Clear();
                        Console.WriteLine("1.Taken\n2.Available");
                        Console.WriteLine("Enter choice: ");
                        string intputForAvailability = Console.ReadLine();
                        bool? isTaken = null;
                        switch (intputForAvailability)
                        {
                            case "1":
                                isTaken = true;
                                break;
                            case "2":
                                isTaken = false;
                                break;
                            default:
                                Console.WriteLine("This is not a choice!");
                                break;
                        }
                        if (isTaken != null)
                        {
                            _libManager.filterByAvailability((bool)isTaken);
                        }
                        break;
                    case "7":
                        _libManager.printBooks();
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("This choice does not exists!");
                        break;
                }

                Console.Write("\nFilter by: 1. Author 2.Category 3.Language 4.ISBN 5.Name 6.Availability | 7.List everything 8.Go back\n");
                Console.Write("Enter choice: ");
            }
        }
    }
   
}
