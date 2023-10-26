using CleanMultitenantArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CleanMultitenantArchitecture.Infraestructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(p => p.Id)
             .ValueGeneratedOnAdd()
             .UseIdentityColumn();

            builder.Property(t => t.CreatedDate)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder.Property(t => t.Email)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(t => t.Password)
                .HasMaxLength(128)
                .IsRequired();

            builder.HasMany(t => t.UserOrganizations)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserFk)
                .IsRequired();



        }
    }
}
