using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnamelTask.Services.UserServices.User;
using AnamelTask.ViewModel.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Anamel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private IUserService _userService;

        public UserController(IUserService userService,IHttpContextAccessor httpContextAccessor):base(httpContextAccessor)
        {
            _userService = userService;
        }


        //api/user/register
        [HttpPost("Register")]
        public IActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var result = _userService.Register(model);

                if (result.Data!=null)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Some properties is not valid. ");  
        }

        //api/user/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.Login(model);
                if (result.Data!=null)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            return BadRequest("Some  properties is not valid. ");

        }
    }
}
