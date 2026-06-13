using LinkVault.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkVault.Data.EntityConfigrations
{
    public class NoteConfigrations : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Content).IsRequired().HasMaxLength(1000);
            builder.Property(n => n.Title).IsRequired().HasMaxLength(100);  
                builder.Property(n => n.IsPinned).HasDefaultValue(false);


            builder.HasOne(n=> n.Category)
                .WithMany(c => c.Notes)
                .HasForeignKey(n => n.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}