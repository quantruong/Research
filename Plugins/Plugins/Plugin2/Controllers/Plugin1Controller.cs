using Microsoft.AspNetCore.Mvc;

namespace Plugin2.Controllers
{
    public class Plugin2Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
