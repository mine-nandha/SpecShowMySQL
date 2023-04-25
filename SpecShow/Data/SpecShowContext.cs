using Microsoft.EntityFrameworkCore;
using SpecShow.Models;

namespace SpecShow.Data
{
    public class SpecShowContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Mobile> Mobiles { get; set; }
        public DbSet<Favourite> Favourites { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(WebApplication.CreateBuilder().Configuration.GetConnectionString("ConnectionString"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User").HasIndex(u => u.UserName).IsUnique();
			modelBuilder.Entity<Mobile>().ToTable("Mobile");
            modelBuilder.Entity<Favourite>().ToTable("Favourite");
        }
    }
}
