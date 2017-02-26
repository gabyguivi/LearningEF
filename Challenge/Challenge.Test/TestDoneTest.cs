using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Challenge.Service.Interfaces;
using Challenge.Service;
using Challenge.Data;
using Moq;
using System.Data.Entity;
using Challenge.Model;
using Challenge.Data.Interfaces;
using System.Linq;
using System.Collections.Generic;

namespace Challenge.Test
{
    [TestClass]
    public class TestDoneTest
    {
        private IService<TestDone> service;
        Mock<IContext> mockContext;
        private IRepository<TestDone> repo;
        Mock<DbSet<TestDone>> mockSet;
        IQueryable<TestDone> listTests;

        [TestInitialize]
        public void Initialize()
        {

            listTests = new List<TestDone>()
            {
                new TestDone()
                {
                    Id = 1,
                    Start = DateTime.Now,
                    End = DateTime.Now,
                    Gradle = 0,
                    Test = new Challenge.Model.TestExam() { Id = 1, Name = "Test1", Description = "DTest1", Duration = 3, PassScore = "50", TotalScore = "100" },
                    User = new User() { Id = 1, FullName = "Guivi", UserName = "guivi", Password = "pato" }
                },
                new TestDone()
                {
                    Id = 2,
                    Start = DateTime.Now,
                    End = DateTime.Now,
                    Gradle = 0,
                    Test = new Challenge.Model.TestExam() { Id = 1, Name = "Test1", Description = "DTest1", Duration = 3, PassScore = "50", TotalScore = "100" },
                    User = new User() { Id = 1, FullName = "Guivi", UserName = "guivi", Password = "pato" }
                }
            }.AsQueryable();

            mockSet = new Mock<DbSet<TestDone>>();
            mockSet.As<IQueryable<TestDone>>().Setup(m => m.Provider).Returns(listTests.Provider);
            mockSet.As<IQueryable<TestDone>>().Setup(m => m.Expression).Returns(listTests.Expression);
            mockSet.As<IQueryable<TestDone>>().Setup(m => m.ElementType).Returns(listTests.ElementType);
            mockSet.As<IQueryable<TestDone>>().Setup(m => m.GetEnumerator()).Returns(listTests.GetEnumerator());

            mockContext = new Mock<IContext>();
            mockContext.Setup(c => c.Set<TestDone>()).Returns(mockSet.Object);
            
            repo = new Repository<TestDone>(mockContext.Object);
            service = new TestDoneService(repo);

        }

        [TestMethod]
        public void GetAll()
        {
            //Act
            List<TestDone> results = service.GetAll().ToList();

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count);
        }

        [TestMethod]
        public void TestInsert()
        {
            int id = 1;
            TestDone test = new TestDone()
            {
                Id = 1,
                Start = DateTime.Now,
                End = DateTime.Now,
                Gradle = 0,
                Test = new Challenge.Model.TestExam() { Id = 1, Name = "Test1", Description = "DTest1", Duration = 3, PassScore = "50", TotalScore = "100" },
                User = new User() { Id = 1, FullName = "Guivi", UserName = "guivi", Password = "pato" }
            };
            mockSet.Setup(m => m.Add(test)).Returns((TestDone e) =>
            {
                e.Id = id;
                return e;
            });
            service.Insert(test);
            Assert.AreEqual(test.Id, id);
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
            mockSet.Verify(m => m.Add(It.IsAny<TestDone>()), Times.Exactly(1));
        }
    }
}
