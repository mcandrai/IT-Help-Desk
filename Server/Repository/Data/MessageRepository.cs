using Microsoft.Extensions.Configuration;
using Server.Context;
using Server.Model;
using Server.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Repository.Data
{
    public class MessageRepository : GeneralRepository<MyContext, Message, int>
    {
        public IConfiguration _configuration;
        private readonly MyContext myContext;
        public MessageRepository(IConfiguration configuration, MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
            this._configuration = configuration;
        }

        public int CreateMessageDetail(MessageDetail messageDetail)
        {
            var ticket = new MessageDetail
            {
                MessageId = messageDetail.MessageId,
                CreateAt = DateTime.Now,
                NIK = messageDetail.NIK,
                MessageText = messageDetail.MessageText
            };
            myContext.MessageDetails.Add(ticket);
            return myContext.SaveChanges();

        }
    }
}
