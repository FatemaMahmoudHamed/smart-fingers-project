using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnamelTask.UserData.DbModels;
namespace AnamelTask.UserData.DtaContext
{
    public class DataSeedingIntilization
    {
        private static UserDbContext _userDbContext;
        private static UserManager<ApplicationUser> _userManager;
        private static IServiceProvider _serviceProvider;

        public static void Seed(UserDbContext userDbContext, IServiceProvider serviceProvider)
        {
            _userDbContext = userDbContext;
            userDbContext.Database.EnsureCreated();
            _serviceProvider = serviceProvider;

            var serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            _userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

            SeedApplicationDefaultAdmin();

            _userDbContext.SaveChanges();
        }

        private static void SeedApplicationDefaultAdmin()
        {
            var superAdmin = _userManager.FindByNameAsync("info@smartfingers.com");
            if (superAdmin.Result == null)
            {
                var applicationUser = new ApplicationUser()
                {
                    EmailConfirmed = true,
                    FirstName = "Fatema",
                    LastName = "Hamed",
                    UserName = "info@smartfingers.com",
                    Email = "info@smartfingers.com",
                    LockoutEnabled = false,
                };

                _userManager.CreateAsync(applicationUser, "FAdmin@123");
            }
        }

    }
}
