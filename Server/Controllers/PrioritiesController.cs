using Microsoft.AspNetCore.Mvc;
using Server.Model;
using Server.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrioritiesController : BaseController<Priority, PriorityRepository, int>
    {
        private readonly PriorityRepository priorityRepository;

        public PrioritiesController(PriorityRepository priorityRepository) : base(priorityRepository)
        {
            this.priorityRepository = priorityRepository;
        }
    }
}
