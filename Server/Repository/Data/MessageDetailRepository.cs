using Microsoft.AspNetCore.Mvc;
using Server.Context;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Repository.Data
{
    public class MessageDetailRepository : GeneralRepository<MyContext, MessageDetail, int>
    {
        public MessageDetailRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
