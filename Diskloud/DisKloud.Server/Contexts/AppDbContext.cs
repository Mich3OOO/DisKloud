using DisKloud.Server.Model;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;
using System;
namespace DisKloud.Server.Contexts 
{
    public class AppDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<FileData> FileData { get; set; }
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            
            Configuration = configuration;
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(Configuration.GetConnectionString("DisKloud"));
        }
    }
}
