using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnamelTask.Data.DataContext;

namespace AnamelTask.UserData.DtaContext
{
    public class TaskDataSeedingIntilization
    {
        private static TaskDbContext _taskDbContext;

        public static void Seed(TaskDbContext taskDbContext)
        {
            _taskDbContext = taskDbContext;
            taskDbContext.Database.EnsureCreated();
            _taskDbContext.SaveChanges();
        }
    }
}
