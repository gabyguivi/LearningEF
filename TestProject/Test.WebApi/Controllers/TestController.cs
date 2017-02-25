using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestExam.Model;
using TestExam.Service;
using TestExam.Service.Interfaces;

namespace Test.WebApi.Controllers
{
    public class TestController : ApiController
    {
        private IService<TestDone> testService;
        private TestDoneService Service
        { get { return ((TestDoneService)testService); } }

        public TestController(IService<TestDone> service)
        {
            testService = service;
        }
        [HttpGet]
        public bool IsEnded(int id)
        {
            try
            {
                TestDone test = Service.GetTestDone(id);
                double minutes = (DateTime.Now - test.Start).TotalMinutes;
                return (minutes >= test.Test.Duration);
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

    }
}
