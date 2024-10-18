using Microsoft.AspNetCore.Mvc;

namespace TedarikaSeller.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
