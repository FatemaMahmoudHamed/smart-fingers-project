using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnamelTask.Services.UserServices.General
{
    public class GeneralService : IGeneralService
    {
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public GeneralService(
                 
                 IMapper mapper,
                 RoleManager<IdentityRole> roleManager
                 
                 )
        {
            _mapper = mapper;
            _roleManager = roleManager;
           
        }


    }
}
