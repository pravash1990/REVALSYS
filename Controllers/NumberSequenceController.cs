
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    public class NumberSequenceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}