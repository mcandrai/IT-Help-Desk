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
    public class EmployeeRepository : GeneralRepository<Employee, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        public EmployeeRepository(Address address, string request = "employees/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };

        }

        public Object Register(RegisterVM entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");

            Object entities = new object();

            using (var response = httpClient.PostAsync(address.link + request + "register", content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                entities = JsonConvert.DeserializeObject<Object>(apiResponse);
            }

            return entities;
        }

      
    }
}
