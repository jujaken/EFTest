
using EFTest.Models;
using Microsoft.EntityFrameworkCore;

namespace EFTest.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Vector> Vectors { get; set; }

        public AppDbContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=eftest.db");
        }
    }
}
