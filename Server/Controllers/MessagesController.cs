using Microsoft.AspNetCore.Mvc;
using Server.Model;
using Server.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : BaseController<Message, MessageRepository, int>
    {
        private readonly MessageRepository messageRepository;

        public MessagesController(MessageRepository messageRepository) : base(messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        [HttpPost]
        [Route("Create-Message")]
        public ActionResult<MessageDetail> CreateMessageDetail(MessageDetail messageDetail)
        {
            var result = messageRepository.CreateMessageDetail(messageDetail);
            return Ok(new { status = HttpStatusCode.OK, message = "Successfully added data!" });
        }
    }
}
