using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using TodoList.Domain.Entities;

namespace TodoList.Infra.Data.EntityConfiguration
{
    public class EmailConfiguration : IEntityTypeConfiguration<EmailUser>
    {
       
        public void Configure(EntityTypeBuilder<EmailUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
        
        }
    }
}
