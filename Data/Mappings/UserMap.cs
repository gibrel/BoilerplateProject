using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Boilerplate.Domain.Entities;

namespace Boilerplate.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username)
                .HasConversion(u => u.ToString(), u => u)
                .IsRequired()
                .HasColumnName("Username")
                .HasColumnType("varchar(100)");

            builder.Property(u => u.PasswordHash)
                .HasConversion(u => u.ToString(), u => u)
                .IsRequired()
                .HasColumnName("PasswordHash")
                .HasColumnType("char(64)");

            builder.Property(u => u.Salt)
                .HasConversion(u => u.ToString(), u => u)
                .IsRequired()
                .HasColumnName("Salt")
                .HasColumnType("char(64)");
        }
    }
}
