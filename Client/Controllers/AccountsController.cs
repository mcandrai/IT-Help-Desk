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

        //api client for login

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
            HttpContext.Session.SetString("JWToken", token);
            return RedirectToAction("index", "home");
        }

        //api client for get data user login

        [HttpGet("accounts/get-data-login")]
        public JsonResult LoginDetail()
        {
            var result = accountRepository.LoginDetail();
            return Json(result);
        }

        //api client for send otp

        [HttpPost("accounts/forgot-password")]
        public JsonResult Register(ForgotPasswordVM entity)
        {
            var result = accountRepository.ForgotPassword(entity);
            return Json(result);
        }

        //api client for change password

        [HttpPost("accounts/change-password")]
        public JsonResult ChangePassword(ForgotPasswordVM entity)
        {
            var result = accountRepository.ChangePassword(entity);
            return Json(result);
        }

        //api client for logout

        [HttpGet("accounts/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index", "login");
        }

    }

}
