using Microsoft.EntityFrameworkCore;
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
            myContext.SaveChanges();

            var message = myContext.Messages.FirstOrDefault(m => m.Id == messageDetail.MessageId);
            var status = myContext.Tickets.FirstOrDefault(t => t.Id == message.TicketId);
            var account = myContext.AccountRoles.FirstOrDefault(ar => ar.AccountId == messageDetail.NIK);
            var role = myContext.Roles.FirstOrDefault(r => r.Id == account.RoleId);
            //user reply sendiri
            if (status.StatusId == 1 && role.Id == 3)
            {
                {
                    status.StatusId = 1;
                };
            }
            //helpdesk reply
            else if (status.StatusId == 1 && role.Id != 3)
            {
                {
                    status.StatusId = 3;
                };
            }
            //user reply setelah helpdesk reply
            else if (status.StatusId == 3 && role.Id == 3)
            {
                {
                    status.StatusId = 2;
                };
            }
            //helpdesk reply setelah user reply kembali
            else if (status.StatusId == 2 && role.Id != 3)
            {
                {
                    status.StatusId = 3;
                };
            }
            else
            {
                {
                    status.StatusId = status.StatusId;
                };
            }
            myContext.Entry(status).State = EntityState.Modified;
            return myContext.SaveChanges();
        }
    }
}
