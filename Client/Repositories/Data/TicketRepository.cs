using Client.Base;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Server.Model;
using Server.ViewModel;
using System;
using System.Collections.Generic;
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
        public TicketRepository(Address address, string request = "tickets/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };

        }
        public Object CreateTicket(TicketDetailVM ticketDetailVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(ticketDetailVM), Encoding.UTF8, "application/json");
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

        public async Task<TicketRequestVM> ViewTicketDetail(int id)
        {
            TicketRequestVM entity = null;

            using (var response = await httpClient.GetAsync(request + "View-Ticket-Detail/" + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<TicketRequestVM>(apiResponse);
            }
            return entity;
        }

    }
}
