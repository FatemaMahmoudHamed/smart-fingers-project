using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnamelTask.Data.DataContext;
using AnamelTask.Services;
using AnamelTask.Services.UserServices.User;
using AnamelTask.UserData.DbModels;
using AnamelTask.UserData.DtaContext;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Lease.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Anamel.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("TaskUserDB")));

            services.AddDbContext<TaskDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("TaskDB")));

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["AuthSettings:Audience"],
                    ValidIssuer = Configuration["AuthSettings:Issuer"],
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthSettings:Key"])),
                    ValidateIssuerSigningKey = true,
                };
            });

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<UserDbContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUserService, UserService>();

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            var builder = new ContainerBuilder();

            builder.RegisterModule(new AutoFacConfiguration());

            builder.Populate(services);

            var container = builder.Build();

            return new AutofacServiceProvider(container);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, UserDbContext userDbContext, TaskDbContext taskDbContext)
        {

            app.UseRequestLocalization();
            var requestOpt = new RequestLocalizationOptions();
            requestOpt.SupportedCultures = new List<CultureInfo> { new CultureInfo("en-US") };
            requestOpt.SupportedUICultures = new List<CultureInfo> { new CultureInfo("en-US") };
            requestOpt.RequestCultureProviders.Clear();
            requestOpt.RequestCultureProviders.Add(new SingleCultureProvider());
            app.UseRequestLocalization(requestOpt);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();

            app.UseCors(option =>
            {
                option.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            DataSeedingIntilization.Seed(userDbContext, serviceProvider);
            TaskDataSeedingIntilization.Seed(taskDbContext);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }

    public class SingleCultureProvider : IRequestCultureProvider
    {
        public Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            return System.Threading.Tasks.Task.Run(() => new ProviderCultureResult("ar-EG", "ar-EG"));
        }
    }
}
