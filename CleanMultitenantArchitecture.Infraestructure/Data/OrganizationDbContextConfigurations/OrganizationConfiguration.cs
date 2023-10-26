using CleanMultitenantArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CleanMultitenantArchitecture.Infraestructure.Data.OrganizationDbContextConfigurations
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(p => p.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

            builder.Property(t => t.CreatedDate)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder.Property(t => t.Name)
                .HasMaxLength(128).IsRequired();

            builder.Property(t => t.Server)
               .HasMaxLength(200)
              .IsRequired();


            builder.Property(t => t.User)
                .HasMaxLength(30)
              .IsRequired();


            builder.Property(t => t.Password)
              .HasMaxLength(50)
              .IsRequired();


            builder.HasMany(t => t.Products)
                .WithOne(p => p.Organization)
                .HasForeignKey(p => p.TenantId)
                .IsRequired();

            builder.HasMany(t => t.UserOrganizations)
             .WithOne(r => r.Organization)
             .HasForeignKey(r => r.OrganizationFk)
             .IsRequired();


        }
    }
}
