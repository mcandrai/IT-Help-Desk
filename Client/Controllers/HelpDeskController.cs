using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize(Roles="Help Desk")]
    public class HelpDeskController : Controller
    {
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
    }
}
