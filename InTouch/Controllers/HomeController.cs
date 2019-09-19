using ProjectDb.Entities;
using ProjectDb.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace InTouch.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        /// <summary>
        /// Returns the main page of application.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Message = "Some message";
            return View();
        }
    }
}