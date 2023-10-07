using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using TodoList.Domain.Entities;

namespace TodoList.Infra.Data.EntityConfiguration
{
    public class EmailConfiguration : IEntityTypeConfiguration<Email>
    {
       
        public void Configure(EntityTypeBuilder<Email> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.EmailSend).HasMaxLength(100).IsRequired();
        
        }
    }
}
