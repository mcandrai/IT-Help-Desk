using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Server.Model;
using Server.Repository.Data;
using Server.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : BaseController<Ticket, TicketRepository, int>
    {
        private readonly TicketRepository ticketRepository;
        public IConfiguration _configuration;
        public TicketsController(TicketRepository ticketRepository, IConfiguration configuration) : base(ticketRepository)
        {
            this.ticketRepository = ticketRepository;
            this._configuration = configuration;
        }

        [HttpPost]
        [Route("Create-Ticket")]
        public ActionResult<TicketDetailVM> CreateTicket(TicketDetailVM ticketDetailVM)
        {
            var result = ticketRepository.CreateTicket(ticketDetailVM);
            return Ok(new { status = HttpStatusCode.OK,message = "Successfully added data!" });
        }

        [HttpPost]
        [Route("Update-Ticket-HelpDesk")]
        public ActionResult<TicketDetailVM> UpdateTicket(TicketDetailVM ticketDetailVM)
        {
            var result = ticketRepository.UpdateTicket(ticketDetailVM);
            return Ok(result);
        }
        [HttpPost]
        [Route("Update-Ticket-BugSystem")]
        public ActionResult<TicketDetailVM> UpdateTicketBug(TicketDetailVM ticketDetailVM)
        {
            var result = ticketRepository.UpdateTicketBug(ticketDetailVM);
            return Ok(result);
        }
        [HttpPost]
        [Route("Update-Ticket-Database")]
        public ActionResult<TicketDetailVM> UpdateTicketDatabase(TicketDetailVM ticketDetailVM)
        {
            var result = ticketRepository.UpdateTicketDatabase(ticketDetailVM);
            return Ok(result);
        }

        [HttpGet("View-Ticket-User")]
        public IActionResult ViewTicketUser(string nik)
        {
            var result = ticketRepository.ViewTicketUser(nik);
            return Ok(result);
        }
        [HttpGet("View-Ticket-HelpDesk")]
        public IActionResult ViewTicketHelpDesk()
        {
            var result = ticketRepository.ViewTicketHelpDesk();
            return Ok(result);
        }
        [HttpGet("View-Ticket-BugSystem")]
        public IActionResult ViewTicketBugSystem()
        {
            var result = ticketRepository.ViewTicketBugSystem();
            return Ok(result);
        }
        [HttpGet("View-Ticket-Database")]
        public IActionResult ViewTicketDatabase()
        {
            var result = ticketRepository.ViewTicketDatabase();
            return Ok(result);
        }

        [HttpGet("View-Ticket-Detail")]
        public IActionResult ViewTicketDetail(int ID)
        {
            var result = ticketRepository.GetTicketDetail(ID);
            return Ok(result);
        }

        [HttpGet("View-Message-Detail")]
        public IActionResult ViewMessageDetail(int id)
        {
            var result = ticketRepository.ViewMessageDetail(id);
            return Ok(result);
        }
    }
}
