using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [Authorize(Roles = Pages.MainMenu.Customer.RoleName)]
    public class LearnerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}