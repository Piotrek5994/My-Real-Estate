using Microsoft.AspNetCore.Mvc;

namespace My_Real_Estate.Controllers
{
    public class PropertyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
