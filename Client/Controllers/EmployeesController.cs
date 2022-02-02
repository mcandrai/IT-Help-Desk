using Client.Base;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using Server.Model;
using Server.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            employeeRepository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }
        //api client for registration

        [HttpPost("employees/register")]
        public JsonResult Register(RegisterVM register)
        {
            var result = employeeRepository.Register(register);
            return Json(result);
        }

        [HttpGet("employees/Get-Register-All/{nik}")]
        public async Task<JsonResult> GetRegisterData(string nik)
        {
            var result = await employeeRepository.GetRegisterData(nik);
            return Json(result);
        }
    }
}
