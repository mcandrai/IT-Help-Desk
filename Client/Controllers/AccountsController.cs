using Client.Base;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Model;
using Server.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;

        public AccountsController(AccountRepository repository) : base(repository)
        {
            accountRepository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("accounts/login")]
        public async Task<IActionResult> Auth(LoginVM login)
        {
            var JwToken = await accountRepository.Auth(login);
            var token = JwToken.idToken;
            
            string status = JwToken.status.ToString();
            if (token == null)
            {
                TempData["status"] = status;
                TempData["message"] = JwToken.message;
                return RedirectToAction("index", "login");
            }
            TempData["nik"] = JwToken.nik;
            
            HttpContext.Session.SetString("JWToken", token);
            var nik = Json(JwToken.nik);
            return RedirectToAction("index", "user",nik);
        }

        /*[HttpGet("Accounts/GetSessionNIK")]
         public async Task<JsonResult> GetSessionNIK()
         {
             var result = HttpContext.Session.GetString("JWToken");
             return Json(result);
         }*/

        [HttpGet("Accounts/GenerateJWTNIK")]
        public async Task<JsonResult> GenerateJWTNIK()
        {
            //var result = await repository.UpdateEmployee(userVM);

            var result = await accountRepository.GenerateJWTNIK();
            return Json(result);
        }

        [HttpGet("Accounts/Logout")]
        public IActionResult Logout()
        {

            HttpContext.Session.Clear();

            return RedirectToAction("index", "login");
        }

    }

}
