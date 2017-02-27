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
    public class LogTest
    {
        private IService<Log> service;
        Mock<IContext> mockContext;
        private IRepository<Log> repo;
        Mock<DbSet<Log>> mockSet;
        IQueryable<Log> list;

        [TestInitialize]
        public void Initialize()
        {

            list = new List<Log>()
            {
                new Log()
                {
                    log_id=1,
                    logger ="crossover1",
                    application_id = "crossover1",
                    level = "Action",
                    message = "helloworld",                    
                    application = new Application()
                    {
                        application_id="crossover1",
                        display_name="crossover1",
                        secret="secret1"
                    }

                },
                new Log()
                {
                    log_id=2,
                    logger ="crossover2",
                    application_id = "crossover2",
                    level = "Action",
                    message = "helloworld",
                    application = new Application()
                    {
                        application_id="crossover2",
                        display_name="crossover2",
                        secret="secret2"
                    }

                }

            }.AsQueryable();

            mockSet = new Mock<DbSet<Log>>();
            mockSet.As<IQueryable<Log>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Log>>().Setup(m => m.Expression).Returns(list.Expression);
            mockSet.As<IQueryable<Log>>().Setup(m => m.ElementType).Returns(list.ElementType);
            mockSet.As<IQueryable<Log>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());

            mockContext = new Mock<IContext>();
            mockContext.Setup(c => c.Set<Log>()).Returns(mockSet.Object);
            
            repo = new Repository<Log>(mockContext.Object);
            service = new LogService(repo);

        }

        [TestMethod]
        public void GetAll()
        {
            //Act
            List<Log> results = service.GetAll().ToList();

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count);
        }
        [TestMethod]
        public void GetById()
        {
            //Act
            Log result = ((LogService)service).GetLog(1);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("crossover1", result.application_id);
        }
        [TestMethod]
        public void Insert()
        {            
            Log log = new Log()
            {
                log_id = 3,
                logger = "crossover3",
                application_id = "crossover3",
                level = "Action",
                message = "helloworld",
                application = new Application()
                {
                    application_id = "crossover3",
                    display_name = "crossover3",
                    secret = "secret3"
                }
            };
            mockSet.Setup(m => m.Add(log)).Returns((Log e) =>
            {
                return e;
            });
            service.Insert(log);
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
            mockSet.Verify(m => m.Add(It.IsAny<Log>()), Times.Exactly(1));
        }

        [TestMethod]
        public void Update()
        {
            //Act
            Log result = ((LogService)service).GetLog(1);
            result.message = "helloworld";
            service.Update(result);
            mockContext.Verify(m => m.SaveChanges(), Times.Once());                   
        }
    }
}
