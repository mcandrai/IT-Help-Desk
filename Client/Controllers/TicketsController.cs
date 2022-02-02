using Client.Base;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using Server.Model;
using Server.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class TicketsController : BaseController<Ticket, TicketRepository, string>
    {
        private readonly TicketRepository ticketRepository;
        public TicketsController(TicketRepository repository) : base(repository)
        {
            ticketRepository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("tickets/create-ticket")]
        public JsonResult CreateTicket(TicketDetailVM ticketDetailVM)
        {
            var result = ticketRepository.CreateTicket(ticketDetailVM);
            return Json(result);
        }

        [HttpPost]
        public JsonResult UpdateTicket(TicketDetailVM ticketDetailVM)
        {
            var result = ticketRepository.UpdateTicket(ticketDetailVM);
            return Json(result);
        }

        [HttpPost]
        public JsonResult UpdateTicketBug(TicketDetailVM ticketDetailVM)
        {
            var result = ticketRepository.UpdateTicketBug(ticketDetailVM);
            return Json(result);
        }

        [HttpPost]
        public JsonResult UpdateTicketDatabase(TicketDetailVM ticketDetailVM)
        {
            var result = ticketRepository.UpdateTicketDatabase(ticketDetailVM);
            return Json(result);
        }

        [HttpPost]
        public JsonResult UpdateTicketDone(TicketDetailVM ticketDetailVM)
        {
            var result = ticketRepository.UpdateTicketDone(ticketDetailVM);
            return Json(result);
        }

        [HttpGet("tickets/View-Ticket-History-User/{nik}")]
        public async Task<JsonResult> ViewTicketHistoryUser(string nik)
        {
            var result = await ticketRepository.ViewTicketHistoryUser(nik);
            return Json(result);
        }

        [HttpGet("tickets/View-Ticket-History")]
        public async Task<JsonResult> ViewTicketHistory()
        {
            var result = await ticketRepository.ViewTicketHistory();
            return Json(result);
        }

        [HttpGet("tickets/View-Ticket-BugSystem")]
        public async Task<JsonResult> ViewTicketBugSystem()
        {
            var result = await ticketRepository.ViewTicketBugSystem();
            return Json(result);
        }

        [HttpGet("tickets/View-Ticket-HelpDesk")]
        public async Task<JsonResult> ViewTicketHelpDesk()
        {
            var result = await ticketRepository.ViewTicketHelpDesk();
            return Json(result);
        }

        [HttpGet("tickets/View-Ticket-Database")]
        public async Task<JsonResult> ViewTicketDatabase()
        {
            var result = await ticketRepository.ViewTicketDatabase();
            return Json(result);
        }

        [HttpGet("tickets/View-Ticket-User/{nik}")]
        public async Task<JsonResult> ViewTicketUser(string nik)
        {
            var result = await ticketRepository.ViewTicketUser(nik);
            return Json(result);
        }

        [HttpGet("tickets/View-Ticket-Detail/{id}")]
        public async Task<JsonResult> Detail(int id)
        {
            var result = await ticketRepository.ViewTicketDetail(id);
            return Json(result);
        }

        [HttpGet("tickets/View-Message-Detail/{Id}")]
        public async Task<JsonResult> ViewMessageDetail(int Id)
        {
            var result = await ticketRepository.ViewMessageDetail(Id);
            return Json(result);
        }
    }
}
