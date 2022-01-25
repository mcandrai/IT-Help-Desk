using Server.Context;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Repository.Data
{
    public class TicketRepository : GeneralRepository<MyContext, Ticket, int>
    {
        public TicketRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
