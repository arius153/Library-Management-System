using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryManagement;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LibraryManagement.Tests
{
    [TestClass()]
    public class ManagerTests
    {
        [TestMethod()]
        public void addBookTest_AddOneBook_Returns1()
        {
            Manager manager = new Manager();
            var book = new Book("name", "Author", "category", "language", DateTime.Now, "isbn");
            int response = manager.addBook(book);
            Assert.AreEqual(1, response);
            File.Delete("books.json");
        }

        [TestMethod()]
        public void ManagerTest()
        {
            var managerToTest = new Manager();
            Assert.IsInstanceOfType(managerToTest, typeof(Manager));
        }

        [TestMethod()]
        public void deleteBookTest_DeleteBookThatDoesNotExists_Response0()
        {
            Manager manager = new Manager();
            int response = manager.deleteBook(99);
            Assert.AreEqual(0, response);
            removeTestGeneratedFiles();
        }
        [TestMethod()]
        public void deleteBookTest_DeleteBookThatIsTaken_Responseminus1()
        {
            Manager manager = new Manager();
            
            var book = new Book("name", "Author", "category", "language", DateTime.Now, "isbn");
            book.isTaken = true;
            manager.addBook(book);
            int response = manager.deleteBook(0);
            Assert.AreEqual(-1, response);
            removeTestGeneratedFiles();
        }
        [TestMethod()]
        public void deleteBookTest_DeleteBookthatExistsAndIsNotTaken_Response1()
        {
            Manager manager = new Manager();
            var book = new Book("name", "Author", "category", "language", DateTime.Now, "isbn");
            manager.addBook(book);
            int response = manager.deleteBook(0);
            Assert.AreEqual(1, response);
            removeTestGeneratedFiles();
        }

        [TestMethod()]
        public void takeBookTest_TakeBookForPeriodLongerThan2Months_ResponseMinus4()
        {
            Manager manager = new Manager();
            var book = new Book("name", "Author", "category", "language", DateTime.Now, "isbn");
            manager.addBook(book);
            int response = manager.takeBook("name", 0, 90);
            Assert.AreEqual(-4, response);
            removeTestGeneratedFiles();
        }
        [TestMethod()]
        public void takeBookTest_TakeBookThatDoesNotExist_ReponseMinus2()
        {
            Manager manager = new Manager();
            
            int response = manager.takeBook("name", 0, 1);
            Assert.AreEqual(-2, response);
            removeTestGeneratedFiles();
        }
        [TestMethod()]
        public void takeBookTest_TakeBookThatIsAlreadyTaken_ResponseMinus3()
        {
            Manager manager = new Manager();
            var book = new Book("name", "Author", "category", "language", DateTime.Now, "isbn");
            book.isTaken = true;
            manager.addBook(book);
            int response = manager.takeBook("name", 0, 1);
            Assert.AreEqual(-3, response);
            removeTestGeneratedFiles();
        }
        [TestMethod()]
        public void takeBookTest_TakeMoreThanThreeBooks_ResponseMinus1()
        {
            Manager manager = new Manager();
            var book = new Book("name", "Author", "category", "language", DateTime.Now, "isbn");
            manager.addBook(book);
            var book1 = new Book("name1", "Author", "category", "language", DateTime.Now, "isbn");
            manager.addBook(book1);
            var book2 = new Book("name2", "Author", "category", "language", DateTime.Now, "isbn");
            manager.addBook(book2);
            var book3 = new Book("name3", "Author", "category", "language", DateTime.Now, "isbn");
            manager.addBook(book3);
             manager.takeBook("name", 0, 2);
            manager.takeBook("name", 1, 1);
             manager.takeBook("name", 2, 1);
            int response = manager.takeBook("name", 3, 1);
            Assert.AreEqual(-1, response);
            removeTestGeneratedFiles();
        }
        [TestMethod()]
        public void takeBookTest_TakeSingleExistingAndAvailableBook_Returns0()
        {
            Manager manager = new Manager();
            var book = new Book("name", "Author", "category", "language", DateTime.Now, "isbn");
            
            manager.addBook(book);
            int response = manager.takeBook("name", 0, 1);
            Assert.AreEqual(0, response);
            removeTestGeneratedFiles();
        }       
        [TestMethod()]
        public void returnBookTest_UserDoesNotExists_ReturnsMinus1()
        {
            Manager manager = new Manager();
            var book = new Book("name", "Author", "category", "language", DateTime.Now, "isbn");
            manager.addBook(book);
            int response = manager.returnBook("name", 0);
            Assert.AreEqual(-1, response);
            removeTestGeneratedFiles();
        }
        [TestMethod()]
        public void returnBookTest_BookDoesNotExists_ReturnsMinus2()
        {
            Manager manager = new Manager();
            var book = new Book("name", "Author", "category", "language", DateTime.Now, "isbn");
            manager.addBook(book);
            manager.takeBook("name", 0, 1);
            int response = manager.returnBook("name", 99);
            Assert.AreEqual(-2, response);
            removeTestGeneratedFiles();
        }       
        [TestMethod()]
        public void returnBookTest_UserDoesNotHaveBookThatHeWantsToReturn_ReturnsMinus3()
        {
            Manager manager = new Manager();
            var book = new Book("name", "Author", "category", "language", DateTime.Now, "isbn");
            manager.addBook(book);
            var book2 = new Book("name2", "Author", "category", "language", DateTime.Now, "isbn");
            manager.addBook(book2);
            manager.takeBook("name", 0, 1);

            int response = manager.returnBook("name", 1);
            Assert.AreEqual(-3, response);
            removeTestGeneratedFiles();


        }       
        [TestMethod()]
        public void returnBookTest_UserReturnedBookTooLate_Returns1()
        {
            Manager manager = new Manager();
            var book = new Book("name", "Author", "category", "language", DateTime.Now, "isbn");
            manager.addBook(book);
            manager.takeBook("name", 0, 1);
            book.returnDate = DateTime.Now.AddDays(-1);
            int response = manager.returnBook("name", 0);
            Assert.AreEqual(1, response);
            removeTestGeneratedFiles();
        }       
        [TestMethod()]
        public void returnBookTest_UserReturnedBookOnTime_Returns0()
        {
            Manager manager = new Manager();
            var book = new Book("name", "Author", "category", "language", DateTime.Now, "isbn");
            manager.addBook(book);
            manager.takeBook("name", 0, 30);
            int response = manager.returnBook("name", 0);
            Assert.AreEqual(0, response);
            removeTestGeneratedFiles();
        }       
        
        void removeTestGeneratedFiles()
        {
            File.Delete("books.json");
            File.Delete("users.json");
        }
        
    }
}