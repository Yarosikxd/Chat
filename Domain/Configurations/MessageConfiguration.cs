using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Text)
                .IsRequired();

            builder.Property(m => m.Timestamp)
                .IsRequired();

            builder.HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(m => m.Chat)
                .WithMany()
                .HasForeignKey(m => m.ChatId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
