using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plugin1.Controllers
{
    public class Plugin1Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
