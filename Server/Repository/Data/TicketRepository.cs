using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Server.Context;
using Server.Model;
using Server.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Server.Repository.Data
{
    public class TicketRepository : GeneralRepository<MyContext, Ticket, int>
    {
        private readonly MyContext myContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        public TicketRepository(MyContext myContext,IWebHostEnvironment hostEnvironment) : base(myContext)
        {
            this.myContext = myContext;
            webHostEnvironment = hostEnvironment;
        }
        public int CreateTicket(TicketDetailVM ticketDetailVM)
        {
            var requestTicket = new Ticket
            {
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                StatusId = 1,
                PriorityId = 4,
                EscalationId = 1,
                CategoryId = ticketDetailVM.CategoryId,
                NIK = ticketDetailVM.NIK,
                ProblemPicture = ticketDetailVM.ImgProblem
            };
            myContext.Tickets.Add(requestTicket);
            myContext.SaveChanges();
            var message = new Message
            {
                MessageText = ticketDetailVM.Message,
                TicketId = requestTicket.Id
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
                PriorityId = ticketDetailVM.PriorityId,
                EscalationId = getTicket.EscalationId,
                NIK = getTicket.NIK,
                ProblemPicture = getTicket.ProblemPicture
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
                PriorityId = getTicket.PriorityId,
                EscalationId = 2,
                NIK = getTicket.NIK,
                ProblemPicture = getTicket.ProblemPicture
            };
            myContext.Entry(getTicket).State = EntityState.Detached;
            myContext.Entry(ticket).State = EntityState.Modified;
            return myContext.SaveChanges();
        }
        public int EscalationTickettoDatabase(TicketDetailVM ticketDetailVM)
        {
            var getTicket = myContext.Tickets.FirstOrDefault(a => a.Id == ticketDetailVM.Id);
            var ticket = new Ticket
            {
                Id = getTicket.Id,
                CreateAt = getTicket.CreateAt,
                UpdateAt = DateTime.Now,
                StatusId = getTicket.StatusId,
                CategoryId = getTicket.CategoryId,
                PriorityId = getTicket.PriorityId,
                EscalationId = 3,
                NIK = getTicket.NIK,
                ProblemPicture = getTicket.ProblemPicture
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
                StatusId = 4,
                PriorityId = getTicket.PriorityId,
                NIK = getTicket.NIK,
                EscalationId = getTicket.EscalationId,
                ProblemPicture = getTicket.ProblemPicture
            };
            myContext.Entry(getTicket).State = EntityState.Detached;
            myContext.Entry(ticket).State = EntityState.Modified;
            return myContext.SaveChanges();
        }

        public bool SendReport(int Id)
        {
            var getTicket = myContext.Tickets.FirstOrDefault(t => t.Id == Id);
            var getEmail = myContext.Accounts.FirstOrDefault(a => a.NIK == getTicket.NIK);
            var getName = myContext.Employees.FirstOrDefault(e => e.NIK == getTicket.NIK);
            var getMessage = myContext.Messages.FirstOrDefault(m => m.TicketId == getTicket.Id);
            var getCategory = myContext.Categories.FirstOrDefault(c => c.Id == getTicket.CategoryId);
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("mccreg61net@gmail.com", "61mccregnet"),
                EnableSsl = true
            };

            DateTime nowTime = DateTime.Now;
            using (var message = new MailMessage("mccreg61net@gmail.com", getEmail.Email)
            {
                Subject = "Report Ticket "+getName.FirstName+" "+getName.LastName,
                Body = 
                "Kepada, "
                +getName.FirstName + " " + getName.LastName+ 
                "<br> Terima kasih telah menggunakan layanan Help Desk kami,Tiket anda : <pre>" +
                " Tanggal Request   :" +getTicket.CreateAt+
                "<br> Kategori Masalah  :" +getCategory.Name+ "</br>" +
                "<br> Keterangan        :" + getMessage.MessageText+ "</br></pre>" +
                "<br><span style="+"background-color:#00c851;color:white;padding:10px;border-radius:10px;letter-spacing:5px;font-weight:600;"+">DONE</span>" +
                "<br><br>Catatan : <br><p> Untuk support lebih lanjut bisa menghubungi kita di 085 - 320 - 119 - 930.</p>" +
                "<br> Hormat Kami,</br>" +
                "<br> Help Desk </br>",
                     IsBodyHtml = true,
            })client.Send(message);
            return true;
        }

        public bool UpdateTicketDone(TicketDetailVM ticketDetailVM)
        {
            var getTicket = myContext.Tickets.FirstOrDefault(a => a.Id == ticketDetailVM.Id);
            if (getTicket.StatusId != 4) {
                return false;
            } 
            var ticket = new Ticket
            {
                Id = getTicket.Id,
                CreateAt = getTicket.CreateAt,
                UpdateAt = DateTime.Now,
                CategoryId = getTicket.CategoryId,
                StatusId = 5,
                PriorityId = getTicket.PriorityId,
                EscalationId = getTicket.EscalationId,
                NIK = getTicket.NIK,
                ProblemPicture = getTicket.ProblemPicture
            };
            myContext.Entry(getTicket).State = EntityState.Detached;
            myContext.Entry(ticket).State = EntityState.Modified;
            SendReport(ticket.Id);
            myContext.SaveChanges();
            return true;
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
                Message = query.Message.MessageText,
                Image = query.ProblemPicture
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
                          orderby st.Id ascending, p.Id descending
                          where  a.NIK== NIK && st.Id != 5
                          select new
                          {
                              t.Id,
                              m.MessageText,
                              t.UpdateAt,
                              t.CreateAt,
                              StatusName = st.Name,
                              CategoryName = ct.Name,
                              EmployeeName = e.FirstName + " " + e.LastName,
                              PriorityName = p.Name
                          });
            return ticket;
        }

        public IQueryable ViewTicketHelpDesk()
        {
            var ticket = (from t in myContext.Tickets
                          join m in myContext.Messages on t.Id equals m.TicketId
                          join e in myContext.Employees on t.NIK equals e.NIK
                          join a in myContext.Accounts on t.NIK equals a.NIK
                          join st in myContext.Statuses on t.StatusId equals st.Id
                          join ct in myContext.Categories on t.CategoryId equals ct.Id
                          join p in myContext.Priorities on t.PriorityId equals p.Id
                          join esc in myContext.Escalations on t.EscalationId equals esc.Id
                          orderby st.Id ascending,p.Id descending
                          where st.Id != 5
                          select new
                          {
                              t.Id,
                              m.MessageText,
                              t.UpdateAt,
                              t.CreateAt,
                              EscalationName = esc.Name,
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
                          join esc in myContext.Escalations on t.EscalationId equals esc.Id
                          orderby st.Id ascending, p.Id descending
                          where esc.Id==2 && st.Id!=5
                          select new
                          {
                              t.Id,
                              m.MessageText,
                              t.UpdateAt,
                              t.CreateAt,
                              StatusName = st.Name,
                              EscalationName = esc.Name,
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
                          join esc in myContext.Escalations on t.EscalationId equals esc.Id
                          orderby st.Id ascending, p.Id descending
                          where esc.Id == 3 && st.Id != 5
                          select new
                          {
                              t.Id,
                              m.MessageText,
                              t.UpdateAt,
                              t.CreateAt,
                              StatusName = st.Name,
                              EscalationName = esc.Name,
                              EmployeeName = e.FirstName + " " + e.LastName,
                              PriorityName = p.Name,
                              CategoryName = ct.Name
                          });
            return ticket;
        }
        public IQueryable ViewTicketHistoryUser(string NIK)
        {
            var ticket = (from t in myContext.Tickets
                          join m in myContext.Messages on t.Id equals m.TicketId
                          join e in myContext.Employees on t.NIK equals e.NIK
                          join st in myContext.Statuses on t.StatusId equals st.Id
                          join ct in myContext.Categories on t.CategoryId equals ct.Id
                          join p in myContext.Priorities on t.PriorityId equals p.Id
                          orderby t.CreateAt descending
                          where e.NIK == NIK && st.Id==5
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
                          join e in myContext.Employees on t.NIK equals e.NIK
                          join st in myContext.Statuses on t.StatusId equals st.Id
                          join ct in myContext.Categories on t.CategoryId equals ct.Id
                          join p in myContext.Priorities on t.PriorityId equals p.Id
                          orderby t.CreateAt descending
                          where st.Id == 5
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

        public IQueryable ViewMessageDetail(int ID)
        {
            var ticket = (from m in myContext.Messages
                          join md in myContext.MessageDetails on m.Id equals md.MessageId
                          join e in myContext.Employees on md.NIK equals e.NIK
                          join a in myContext.Accounts on md.NIK equals a.NIK
                          join ar in myContext.AccountRoles on md.NIK equals ar.AccountId
                          join r in myContext.Roles on ar.RoleId equals r.Id
                          orderby md.CreateAt ascending
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
