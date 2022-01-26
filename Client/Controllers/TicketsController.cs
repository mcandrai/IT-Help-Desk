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
        [HttpPost]
        public JsonResult CreateTicket(TicketDetailVM ticketDetailVM)
        {
            var result = ticketRepository.CreateTicket(ticketDetailVM);
            return Json(result);
        }
    }
}
