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
                PriorityId = 1,
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
                StatusId = getTicket.StatusId,
                CategoryId = getTicket.CategoryId,
                PriorityId = 2,
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
                StatusId = getTicket.StatusId,
                CategoryId = getTicket.CategoryId,
                PriorityId = 3,
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
                StatusId = 2,
                PriorityId = 3,
                NIK = getTicket.NIK
            };
            myContext.Entry(getTicket).State = EntityState.Detached;
            myContext.Entry(ticket).State = EntityState.Modified;
            return myContext.SaveChanges();
        }

        public TicketMessage GetTicketDetail(int ID)
        {
            var query = myContext.Tickets.Where(t=>t.Id == ID).Include(m => m.Message).Include(s=>s.Status).Include(c=>c.Category).Include(p=>p.Priority).Include(e=>e.Employee).FirstOrDefault();
            if (query == null)
            {
                return null;
            }
            var getData = new TicketMessage
            {
                Id = query.Id,
                CreateAt = query.CreateAt,
                CategoryName = query.Category.Name,
                StatusName = query.Status.Name,
                PriorityName = query.Priority.Name,
                UserName = query.Employee.FirstName+" "+query.Employee.LastName,
                Message = query.Message.MessageText
            };
            return getData;
        }

        public IQueryable ViewTicketUser(string NIK)
        {
            var ticket = (from t in myContext.Tickets
                          join a in myContext.Accounts on t.NIK equals a.NIK
                          join e in myContext.Employees on t.NIK equals e.NIK
                          join m in myContext.Messages on t.Id equals m.TicketId
                          join st in myContext.Statuses on t.StatusId equals st.Id
                          join ct in myContext.Categories on t.CategoryId equals ct.Id
                          join p in myContext.Priorities on t.PriorityId equals p.Id
                          where  a.NIK==NIK
                          select new
                          {
                              t.Id,
                              m.MessageText,
                              t.UpdateAt,
                              t.CreateAt,
                              StatusName = st.Name,
                              CategoryName = ct.Name,
                              PriorityName = p.Name
                          });
            return ticket;
        }

        public IQueryable ViewTicketHelpDesk()
        {
            var ticket = (from t in myContext.Tickets
                          join m in myContext.Messages on t.Id equals m.TicketId
                          join e in myContext.Employees on t.NIK equals e.NIK
                          join st in myContext.Statuses on t.StatusId equals st.Id
                          join ct in myContext.Categories on t.CategoryId equals ct.Id
                          join p in myContext.Priorities on t.PriorityId equals p.Id
                          select new
                          {
                              t.Id,
                              m.MessageText,
                              t.UpdateAt,
                              t.CreateAt,
                              StatusName = st.Name,
                              PriorityName = p.Name,
                              EmployeeName = e.FirstName+" "+e.LastName,
                              CategoryName = ct.Name
                          });
            return ticket;
        }
        public IQueryable ViewTicketBugSystem()
        {
            var ticket = (from t in myContext.Tickets
                          join m in myContext.Messages on t.Id equals m.TicketId
                          join e in myContext.Employees on t.NIK equals e.NIK
                          join st in myContext.Statuses on t.StatusId equals st.Id
                          join ct in myContext.Categories on t.CategoryId equals ct.Id
                          join p in myContext.Priorities on t.PriorityId equals p.Id
                          where t.PriorityId==2
                          select new
                          {
                              t.Id,
                              m.MessageText,
                              t.UpdateAt,
                              t.CreateAt,
                              StatusName = st.Name,
                              EmployeeName = e.FirstName + " " + e.LastName,
                              PriorityName = p.Name,
                              CategoryName = ct.Name
                          });
            return ticket;
        }
        public IQueryable ViewTicketDatabase()
        {
            var ticket = (from t in myContext.Tickets
                          join m in myContext.Messages on t.Id equals m.TicketId
                          join e in myContext.Employees on t.NIK equals e.NIK
                          join st in myContext.Statuses on t.StatusId equals st.Id
                          join ct in myContext.Categories on t.CategoryId equals ct.Id
                          join p in myContext.Priorities on t.PriorityId equals p.Id
                          where t.PriorityId == 3
                          select new
                          {
                              t.Id,
                              m.MessageText,
                              t.UpdateAt,
                              t.CreateAt,
                              StatusName = st.Name,
                              EmployeeName = e.FirstName + " " + e.LastName,
                              PriorityName = p.Name,
                              CategoryName = ct.Name
                          });
            return ticket;
        }
        public IQueryable ViewTicketHistory()
        {
            var ticket = (from t in myContext.Tickets
                          join m in myContext.Messages on t.Id equals m.TicketId
                          join st in myContext.Statuses on t.StatusId equals st.Id
                          join ct in myContext.Categories on t.CategoryId equals ct.Id
                          join p in myContext.Priorities on t.PriorityId equals p.Id
                          select new
                          {
                              t.Id,
                              m.MessageText,
                              t.UpdateAt,
                              t.CreateAt,
                              StatusName = st.Name,
                              PriorityName = p.Name,
                              CategoryName = ct.Name
                          });
            return ticket;
        }

        public IQueryable ViewMessageDetail(int ID)
        {
            var ticket = (from m in myContext.Messages
                          join md in myContext.MessageDetails on m.Id equals md.MessageId
                          join e in myContext.Employees on md.NIK equals e.NIK
                          join a in myContext.Accounts on md.NIK equals a.NIK
                          join ar in myContext.AccountRoles on md.NIK equals ar.AccountId
                          join r in myContext.Roles on ar.RoleId equals r.Id
                          where m.Id == ID
                          select new
                          {
                              md.MessageText,
                              r.Name,
                              fullName = e.FirstName+" "+e.LastName,
                              CreateAt = TimeAgo(md.CreateAt)
                          });
            return ticket;
        }

        public static string TimeAgo(DateTime dateTime)
        {
            string result = string.Empty;
            var timeSpan = DateTime.Now.Subtract(dateTime);

            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                result = string.Format("{0} seconds ago", timeSpan.Seconds);
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                result = timeSpan.Minutes > 1 ?
                    String.Format("{0} minutes ago", timeSpan.Minutes) :
                    "about a minute ago";
            }
            else if (timeSpan <= TimeSpan.FromHours(24))
            {
                result = timeSpan.Hours > 1 ?
                    String.Format("{0} hours ago", timeSpan.Hours) :
                    "about an hour ago";
            }
            else if (timeSpan <= TimeSpan.FromDays(30))
            {
                result = timeSpan.Days > 1 ?
                    String.Format("{0} days ago", timeSpan.Days) :
                    "yesterday";
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                result = timeSpan.Days > 30 ?
                    String.Format("{0} months ago", timeSpan.Days / 30) :
                    "about a month ago";
            }
            else
            {
                result = timeSpan.Days > 365 ?
                    String.Format("{0} years ago", timeSpan.Days / 365) :
                    "about a year ago";
            }
            return result;
        }
    }
}
