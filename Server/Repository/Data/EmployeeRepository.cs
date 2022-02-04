using Castle.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using Server.Context;
using Server.Model;
using Server.ViewModel;
using System;
using System.Collections;
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
        public bool UpdateEmail(GetRegisterVM register)
        {
            var getEmail = myContext.Accounts.Where(a => a.Email == register.Email).FirstOrDefault();
            if (getEmail == null)
            {
                return false;
            }
            else if (getEmail.NIK == register.NIK)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool UpdatePhone(GetRegisterVM register)
        {
            var getPhone = myContext.Employees.Where(e => e.Phone == register.Phone).FirstOrDefault();
            if (getPhone == null)
            {
                return false;
            }
            else if (getPhone.NIK == register.NIK)
            {
                return false;
            }
            else
            {
                return true;
            }
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

        public IQueryable GetRegisterAll(string NIK)
        {
            var query = (from employee in myContext.Set<Employee>()
                         join account in myContext.Set<Account>()
                            on employee.NIK equals account.NIK
                         join accountrole in myContext.Set<AccountRole>()
                            on employee.NIK equals accountrole.AccountId
                         join role in myContext.Set<Role>()
                            on accountrole.RoleId equals role.Id
                         orderby employee.FirstName
                         where employee.NIK==NIK
                         select new
                         {
                             employee.NIK,
                             FullName = employee.FirstName + " " + employee.LastName,
                             account.Email,
                             employee.Phone,
                             Gender = employee.Gender.ToString(),
                             Role = role.Name,
                         });
            return query;
        }

        public RegisterVM GetRegisterDetail(string NIK)
        {
            var query = myContext.Employees.Where(e => e.NIK == NIK).Include(a => a.Account).FirstOrDefault();
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

        public int UpdateRegister(GetRegisterVM register)
        {
            var names = register.FullName.Split(' ');
            var employee = myContext.Employees.FirstOrDefault(a => a.NIK == register.NIK);
            {
                employee.NIK = register.NIK;
                employee.FirstName = names[0];
                employee.LastName = names[1];
                employee.Phone = register.Phone;
                employee.Gender = register.Gender;
            };
            myContext.Entry(employee).State = EntityState.Modified;
            myContext.SaveChanges();

            var account = myContext.Accounts.FirstOrDefault(a => a.NIK == register.NIK);
            {
                account.Email = register.Email;
            };
            myContext.Entry(account).State = EntityState.Modified;
            return myContext.SaveChanges();
        }

        public bool DeleteRegister(RegisterVM register)
        {
            myContext.Remove(myContext.Employees.Single(e => e.NIK == register.NIK));
            myContext.SaveChanges();
            return true;
        }

        public ReportVM AllReport()
        {
            var emp = myContext.Employees.Count();
            var cat = myContext.Categories.Count();
            var tick = myContext.Tickets.Count();
            var prio = myContext.Priorities.Count();

            var getReport = new ReportVM
            {
                employees = emp,
                tickets = tick,
                categories = cat,
                priorities = prio
            };

            return getReport;
        }

        public IEnumerable<PriorityVM> GetTicketPriority()
        {
            var list = from tic in myContext.Tickets
                       join pri in myContext.Priorities on tic.PriorityId equals pri.Id
                       group pri by new { pri.Id, pri.Name } into Group
                       select new PriorityVM
                       {
                           PriorityId = Group.Key.Id,
                           PriorityName = Group.Key.Name,
                           BasePriority = Group.Count()
                       };

            return list.ToList();
        }

        public IEnumerable<StatusVM> GetTicketStatus()
        {
            var list = from tic in myContext.Tickets
                       join sta in myContext.Statuses on tic.StatusId equals sta.Id
                       group sta by new { sta.Id, sta.Name } into Group
                       select new StatusVM
                       {
                           StatusId = Group.Key.Id,
                           StatusName = Group.Key.Name,
                           BaseStatus = Group.Count()
                       };

            return list.ToList();
        }

    }
}
