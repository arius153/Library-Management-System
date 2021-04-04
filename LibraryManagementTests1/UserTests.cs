using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Tests
{
    [TestClass()]
    public class UserTests
    {
        [TestMethod()]
        public void UserTest()
        {
            var user = new User("name");
            Assert.IsInstanceOfType(user, typeof(User));
        }

        [TestMethod()]
        public void takeBookTest_TakeOneBook_Returns0()
        {
            //Arange
            User user = new User("name");
            //Act
            int response = user.takeBook(0);
            //Assert
            Assert.AreEqual(0, response);
        }
        [TestMethod()]
        public void takeBookTest_TakeTwoBooks_Returns0()
        {
            User user = new User("name");
            user.takeBook(0);
            int response = user.takeBook(1);
            Assert.AreEqual(0, response);

        }
        [TestMethod()]
        public void takeBookTest_TakeThreeBooks_Returns0()
        {
            User user = new User("name");
            user.takeBook(0);
            user.takeBook(1);
            int response = user.takeBook(2);
            Assert.AreEqual(0, response);

        }
        [TestMethod()]
        public void takeBookTest_TakeFourBooks_Returnsminus1()
        {
            User user = new User("name");
            user.takeBook(0);
            user.takeBook(1);
            user.takeBook(2);
            int response = user.takeBook(3);
            Assert.AreEqual(-1, response);

        }



        [TestMethod()]
        public void returnBookTest_returnBookThatUserDoesNotHave_Returnsminus1()
        {
            User user = new User("name");
            user.takeBook(0);
            int response = user.returnBook(2);
            Assert.AreEqual(-1, response);
        }
        [TestMethod()]
        public void returnBookTest_ReturnBookThatUserHas_Returns0()
        {
            User user = new User("name");
            user.takeBook(0);
            int response = user.returnBook(0);
            Assert.AreEqual(0, response);
        }
    }
}