using Client.Base;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class CategoriesController : BaseController<ProblemCategory, CategoryRepository, string>
    {
        private readonly CategoryRepository categoryRepository;

        public CategoriesController(CategoryRepository repository) : base(repository)
        {
            categoryRepository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
