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
    public class MessagesController : BaseController<Message, MessageRepository, int>
    {
        private readonly MessageRepository messageRepository;

        public MessagesController(MessageRepository messageRepository) : base(messageRepository)
        {
            this.messageRepository = messageRepository;
        }
    }
}
