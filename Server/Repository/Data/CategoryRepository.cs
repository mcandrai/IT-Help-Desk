using Server.Context;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Repository.Data
{
    public class CategoryRepository : GeneralRepository<MyContext, ProblemCategory, int>
    {
        public CategoryRepository(MyContext myContext) : base(myContext)
        {

        }

    }
}
