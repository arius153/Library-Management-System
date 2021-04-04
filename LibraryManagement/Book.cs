using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement
{
    public class Book
    {
        public string name { get; }
        public string author { get; }
        public string category { get; }
        public string language { get; }
        public DateTime publicationDate { get; }
        public string isbn { get; }
        public bool isTaken { get; set; }
        public DateTime? returnDate { get; set; }
        public int id { get; set; }

        public Book(string name, string author, string category, string language, DateTime pubDate, string isbn)
        {
            this.name = name;
            this.author = author;
            this.category = category;
            this.language = language;
            publicationDate = pubDate;
            this.isbn = isbn;
            isTaken = false;
        }

        public override string ToString()
        {
            return id + ". " +
                   "Name: " + name + " " +
                   "Author: " + author + " " +
                   "Category: " + category + " " +
                   "language: " + language + " " +
                   "PubDate: " + publicationDate.ToString() + " " +
                   "ISBN: " + isbn +
                   "Is taken: " + isTaken.ToString() + "\n";
        }
    }
}
