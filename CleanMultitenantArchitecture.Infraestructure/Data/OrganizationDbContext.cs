using CleanMultitenantArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace CleanMultitenantArchitecture.Infraestructure.Data
{
    public class OrganizationDbContext:DbContext
    {
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOrganization> UserOrganizations { get; set; }

        public OrganizationDbContext(DbContextOptions<OrganizationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrganizationDbContext).Assembly);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
