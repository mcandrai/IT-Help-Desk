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
    public class MessagesController : BaseController<Message, MessageRepository, string>
    {
        private readonly MessageRepository messageRepository;
        public MessagesController(MessageRepository repository) : base(repository)
        {
            messageRepository = repository;
        }
        [HttpGet("ticket-detail/{nik}")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("ticket-history/{nik}")]
        public IActionResult History()
        {
            return View();
        }

        [HttpPost("messages/Create-Message")]
        public JsonResult CreateMessageDetail(MessageDetail messageDetail)
        {
            var result = messageRepository.CreateMessageDetail(messageDetail);
            return Json(result);
        }


    }
}
