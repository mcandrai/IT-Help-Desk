﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class UserController : Controller
    {
        [HttpGet("user-ticket")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
