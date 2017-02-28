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
    public class LogController : Controller
    {
        
        public LogController()
        {            
        }

        public ActionResult Index(string token, string app)
        {
            ViewBag.Token = token;
            ViewBag.App = app;
            return View();         
        }

    }
}