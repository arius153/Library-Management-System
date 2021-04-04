using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Tests
{
    [TestClass()]
    public class BookTests
    {
        [TestMethod()]
        public void BookTest()
        {
            var book = new Book("name", "Author", "category", "language", DateTime.Now, "isbn");
            Assert.IsInstanceOfType(book, typeof(Book));
        }

        
        
    }
}