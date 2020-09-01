using AnamelTask.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


 namespace AnamelTask.Data.DtaContext
{
    class AppDbContextFactory : IDesignTimeDbContextFactory<TaskDbContext>
    {
        public TaskDbContext CreateDbContext(string[] args)
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("appsettings.json");
            var config = configBuilder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<TaskDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("TaskDB"));


            return new TaskDbContext(optionsBuilder.Options);
        }
    }
}
