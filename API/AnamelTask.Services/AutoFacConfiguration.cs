using AnamelTask.Repositories.UserDataRepositories.UOW;
using Autofac;
using AnamelTask.Repositories.DataRepositories.Customer;
using AnamelTask.Services.UserServices.General;
using AnamelTask.Services.DataServices.Customers;
using AnamelTask.Services.UserServices.User;
using AnamelTask.Services.UserServices.User;

namespace AnamelTask.Services
{
    public class AutoFacConfiguration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register Unit of work service
            builder.RegisterGeneric(typeof(UnitOfWork<>)).As(typeof(IUnitOfWork<>));
            // Global
            builder.RegisterType<GeneralService>().As<IGeneralService>();

            #region User
            builder.RegisterType<UserService>().As<IUserService>();
            #endregion

            #region Customer
            builder.RegisterType<CustomerServices>().As<ICustomerServices>();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
            #endregion

        }
    }
}