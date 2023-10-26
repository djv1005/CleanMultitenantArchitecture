using CleanMultitenantArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CleanMultitenantArchitecture.Infraestructure.Data.Configurations
{
    public class EntityBaseConfiguration<T> : IEntityTypeConfiguration<BaseEntity<T>> where T : struct
    {
        public void Configure(EntityTypeBuilder<BaseEntity<T>> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.CreatedDate).IsRequired();
        }
    }




}
