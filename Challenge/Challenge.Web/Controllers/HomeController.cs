using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Challenge.Model;
using Challenge.Service;
using Challenge.Service.Interfaces;

namespace Challenge.Web.Controllers
{
    public class HomeController : Controller
    {
        private IService<User> userService;
        private IService<Challenge.Model.TestExam> testService;

        public HomeController(IService<User> userService, IService<Challenge.Model.TestExam> testService)
        {
            this.userService = userService;
            this.testService = testService;
        }

        public ActionResult Index()
        {
            Challenge.Model.TestExam test = testService.GetAll().First();
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