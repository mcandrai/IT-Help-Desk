using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize(Roles = "Bug System")]
    public class BugSystemController : Controller
    {
        [HttpGet("bug-system-ticket")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("bug-system-history")]
        public IActionResult History()
        {
            return View();
        }
    }
}
