using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;
using System.Reflection.Emit;

namespace TodoList.Infra.Data.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {


            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserName).HasMaxLength(80).IsRequired();
            builder.HasIndex(x => x.UserName).IsUnique();

            builder.Property(x => x.Email).HasMaxLength(150).IsRequired();
            builder.HasIndex(x => x.Email).IsUnique();


            builder.Property(x => x.Password).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Role).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            
        }
    }
}
