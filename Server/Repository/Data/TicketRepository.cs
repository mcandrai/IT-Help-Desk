using Microsoft.EntityFrameworkCore;
using Server.Context;
using Server.Model;
using Server.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Repository.Data
{
    public class TicketRepository : GeneralRepository<MyContext, Ticket, int>
    {
        private readonly MyContext myContext;
        public TicketRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public int CreateTicket(TicketDetailVM ticketDetailVM)
        {
            var ticket = new Ticket
            {
                CreateAt = ticketDetailVM.CreateAt,
                UpdateAt = ticketDetailVM.UpdateAt,
                StatusId = 1,
                CategoryId = ticketDetailVM.CategoryId,
                NIK = ticketDetailVM.NIK
            };
            myContext.Tickets.Add(ticket);
            myContext.SaveChanges();

            var message = new Message
            {
                MessageText = ticketDetailVM.Message,
                TicketId = ticket.Id,
            };
            myContext.Messages.Add(message);
            return myContext.SaveChanges();
        }

        public int UpdateTicket(TicketDetailVM ticketDetailVM)
        {
            var ticket = myContext.Tickets.FirstOrDefault(a => a.Id == ticketDetailVM.Id);
            {
                ticket.UpdateAt = DateTime.Now;
                ticket.StatusId = ticketDetailVM.StatusId;
            };
            myContext.Entry(ticket).State = EntityState.Modified;
            return myContext.SaveChanges();
        }
    }
}
