using AnamelTask.Services.UserServices.User;
using AnamelTask.UserData.DbModels;
using AnamelTask.ViewModel.GeneralResponse;
using AnamelTask.ViewModel.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AnamelTask.Services.UserServices.User
{
    public class UserService : IUserService
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private IConfiguration _configuration;

        public UserService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration
            )
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<ResponseObjVM<string>> Login(LoginVM loginVM)
        {
            var user = await _userManager.FindByEmailAsync(loginVM.Email);

            if (user == null)
                return "لا   يوجد    مستخدم  بهذا    البريد  اللإلكترونى.";

            var result = await _userManager.CheckPasswordAsync(user, loginVM.Password);

            if (!result)
                return "كلمة السر    غير صحيحة.";

            var claims = new[]{
                new Claim("Email",loginVM.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };
             var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(10),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new ResponseObjVM<string>(tokenString, "User has been Logged successfully.");
        }

        public ResponseObjVM<string> Register(RegisterVM registerVM)
        {
            if (registerVM == null)
            {
                throw new NotImplementedException("Register Model    is  Null");
            }

            if (registerVM.Password != registerVM.ConfirmPassword)
                return "Confirm   password    dosen't match   password";

            var applicationUser = new ApplicationUser()
            {
                EmailConfirmed = true,
                FirstName = "",
                LastName = "",
                UserName = "",
                Email = registerVM.Email,
                LockoutEnabled = false,
            };

            var Result=_userManager.CreateAsync(applicationUser, registerVM.Password);

            if (Result.Result.Succeeded)
            {
                return new ResponseObjVM<string>("Registered", "User has been created successfully.");
            }
            return Result.Result.Errors.Select(e => e.Description).FirstOrDefault();
        }

    }
}
