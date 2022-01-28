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
    public class MessageDetailsController : BaseController<MessageDetail, MessageDetailRepository, int>
    {
        private readonly MessageDetailRepository messageDetailRepository;

        public MessageDetailsController(MessageDetailRepository messageDetailRepository) : base(messageDetailRepository)
        {
            this.messageDetailRepository = messageDetailRepository;
        }
    }
}
