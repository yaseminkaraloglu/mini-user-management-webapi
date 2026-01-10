using Microsoft.EntityFrameworkCore;
using MyWebApplication.Models;
using System.Collections.Generic;

namespace MyWebApplication.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}

