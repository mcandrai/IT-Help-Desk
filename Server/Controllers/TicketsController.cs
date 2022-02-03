using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment webHostEnvironment;
        public IConfiguration _configuration;
        public TicketsController(TicketRepository ticketRepository, IConfiguration configuration, IWebHostEnvironment hostEnvironment) : base(ticketRepository)
        {
            this.ticketRepository = ticketRepository;
            this._configuration = configuration;
            webHostEnvironment = hostEnvironment;
        }

        [HttpPost]
        [Route("Create-Ticket")]
        public ActionResult CreateTicket(TicketDetailVM ticketDetailVM)
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

        [HttpPost]
        [Route("Update-Ticket-Done")]
        public ActionResult<TicketDetailVM> UpdateTicketDone(TicketDetailVM ticketDetailVM)
        {
            var result = ticketRepository.UpdateTicketDone(ticketDetailVM);
            if (!result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("View-Ticket-User/{nik}")]
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

        [HttpGet("View-Ticket-History-User/{nik}")]
        public IActionResult ViewTicketHistoryUser(string nik)
        {
            var result = ticketRepository.ViewTicketHistoryUser(nik);
            return Ok(result);
        }

        [HttpGet("View-Ticket-History")]
        public IActionResult ViewTicketHistory()
        {
            var result = ticketRepository.ViewTicketHistory();
            return Ok(result);
        }

        [HttpGet("View-Ticket-Detail/{Id}")]
        public IActionResult ViewTicketDetail(int Id)
        {
            var result = ticketRepository.GetTicketDetail(Id);
            return Ok(result);
        }
        
        [HttpGet("View-Message-Detail/{Id}")]
        public IActionResult ViewMessageDetail(int id)
        {
            var result = ticketRepository.ViewMessageDetail(id);
            return Ok(result);
        }
    }
}
