using Microsoft.AspNetCore.Http;
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
    public class StatusesController : BaseController<Status, StatusRepository, int>
    {
        private readonly StatusRepository statusRepository;

        public StatusesController(StatusRepository statusRepository) : base(statusRepository)
        {
            this.statusRepository = statusRepository;
        }
    }
}
