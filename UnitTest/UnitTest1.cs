using System;
using System.Collections.Generic;
using System.Web.Mvc;
using InTouch.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProjectDb.Entities;
using ProjectDb.Interfaces;
using ProjectDb.Repositories;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        internal EFUnitOfWork repository;

        public UnitTest1()
        {
            repository = new EFUnitOfWork("DefaultConnection");
        }

        [TestMethod]
        public void TestMethodGet()
        {
            // Arrange
            var mock = new Mock<IRepository<Person>>();
            int id = 1;
            mock.Setup(a => a.Get(id)).Returns(new Person());
            UserController userController = new UserController(repository);

            // Act 
            ViewResult result = userController.Index() as ViewResult;
            List<Message> actual = result.ViewBag.Messages as List<Message>;
            // Assert
            Assert.IsNotNull(actual);
        }


    }
}
