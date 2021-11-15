using DemoService.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoService.Data
{
    public class MyServiceContext : DbContext
    {
        public MyServiceContext(DbContextOptions<MyServiceContext> opt) : base(opt)
        {
            
        }

        public DbSet<MyService> MyServices { get; set; }

    }
}