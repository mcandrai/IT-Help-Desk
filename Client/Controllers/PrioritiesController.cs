using Client.Base;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class PrioritiesController : BaseController<Priority, PriorityRepository, string>
    {
        private readonly PriorityRepository priorityRepository;

        public PrioritiesController(PriorityRepository repository) : base(repository)
        {
            priorityRepository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
