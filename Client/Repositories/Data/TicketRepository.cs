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
    }
}
