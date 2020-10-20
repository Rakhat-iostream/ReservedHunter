using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IndustrialStudentPositionHunters.Controllers.ViewControllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
           return View();
        }

        public IActionResult Advertisements()
        {
            return View("Index");
        }

        public IActionResult RegistrationForm()
        {
            return PartialView("RegistrationForm");
        }
        public IActionResult AuthorisationForm()
        {
            return PartialView("AuthorisationForm");
        }

        public IActionResult Profile()
        {
            return View("Index");
        }
    }
}
