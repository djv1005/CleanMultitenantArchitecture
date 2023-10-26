using CleanMultitenantArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CleanMultitenantArchitecture.Infraestructure.Data.ProductDbContextConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(p => p.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

            builder.Property(t => t.CreatedDate)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder.Property(t => t.Name)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(t => t.Description)
               .HasMaxLength(500)
               .IsRequired(false);

            builder.Property(t => t.Price)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

            builder.HasOne(t => t.Organization)
                .WithMany(p => p.Products)
                .HasForeignKey(t => t.TenantId)
                .IsRequired();

        }

     
    }
}
