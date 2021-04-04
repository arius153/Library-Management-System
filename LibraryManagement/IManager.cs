using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement
{
    public interface IManager
    {
        public int addBook(Book book);
        public int deleteBook(int bookId);
        public int takeBook(string name, int bookId, int periodInDays);
        public int returnBook(string name, int bookId);
        public void printUserBooks(string name);
        public void printBooks(List<Book> booksToPrint = null);
        public void filterByAuthor(string author);
        public void filterByCategory(string category);
        public void filterByLanguage(string language);
        public void filterByISBN(string isbn);
        public void filterByName(string name);
        public void filterByAvailability(bool isNotAvailable);
        public void saveToFiles();
    }
}
