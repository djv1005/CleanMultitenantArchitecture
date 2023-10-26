using CleanMultitenantArchitecture.Domain.Entities;
using CleanMultitenantArchitecture.Infraestructure.Services;
using Microsoft.EntityFrameworkCore;


namespace CleanMultitenantArchitecture.Infraestructure.Data
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        private readonly ITenantService _tenantService;
        private string tenantConnection { get; set; }
        public ProductDbContext(DbContextOptions options, ITenantService tenantService)
        {
            _tenantService = tenantService;
            tenantConnection = _tenantService.TenantConnectionString;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(tenantConnection);
        }

        /*protected override int SaveChanges()
        {
            int result;
            foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>().ToList())
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entry.Entity.TenantId = tenantId;
                }
             
            }
            result = base.SaveChanges();
            return result;
        }*/
    }
}
