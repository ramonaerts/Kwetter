using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Entities;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace AuthenticationService.DAL
{
    public class AuthenticationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AuthenticationContext(DbContextOptions options) : base(options)
        {
            
        }

        public AuthenticationContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=authenticationdatabase;Port=3306;Database=kwetter;Uid=root;password=example;");
        }
    }
}
