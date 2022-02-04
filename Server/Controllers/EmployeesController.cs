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
        [Route("register")]
        public ActionResult<RegisterVM> RegisterEmployee(RegisterVM register)
        {
            try
            {
                bool isDuplicateEmail = employeeRepository.DuplicateEmailValue(register);
                if (isDuplicateEmail)
                {
                    return BadRequest(new { status = HttpStatusCode.Conflict, message = "Email has already been taken!" });
                }

                bool isDuplicatePhone = employeeRepository.DuplicatePhoneValue(register);

                if (isDuplicatePhone)
                {
                    return BadRequest(new { status = HttpStatusCode.Conflict, message = "Phone Number has already been taken!" });
                }
                else
                {
                    employeeRepository.RegisterEmployee(register);
                    return Ok(new { status = HttpStatusCode.OK, message = "Your account has been successfully created!" });
                }

            }
            catch (Exception)
            {
                return BadRequest(new { status = HttpStatusCode.InternalServerError, message = "Something has gone wrong!" });
            }

        }


        [HttpGet("Get-Register-All/{NIK}")]
        public virtual ActionResult<RegisterVM> GetRegisterAll(string NIK)
        {
            var result = employeeRepository.GetRegisterAll(NIK);
            return Ok(result);
        }

        [HttpGet]
        [Route("Get-Register-Detail/{NIK}")]
        public virtual ActionResult<RegisterVM> GetRegisterDetail(string NIK)
        {
            var result = employeeRepository.GetRegisterDetail(NIK);
            return result;
        }

        [HttpPut]
        [Route("Update-Register")]
        public ActionResult<RegisterVM> RegisterUpdate(GetRegisterVM register)
        {
            /*var result = employeeRepository.UpdateRegister(register);
            return Ok(result);*/
            try
            {
                bool isDuplicateEmail = employeeRepository.UpdateEmail(register);
                if (isDuplicateEmail)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "Email already used!" });
                }

                bool isDuplicatePhone = employeeRepository.UpdatePhone(register);

                if (isDuplicatePhone)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "Phone Number already used!" });
                }
                else
                {
                    employeeRepository.UpdateRegister(register);
                    return Ok(new { status = HttpStatusCode.OK, result = 1, message = "Successfully update data!" });
                }

            }
            catch (Exception e)
            {
                return BadRequest(new { status = HttpStatusCode.InternalServerError, result = e, message = "Something has gone wrong!" });
            }
        }

        [HttpDelete]
        [Route("Delete-Register")]
        public ActionResult<RegisterVM> DeleteRegisterData(RegisterVM register)
        {
            try
            {
                bool result = employeeRepository.DeleteRegister(register);
                return Ok(new { status = HttpStatusCode.OK, result = 1, message = "Successfully delete data" });
            }
            catch (Exception e)
            {
                return BadRequest(new { status = HttpStatusCode.InternalServerError, result = e, message = "Something has gone wrong" });
            }
        }

        [HttpGet]
        [Route("System/Report")]
        public ActionResult<ReportVM> ReportDataAll()
        {
            try
            {
                var result = employeeRepository.AllReport();
                return Ok(new { status = HttpStatusCode.OK,  message = "Successfully get data!", data = result });
            }
            catch (Exception )
            {
                return BadRequest(new { status = HttpStatusCode.InternalServerError, message = "Something has gone wrong" });
            }
        }

       
        [HttpGet]
        [Route("System/Report-Priority")]
        public ActionResult<PriorityVM> GetTicketPriority()
        {
            try
            {
                var result = employeeRepository.GetTicketPriority();
                return Ok(new { status = HttpStatusCode.OK, message = "Successfully get data!", data = result });
            }
            catch (Exception )
            {
                return BadRequest(new { status = HttpStatusCode.InternalServerError,  message = "Something has gone wrong!" });
            }
        }

        [HttpGet]
        [Route("System/Report-Statuses")]
        public ActionResult<StatusVM> GetTicketStatus()
        {
            try
            {
                var result = employeeRepository.GetTicketStatus();
                return Ok(new { status = HttpStatusCode.OK, message = "Successfully get data!", data = result });
            }
            catch (Exception)
            {
                return BadRequest(new { status = HttpStatusCode.InternalServerError, message = "Something has gone wrong!" });
            }
        }

    }
}
