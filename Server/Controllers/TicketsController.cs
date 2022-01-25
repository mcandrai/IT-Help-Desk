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
    public class TicketsController : BaseController<Ticket, TicketRepository, int>
    {
        private readonly TicketRepository ticketRepository;
        public TicketsController(TicketRepository ticketRepository) : base(ticketRepository)
        {
            this.ticketRepository = ticketRepository;
        }
    }
}
