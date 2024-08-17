using Microsoft.EntityFrameworkCore;
using TestRestAPI.Models.Entities;

namespace TestRestAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Category> categories { get; set; }
        public DbSet<Item> items { get; set; }


    }

}
