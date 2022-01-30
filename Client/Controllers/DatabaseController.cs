using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize(Roles = "Database Engineer")]
    public class DatabaseController : Controller
    {
        [HttpGet("database-engineer-ticket")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
