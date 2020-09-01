using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Anamel.API.Controllers
{
    public class BaseController : Controller
    {
        private IHttpContextAccessor _httpContextAccessor;

        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string LoggedInUserId { get { return (User.FindFirstValue(ClaimTypes.NameIdentifier)); } }
        public string LoggedInUserName { get { return (_httpContextAccessor.HttpContext.User.Identity.Name).ToString(); } }
   


    }
}