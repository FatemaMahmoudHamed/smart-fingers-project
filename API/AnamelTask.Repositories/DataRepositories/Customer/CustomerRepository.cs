
using AnamelTask.Repositories.DataRepositories.Generic;
using AnamelTask.Data.DataContext;

namespace AnamelTask.Repositories.DataRepositories.Customer
{
    public class CustomerRepository : DataGenericRepository<AnamelTask.Data.DbModels.Customer>, ICustomerRepository
    {
        private readonly TaskDbContext _appDbContext;
        public CustomerRepository(TaskDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
