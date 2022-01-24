using Microsoft.AspNetCore.Mvc;
using Server.Model;
using Server.Repository.Data;
using Server.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;

        public EmployeesController(EmployeeRepository employeeRepository) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;

        }
        [HttpPost]
        [Route("Register")]
        public ActionResult<RegisterVM> RegisterEmployee(RegisterVM register)
        {
            try
            {
                bool isDuplicateEmail = employeeRepository.DuplicateEmailValue(register);
                if (isDuplicateEmail)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "Email already used!" });
                }

                bool isDuplicatePhone = employeeRepository.DuplicatePhoneValue(register);

                if (isDuplicatePhone)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "Phone Number already used!" });
                }
                else
                {
                    employeeRepository.RegisterEmployee(register);
                    return Ok(new { status = HttpStatusCode.OK, result = 1, message = "Successfully added data!" });
                }

            }
            catch (Exception e)
            {
                return BadRequest(new { status = HttpStatusCode.InternalServerError, result = e, message = "Something has gone wrong!" });
            }

            /*var result = employeeRepository.RegisterEmployee(register);
            return Ok(new { status = HttpStatusCode.OK, result = 1, message = "Successfully added data!" });*/

        }

        [HttpGet]
        [Route("GetRegisterAll")]
        public virtual ActionResult<RegisterVM> Detail()
        {
            var result = employeeRepository.GetRegisterAll();
            return Ok(result);
        }


    }
}
