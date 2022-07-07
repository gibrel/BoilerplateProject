using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Boilerplate.Data.Configurations;
using Boilerplate.Data.Mapping;
using Boilerplate.Domain.Entities;

namespace Boilerplate.Data.Context
{
    public class DatabaseContext : DbContext
    {
        private readonly DatabaseConfiguration _configuration;

        public DatabaseContext(
            IOptions<DatabaseConfiguration> configuration,
            DbContextOptions<DatabaseContext> options) : base(options)
        {
            _configuration = configuration.Value;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.WebApiDatabase ?? "TestDatabase";

            optionsBuilder.UseInMemoryDatabase(connectionString, opt =>
            {
                opt.EnableNullChecks();
                //opt.CommandTimeout(120);
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(new UserMap().Configure);
        }
    }
}
