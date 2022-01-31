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
    }
}
