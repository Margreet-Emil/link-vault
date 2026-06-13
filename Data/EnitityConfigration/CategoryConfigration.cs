using LinkVault.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkVault.Data.EntityConfigrations
{
    public class CategoryConfigrations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Description).HasMaxLength(500);
            builder.Property(c => c.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

          
        }
    }
}