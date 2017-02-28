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
    public class AuthController : Controller
    {
        
        public AuthController()
        {            
        }

        public ActionResult Index()
        {
            return View();         
        }

    }
}