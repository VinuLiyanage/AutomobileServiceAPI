using GarageAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GarageAPI.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) :base(options) { }

        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
    }
}
