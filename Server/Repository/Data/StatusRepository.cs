﻿using Server.Context;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Repository.Data
{
    public class StatusRepository : GeneralRepository<MyContext, Status, int>
    {
        public StatusRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}