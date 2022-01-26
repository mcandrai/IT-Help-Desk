using Microsoft.EntityFrameworkCore;
using Server.Context;
using Server.Model;
using Server.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Server.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext myContext;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public bool CheckEmail(LoginVM login)
        {
            var getEmail = myContext.Accounts.Where(a => a.Email == login.Email).FirstOrDefault();

            if (getEmail == null)
            {
                return false;
            }

            return true;
        }

        public bool CheckEmail(string Email)
        {
            var getEmail = myContext.Accounts.Where(a => a.Email == Email).FirstOrDefault();

            if (getEmail == null)
            {
                return false;
            }
            return true;
        }

        public bool Login(LoginVM login)
        {
            var getAccount = myContext.Accounts.Where(a => a.Email == login.Email).FirstOrDefault();

            if (getAccount == null)
            {
                return false;
            }
            else if (BCrypt.Net.BCrypt.Verify(login.Password, getAccount.Password))
            {
                return true;
            }
            return false;
        }

        public string GetRole(string email)
        {
            var getAccount = myContext.Accounts.FirstOrDefault(a => a.Email == email);

            var result = myContext.AccountRoles.Where(ar => ar.AccountId == getAccount.NIK).Select(ar => ar.Role.Name).ToList();

            return string.Join(",", result);
        }

        public int GenerateOTP()
        {
            Random random = new Random();
            int randomOTP = random.Next(100000, 999999);
            return randomOTP;
        }

        public bool SendOTP(string Email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("mccreg61net@gmail.com", "61mccregnet"),
                EnableSsl = true
            };

            DateTime nowTime = DateTime.Now;
            DateTime expiredToken = nowTime.AddMinutes(5);

            int randomOTP = GenerateOTP();
            string bodyMessage = $"To reset your password, use code OTP : {randomOTP} \n\n Expired : {expiredToken} .";
            client.Send("mccreg61net@gmail.com", Email, "Help Dek - Reset Password", bodyMessage);

            UpdateAccountOTP(Email, randomOTP, expiredToken);
            return true;
        }

        public void UpdateAccountOTP(string Email, int randomOTP, DateTime expiredToken)
        {
            var getAccount = myContext.Accounts.Where(a => a.Email == Email).FirstOrDefault();
            var account = new Account()
            {
                NIK = getAccount.NIK,
                Email = Email,
                Password = getAccount.Password,
                OTPCode = randomOTP,
                OTPStatus = true,
                OTPExpired = expiredToken
            };
            myContext.Entry(getAccount).State = EntityState.Detached;
            myContext.Entry(account).State = EntityState.Modified;
            myContext.SaveChanges();
        }

        public bool CheckExpiredOTP(string Email)
        {
            var getAccount = myContext.Accounts.FirstOrDefault(a => a.Email == Email);

            DateTime dateOTP = getAccount.OTPExpired;
            DateTime dateNow = DateTime.Now;

            int compareDate = DateTime.Compare(dateNow, dateOTP);

            if (compareDate > 0)
            {
                return false;
            }
            return true;
        }

        public bool ChangePassword(ForgotPasswordVM forgotPassword)
        {
            var getAccount = myContext.Accounts.FirstOrDefault(a=>a.Email == forgotPassword.Email);

            int employeeOTP = getAccount.OTPCode;
            int employeeInputOTP = forgotPassword.OTPCode;

            bool isActiveOTP = getAccount.OTPStatus;

            if (employeeInputOTP != employeeOTP || isActiveOTP == false)
            {
                return false;
            }

            string password = BCrypt.Net.BCrypt.HashPassword(forgotPassword.Password);

            var account = new Account
            {
                NIK = getAccount.NIK,
                Email = forgotPassword.Email,
                Password = password,
                OTPCode = 0,
                OTPStatus = false,
                OTPExpired = getAccount.OTPExpired
            };
            myContext.Entry(getAccount).State = EntityState.Detached;
            myContext.Entry(account).State = EntityState.Modified;
            myContext.SaveChanges();
            return true;
        }
    }
}
