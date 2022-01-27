using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Server.Model;
using Server.Repository.Data;
using Server.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        public IConfiguration _configuration;
        public AccountsController(AccountRepository accountRepository, IConfiguration configuration) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            this._configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginVM login)
        {

            try
            {
                bool isEmail = accountRepository.CheckEmail(login);

                if (!isEmail)
                {
                    return Ok(new JwToken { status = HttpStatusCode.NotFound, idToken = null, message = "Account not found!" });
                }

                bool isLogin = accountRepository.Login(login);

                if (isLogin)
                {
                    string employeeRole = accountRepository.GetRole(login.Email);
                    var claims = new List<Claim>
                    {
                        new Claim("email", login.Email),
                        new Claim("role", employeeRole)
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(10),
                            signingCredentials: signIn
                        );
                    var idToken = new JwtSecurityTokenHandler().WriteToken(token);
                    claims.Add(new Claim("Token Security", idToken.ToString()));
                    return Ok(new JwToken { status = HttpStatusCode.OK, idToken = idToken, message = "Successful login!" });
                }
                else
                {
                    return Ok(new JwToken { status = HttpStatusCode.Forbidden, idToken = null, message = "Your password is invalid, Please try again!" });
                }

            }
            catch (Exception)
            {
                return BadRequest(new JwToken { status = HttpStatusCode.InternalServerError, idToken = null, message = "Something has gone wrong!" });
            }
        }

        [HttpPost]
        [Route("Forgot-Password")]
        public ActionResult ForgotPassword(ForgotPasswordVM forgotPassword)
        {
            try
            {
                bool isEmail = accountRepository.CheckEmail(forgotPassword.Email);
                if (!isEmail)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "Account not found!" });
                }
                else
                {
                    accountRepository.SendOTP(forgotPassword.Email);
                    return Ok(new { status = HttpStatusCode.OK, result = 1, message = "OTP code sent to your email, Please check inbox or spam!" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { status = HttpStatusCode.InternalServerError, result = 0, message = "Something has gone wrong!" });
            }
        }

        [HttpPost]
        [Route("Change-Password")]
        public ActionResult ChangePassword(ForgotPasswordVM forgotPassword)
        {
            try
            {
                string password = forgotPassword.Password;
                
                if (String.IsNullOrEmpty(password))
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "Password cannot be empty!" });
                }

                bool isEmail = accountRepository.CheckEmail(forgotPassword.Email);

                if (!isEmail)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "Account not found!" });
                }

                bool isValidOTP = accountRepository.CheckExpiredOTP(forgotPassword.Email);

                if (!isValidOTP)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "OTP code has expired, Please request again!" });
                }
                else
                {
                    bool isChangePassword = accountRepository.ChangePassword(forgotPassword);

                    if (isChangePassword)
                    {
                        return Ok(new { status = HttpStatusCode.OK, result = 1, message = "Success change password!" });
                    }
                    else
                    {
                        return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "OTP code does not match, Please try again!" });
                    }
                }

            }
            catch (Exception)
            {
                return BadRequest(new { status = HttpStatusCode.InternalServerError, result = 0, message = "Something has gone wrong!" });
            }
        }

    }
}

