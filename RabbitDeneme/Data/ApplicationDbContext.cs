using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RabbitDeneme.Models;

namespace RabbitDeneme.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
