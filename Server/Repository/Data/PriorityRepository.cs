﻿using Server.Context;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Repository.Data
{
    public class PriorityRepository : GeneralRepository<MyContext, Priority, int>
    {
        public PriorityRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
