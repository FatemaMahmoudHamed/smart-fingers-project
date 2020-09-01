using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnamelTask.Data.DbModels;

namespace AnamelTask.Data.DataContext
{
    public class TaskDbContext:DbContext
    {

        public TaskDbContext(DbContextOptions<TaskDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
