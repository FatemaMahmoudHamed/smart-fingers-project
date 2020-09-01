using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anamel.API.Controllers;
using AnamelTask.Services.DataServices.Customers;
using AnamelTask.ViewModel.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Anamel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        private readonly ICustomerServices _customerServices;

        public CustomerController(ICustomerServices customerServices, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _customerServices = customerServices;
        }

        /// <summary>
        //  /api/customer/Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public IActionResult  CreateCustomer( CustomerVM model)
        {
            if(ModelState.IsValid)
            {
                model.Age = GetAge(model.Birthdate);
                var Result = _customerServices.CreateCustomer(model);
                if (Result.Result.Data!=null)
                    return Ok(Result.Result.Data);
            }
            return BadRequest("Some Proberties isinvalid.   ");
        }

        /// <summary>
        //  /api/User/GetAll
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var Result = _customerServices.GetAll();
            if (Result.Result.Data!=null)
                return Ok(Result.Result.Data);
            return BadRequest(Result);
        }

        /// <summary>
        //  /api/User/Get
        /// </summary>
        /// <param name="model">UserId</param>
        /// <returns></returns>
        [HttpGet("Get")]
        public IActionResult Get(int customerID)
        {
            var Result = _customerServices.GetCustomerByID(customerID);
            if (Result.Result.Data!=null)
                return Ok(Result.Result.Data);
            return BadRequest(Result);
        }

        /// <summary>
        //  /api/User/Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public IActionResult UpdateCustomer(CustomerVM model)
        {
            if (ModelState.IsValid)
            {
                model.Age = GetAge(model.Birthdate);
                var Result = _customerServices.UpdateCustomer(model);
                if (Result.Result.Data!=null)
                    return Ok(Result.Result.Data);
            }
            return BadRequest("Some Proberties isinvalid.   ");
        }

        /// <summary>
        //  /api/User/Delete
        /// </summary>
        /// <param name="model">UserId</param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public IActionResult Delete(int customerID)
        {
            var Result = _customerServices.DeleteCustomer(customerID);
            if (Result.Result.Data!=null)
                return Ok(Result.Result.Data);
            return BadRequest(Result);
        }

        public static Int32 GetAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;

            return (a - b) / 10000;
        }


    }
}
