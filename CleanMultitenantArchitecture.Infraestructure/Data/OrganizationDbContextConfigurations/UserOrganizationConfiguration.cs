using CleanMultitenantArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanMultitenantArchitecture.Infraestructure.Data.OrganizationDbContextConfigurations
{
    public class UserOrganizationConfiguration : IEntityTypeConfiguration<UserOrganization>
    {
        public void Configure(EntityTypeBuilder<UserOrganization> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(p => p.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

            builder.Property(t => t.CreatedDate)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder.HasOne(t => t.Organization)
                .WithMany(r => r.UserOrganizations)
                .HasForeignKey(r => r.OrganizationFk)
                .IsRequired();

            builder.HasOne(t => t.User)
               .WithMany(r => r.UserOrganizations)
               .HasForeignKey(r => r.UserFk)
               .IsRequired();


        }
    }
}
