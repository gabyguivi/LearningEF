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
    public class ApplicationTest
    {
        private IService<Application> service;
        Mock<IContext> mockContext;
        private IRepository<Application> repo;
        Mock<DbSet<Application>> mockSet;
        IQueryable<Application> list;

        [TestInitialize]
        public void Initialize()
        {

            list = new List<Application>()
            {
                new Application()
                {
                    application_id="crossover1",
                    display_name="crossover1",
                    secret="secret1"
                },
                new Application()
                {
                    application_id="crossover2",
                    display_name="crossover2",
                    secret="secret2"
                }
            }.AsQueryable();

            mockSet = new Mock<DbSet<Application>>();
            mockSet.As<IQueryable<Application>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Application>>().Setup(m => m.Expression).Returns(list.Expression);
            mockSet.As<IQueryable<Application>>().Setup(m => m.ElementType).Returns(list.ElementType);
            mockSet.As<IQueryable<Application>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());

            mockContext = new Mock<IContext>();
            mockContext.Setup(c => c.Set<Application>()).Returns(mockSet.Object);
            
            repo = new Repository<Application>(mockContext.Object);
            service = new ApplicationService(repo);

        }

        [TestMethod]
        public void GetAll()
        {
            //Act
            List<Application> results = service.GetAll().ToList();

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count);
        }
        [TestMethod]
        public void GetById()
        {
            //Act
            Application result = ((ApplicationService)service).GetApplication("crossover2");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("crossover2", result.application_id);
        }
        [TestMethod]
        public void Insert()
        {            
            Application app = new Application()
            {
                application_id = "crossover3",
                display_name = "crossover3",
                secret = "secret3"
            };
            mockSet.Setup(m => m.Add(app)).Returns((Application e) =>
            {
                return e;
            });
            service.Insert(app);
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
            mockSet.Verify(m => m.Add(It.IsAny<Application>()), Times.Exactly(1));
        }

        [TestMethod]
        public void Update()
        {
            //Act
            Application result = ((ApplicationService)service).GetApplication("crossover2");
            result.display_name = "crossoverupdated";
            service.Update(result);
            mockContext.Verify(m => m.SaveChanges(), Times.Once());                   
        }
    }
}
