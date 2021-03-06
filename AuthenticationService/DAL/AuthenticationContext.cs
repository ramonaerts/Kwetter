using AuthenticationService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.DAL
{
    public class AuthenticationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AuthenticationContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => { entity.HasKey(user => user.Id); });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=authenticationdatabase;Port=3306;Database=kwetter;Uid=root;password=example;");
        }
    }
}
