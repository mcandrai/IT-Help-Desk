using Server.Context;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Repository.Data
{
    public class MessageRepository : GeneralRepository<MyContext, Message, int>
    {
        public MessageRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
