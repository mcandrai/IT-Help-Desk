using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize(Roles = "Employee")]
    public class UserController : Controller
    {
        [HttpGet("ticket-user")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("history-user")]
        public IActionResult History()
        {
            return View();
        }
    }
}
