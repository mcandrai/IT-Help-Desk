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
    public class EscalationsController : BaseController<Priority, EscalationRepository, int>
    {
        private readonly EscalationRepository escalationRepository;

        public EscalationsController(EscalationRepository escalationRepository) : base(escalationRepository)
        {
            this.escalationRepository = escalationRepository;
        }
    }
}
