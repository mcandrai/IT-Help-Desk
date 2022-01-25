using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Model;
using Server.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController<ProblemCategory, CategoryRepository, int>
    {
        private readonly CategoryRepository categoryRepository;

        public CategoriesController(CategoryRepository categoryRepository) : base(categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
    }
}
