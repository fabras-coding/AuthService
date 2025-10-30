using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AuthService.Infra.Data.EntitiesConfiguration
{
    public class JWTUserConfiguration : IEntityTypeConfiguration<JWTUser>
    {

        public void Configure(EntityTypeBuilder<JWTUser> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Username).IsRequired().HasMaxLength(36);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(36);

            var converter = new ValueConverter<List<string>, string>(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

            builder.Property( p=> p.Roles).HasConversion(converter);

        }

    }
}