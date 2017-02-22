using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestExam.Model;
using TestExam.Service;
using TestExam.Service.Interfaces;

namespace TestExam.Web.Controllers
{
    public class HomeController : Controller
    {
        private IService<User> userService;
        private IService<TestExam.Model.TestExam> testService;

        public HomeController(IService<User> userService, IService<TestExam.Model.TestExam> testService)
        {
            this.userService = userService;
            this.testService = testService;
        }

        public ActionResult Index()
        {
            TestExam.Model.TestExam test = testService.GetAll().First();
            TempData.Add("Test", test);
            ViewBag.Title = test.Name;
            ViewBag.Error = "";
            return View(test);
        }

        public ActionResult Start(string userName, string userPass)
        {
            User user = ((UserService)userService).GetUser(userName, userPass);
            if (user != null)
            {
                Session.Add("User", user);
                Session.Add("Test", TempData["Test"]);
                RedirectToAction("Index", "QuestionName");
                //redirigir a la vista de las preguntas
            }
            
            ViewBag.Error = "User name or password incorrect.";
            TempData.Keep();
            return View("Index", TempData["Test"]);
        }
    }
}