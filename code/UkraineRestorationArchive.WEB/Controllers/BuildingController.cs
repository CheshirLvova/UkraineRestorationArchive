using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UkraineRestorationArchive.WEB.Controllers
{
    public class BuildingController : Controller
    {
        public IActionResult Index(string addres)
        {
            ViewData["Address"] = addres;
            return View();
        }
    }
}
