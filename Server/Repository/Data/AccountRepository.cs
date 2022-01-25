using Server.Context;
using Server.Model;
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
    }
}
