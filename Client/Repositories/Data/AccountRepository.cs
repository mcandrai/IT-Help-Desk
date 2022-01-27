using Client.Base;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Server.Model;
using Server.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class AccountRepository : GeneralRepository<Account, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        public AccountRepository(Address address, string request = "accounts/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };

        }
        public async Task<JwToken> Auth(LoginVM login)
        {
            JwToken token = null;

            StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(request + "login", content);

            string apiResponse = await result.Content.ReadAsStringAsync();
            token = JsonConvert.DeserializeObject<JwToken>(apiResponse);

            return token;
        }

        public async Task<GetDataLogin> GenerateJWTNIK()
        {
            var content = new GetDataLogin();
            var token = _contextAccessor.HttpContext.Session.GetString("JWToken");
            var result = new JwtSecurityTokenHandler().ReadJwtToken(token);

            content.NIK = result.Claims.First(claim => claim.Type == "nik").Value;
            content.Name = result.Claims.First(claim => claim.Type == "name").Value;
            content.Email = result.Claims.First(claim => claim.Type == "email").Value;
            content.Role = result.Claims.First(claim => claim.Type == "role").Value;
            return content;
        }


    }
}
