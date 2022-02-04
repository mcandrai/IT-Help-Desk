using Client.Base;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Server.Model;
using Server.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class TicketRepository : GeneralRepository<Ticket, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        private readonly IWebHostEnvironment webHostEnvironment;
        public TicketRepository(Address address, IWebHostEnvironment hostEnvironment, string request = "tickets/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            webHostEnvironment = hostEnvironment;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };

        }
        public Object CreateTicket(TicketDetailVM ticketDetailVM)
        {
            string uniqueFileName = null;

            if (ticketDetailVM.ProblemPicture != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + ticketDetailVM.ProblemPicture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ticketDetailVM.ProblemPicture.CopyTo(fileStream);
                }
            }
            Object ticket = new TicketDetailVM
            {
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                StatusId = 1,
                PriorityId = 4,
                CategoryId = ticketDetailVM.CategoryId,
                NIK = ticketDetailVM.NIK,
                ImgProblem = uniqueFileName,
                Message = ticketDetailVM.Message,
                EscalationId = 1
            };
            StringContent content = new StringContent(JsonConvert.SerializeObject(ticket), Encoding.UTF8, "application/json");
            Object entities = new Object();
            using (var response = httpClient.PostAsync(request + "Create-Ticket", content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                entities = JsonConvert.DeserializeObject<Object>(apiResponse);
            }

            return entities;
        }

        public Object UpdateTicket(TicketDetailVM ticketDetailVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(ticketDetailVM), Encoding.UTF8, "application/json");
            Object entities = new Object();
            using (var response = httpClient.PostAsync(request + "Update-Ticket-HelpDesk", content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                entities = JsonConvert.DeserializeObject<Object>(apiResponse);
            }

            return entities;
        }

        public Object UpdateTicketBug(TicketDetailVM ticketDetailVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(ticketDetailVM), Encoding.UTF8, "application/json");
            Object entities = new Object();
            using (var response = httpClient.PostAsync(request + "Update-Ticket-BugSystem", content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                entities = JsonConvert.DeserializeObject<Object>(apiResponse);
            }

            return entities;
        }
        public Object EscalationTicketBug(TicketDetailVM ticketDetailVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(ticketDetailVM), Encoding.UTF8, "application/json");
            Object entities = new Object();
            using (var response = httpClient.PostAsync(request + "Escalation-Ticket-BugSystem", content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                entities = JsonConvert.DeserializeObject<Object>(apiResponse);
            }

            return entities;
        }

        public Object UpdateTicketDatabase(TicketDetailVM ticketDetailVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(ticketDetailVM), Encoding.UTF8, "application/json");
            Object entities = new Object();
            using (var response = httpClient.PostAsync(request + "Update-Ticket-Database", content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                entities = JsonConvert.DeserializeObject<Object>(apiResponse);
            }

            return entities;
        }

        public Object UpdateTicketBugSystem(TicketDetailVM ticketDetailVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(ticketDetailVM), Encoding.UTF8, "application/json");
            Object entities = new Object();
            using (var response = httpClient.PostAsync(request + "Update-Ticket-BugSystem", content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                entities = JsonConvert.DeserializeObject<Object>(apiResponse);
            }

            return entities;
        }

        public Object UpdateTicketDone(TicketDetailVM ticketDetailVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(ticketDetailVM), Encoding.UTF8, "application/json");
            Object entities = new Object();
            using (var response = httpClient.PostAsync(request + "Update-Ticket-Done", content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                entities = JsonConvert.DeserializeObject<Object>(apiResponse);
            }

            return entities;
        }

        public async Task<List<TicketRequestVM>> ViewTicketHistoryUser(string nik)
        {
            List<TicketRequestVM> entities = new List<TicketRequestVM>();

            using (var response = await httpClient.GetAsync(request + "View-Ticket-History-User/" + nik))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<TicketRequestVM>>(apiResponse);
            }
            return entities;
        }

        public async Task<List<TicketRequestVM>> ViewTicketHistory()
        {
            List<TicketRequestVM> entities = new List<TicketRequestVM>();

            using (var response = await httpClient.GetAsync(request + "View-Ticket-History"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<TicketRequestVM>>(apiResponse);
            }
            return entities;
        }

        public async Task<List<TicketRequestVM>> ViewTicketBugSystem()
        {
            List<TicketRequestVM> entities = new List<TicketRequestVM>();

            using (var response = await httpClient.GetAsync(request + "View-Ticket-BugSystem"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<TicketRequestVM>>(apiResponse);
            }
            return entities;
        }

        public async Task<List<TicketRequestVM>> ViewTicketHelpDesk()
        {
            List<TicketRequestVM> entities = new List<TicketRequestVM>();

            using (var response = await httpClient.GetAsync(request + "View-Ticket-HelpDesk"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<TicketRequestVM>>(apiResponse);
            }
            return entities;
        }

        public async Task<List<TicketRequestVM>> ViewTicketDatabase()
        {
            List<TicketRequestVM> entities = new List<TicketRequestVM>();

            using (var response = await httpClient.GetAsync(request + "View-Ticket-Database"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<TicketRequestVM>>(apiResponse);
            }
            return entities;
        }

        public async Task<List<TicketRequestVM>> ViewTicketUser(string nik)
        {
            List<TicketRequestVM> entities = new List<TicketRequestVM>();

            using (var response = await httpClient.GetAsync(request + "View-Ticket-User/" + nik))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<TicketRequestVM>>(apiResponse);
            }
            return entities;
        }


        public async Task<TicketMessage> ViewTicketDetail(int id)
        {
            TicketMessage entity = null;

            using (var response = await httpClient.GetAsync(request + "View-Ticket-Detail/" + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<TicketMessage>(apiResponse);
            }
            return entity;
        }

        public async Task<List<MessageDetailVM>> ViewMessageDetail(int Id)
        {
            List<MessageDetailVM> entities = new List<MessageDetailVM>();

            using (var response = await httpClient.GetAsync(request + "View-Message-Detail/" + Id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<MessageDetailVM>>(apiResponse);
            }
            return entities;
        }
    }
}
