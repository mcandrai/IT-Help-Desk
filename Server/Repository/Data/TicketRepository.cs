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
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
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
            var getTicket = myContext.Tickets.FirstOrDefault(a => a.Id == ticketDetailVM.Id);
            var ticket = new Ticket
            {
                Id = getTicket.Id,
                CreateAt = getTicket.CreateAt,
                UpdateAt = DateTime.Now,
                CategoryId = getTicket.CategoryId,
                StatusId = 2,
                NIK = getTicket.NIK
            };
            myContext.Entry(getTicket).State = EntityState.Detached;
            myContext.Entry(ticket).State = EntityState.Modified;
            return myContext.SaveChanges();
        }

        public int UpdateTicketBug(TicketDetailVM ticketDetailVM)
        {
            var getTicket = myContext.Tickets.FirstOrDefault(a => a.Id == ticketDetailVM.Id);
            var ticket = new Ticket
            {
                Id = getTicket.Id,
                CreateAt = getTicket.CreateAt,
                UpdateAt = DateTime.Now,
                CategoryId = getTicket.CategoryId,
                StatusId = 3,
                NIK = getTicket.NIK
            };
            myContext.Entry(getTicket).State = EntityState.Detached;
            myContext.Entry(ticket).State = EntityState.Modified;
            return myContext.SaveChanges();
        }
        public int UpdateTicketDatabase(TicketDetailVM ticketDetailVM)
        {
            var getTicket = myContext.Tickets.FirstOrDefault(a => a.Id == ticketDetailVM.Id);
            var ticket = new Ticket
            {
                Id = getTicket.Id,
                CreateAt = getTicket.CreateAt,
                UpdateAt = DateTime.Now,
                CategoryId = getTicket.CategoryId,
                StatusId = 5,
                NIK = getTicket.NIK
            };
            myContext.Entry(getTicket).State = EntityState.Detached;
            myContext.Entry(ticket).State = EntityState.Modified;
            return myContext.SaveChanges();
        }

        public TicketDetailVM GetTicketDetail(int ID)
        {
            var query = myContext.Tickets.Where(t=>t.Id == ID).Include(m => m.Message).FirstOrDefault();
            if (query == null)
            {
                return null;
            }
            var getData = new TicketDetailVM
            {
                Id = query.Id,
                CreateAt = query.CreateAt,
                UpdateAt = query.UpdateAt,
                CategoryId = query.CategoryId,
                StatusId = query.StatusId,
                NIK = query.NIK,
                Message = query.Message.MessageText
            };
            return getData;
        }

        public IQueryable ViewTicketUser(string NIK)
        {
            var ticket = (from t in myContext.Tickets
                          join a in myContext.Accounts on t.NIK equals a.NIK
                          join m in myContext.Messages on t.Id equals m.TicketId
                          join st in myContext.Statuses on t.StatusId equals st.Id
                          join ct in myContext.Categories on t.CategoryId equals ct.Id
                          where  a.NIK==NIK && st.Id!=3
                          select new
                          {
                              t.Id,
                              m.MessageText,
                              t.UpdateAt,
                              t.CreateAt,
                              StatusName = st.Name,
                              CategoryName = ct.Name,
                          });
            return ticket;
        }

        public IQueryable ViewTicketHelpDesk()
        {
            var ticket = (from t in myContext.Tickets
                          join m in myContext.Messages on t.Id equals m.TicketId
                          join st in myContext.Statuses on t.StatusId equals st.Id
                          join ct in myContext.Categories on t.CategoryId equals ct.Id
                          select new
                          {
                              t.Id,
                              m.MessageText,
                              t.UpdateAt,
                              t.CreateAt,
                              StatusName = st.Name,
                              CategoryName = ct.Name,
                          });
            return ticket;
        }
        public IQueryable ViewTicketBugSystem()
        {
            var ticket = (from t in myContext.Tickets
                          join m in myContext.Messages on t.Id equals m.TicketId
                          join st in myContext.Statuses on t.StatusId equals st.Id
                          join ct in myContext.Categories on t.CategoryId equals ct.Id
                          where t.StatusId==2
                          select new
                          {
                              t.Id,
                              m.MessageText,
                              t.UpdateAt,
                              t.CreateAt,
                              StatusName = st.Name,
                              CategoryName = ct.Name,
                          });
            return ticket;
        }
        public IQueryable ViewTicketDatabase()
        {
            var ticket = (from t in myContext.Tickets
                          join m in myContext.Messages on t.Id equals m.TicketId
                          join st in myContext.Statuses on t.StatusId equals st.Id
                          join ct in myContext.Categories on t.CategoryId equals ct.Id
                          where t.StatusId == 3
                          select new
                          {
                              t.Id,
                              m.MessageText,
                              t.UpdateAt,
                              t.CreateAt,
                              StatusName = st.Name,
                              CategoryName = ct.Name,
                          });
            return ticket;
        }
        public IQueryable ViewTicketHistory()
        {
            var ticket = (from t in myContext.Tickets
                          join m in myContext.Messages on t.Id equals m.TicketId
                          join st in myContext.Statuses on t.StatusId equals st.Id
                          join ct in myContext.Categories on t.CategoryId equals ct.Id
                          where t.StatusId == 3
                          select new
                          {
                              t.Id,
                              m.MessageText,
                              t.UpdateAt,
                              t.CreateAt,
                              StatusName = st.Name,
                              CategoryName = ct.Name,
                          });
            return ticket;
        }
    }
}
