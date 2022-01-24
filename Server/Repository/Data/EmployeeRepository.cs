using Castle.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using Server.Context;
using Server.Model;
using Server.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        public IConfiguration _configuration;
        private readonly MyContext myContext;
        //Hash hash = new Hash();
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
            /*this._configuration = configuration;*/
        }
        public string GenerateNIK()
        {
            string nowYear = DateTime.Now.Year.ToString();
            int countEmployee = myContext.Employees.ToList().Count;
            string result;
            if (countEmployee < 1)
            {
                return result = nowYear + "0" + (countEmployee + 1).ToString();
            }
            else
            {
                var maxId = myContext.Employees.Max(e => e.NIK);
                int setId = Int32.Parse(maxId) + 1;
                return result = setId.ToString();
            }

        }
        
        public bool DuplicateEmailValue(RegisterVM register)
        {
            int getEmail = myContext.Accounts.Where(a => a.Email == register.Email).Count();
            if (getEmail > 0)
            {
                return true;
            }
            return false;
        }

        public bool DuplicatePhoneValue(RegisterVM register)
        {
            int getPhone = myContext.Employees.Where(e => e.Phone == register.Phone).Count();
            if (getPhone > 0)
            {
                return true;
            }
            return false;
        }
        public int RegisterEmployee(RegisterVM register)
        {

            var employee = new Employee
            {
                NIK = GenerateNIK(),
                FirstName = register.FirstName,
                LastName = register.LastName,
                Phone = register.Phone,
                Gender = (Model.Gender)register.Gender
            };

            myContext.Employees.Add(employee);
            myContext.SaveChanges();

            string hashPassword = BCrypt.Net.BCrypt.HashPassword(register.Password);
            var account = new Account
            {
                NIK = employee.NIK,
                Email = register.Email,
                Password = hashPassword,
            };
            myContext.Accounts.Add(account);
            myContext.SaveChanges();

            var accountrole = new AccountRole
            {
                AccountId = employee.NIK,
                RoleId = 3,
            };

            myContext.AccountRoles.Add(accountrole);
            myContext.SaveChanges();

            return myContext.SaveChanges();
        }

        public RegisterVM GetRegisterAll()
        {

            var query = myContext.Employees.Include(a => a.Account).FirstOrDefault();
            if (query == null)
            {
                return null;
            }
            var getData = new RegisterVM
            {
                NIK = query.NIK,
                FirstName = query.FirstName,
                LastName = query.LastName,
                Phone = query.Phone,
                Email = query.Account.Email,
                Gender = query.Gender,
                AccountRoles = myContext.AccountRoles.Where(accountrole => accountrole.AccountId == query.NIK).Select(accountrole => accountrole.Role.Name).ToList()
            };
            return getData;

        }
    }
}
