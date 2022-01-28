﻿using Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize]
    /*[Authorize(Roles = "HelpDesk")]*/
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("dashboard")]
        public IActionResult Index()
        {
            if (User.IsInRole("Employee"))
            {
                return RedirectToAction("index", "user");
            }
            else if (User.IsInRole("Help Desk"))
            {
                return View();
            }
            else if (User.IsInRole("Bug System"))
            {
                return RedirectToAction("index", "bugsystem");
            }
            else
            {
                return RedirectToAction("index", "database");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
