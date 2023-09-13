using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace Warehouse.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Department> Departments { get; set; } = null!;

        public DbSet<Worker> Workers { get; set; } = null!;

        public DbSet<Product> Products { get; set; } = null!;

        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }
    }
}
