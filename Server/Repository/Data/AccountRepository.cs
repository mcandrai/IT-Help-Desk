using Server.Context;
using Server.Model;
using Server.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
