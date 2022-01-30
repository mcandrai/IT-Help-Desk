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
    public class MessageController : BaseController<Message, MessageRepository, string>
    {
        private readonly MessageRepository messageRepository;
        public MessageController(MessageRepository repository) : base(repository)
        {
            messageRepository = repository;
        }
        [HttpGet("ticket-detail/{nik}")]
        public IActionResult Index()
        {
            return View();
        }

    }
}
