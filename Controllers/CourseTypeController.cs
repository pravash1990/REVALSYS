﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Controllers
{
    [Authorize(Roles = Pages.MainMenu.CustomerType.RoleName)]
    public class CourseTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}