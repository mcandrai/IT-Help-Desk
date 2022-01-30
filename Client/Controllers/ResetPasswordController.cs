using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class ResetPasswordController : Controller
    {
        [HttpGet("reset-password")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
