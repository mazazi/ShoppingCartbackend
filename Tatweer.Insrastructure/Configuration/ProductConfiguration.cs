using Tatweer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tatweer.Insrastructure.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Qty).HasDefaultValue(0);
            builder.Property(c => c.IsVisible).HasDefaultValue(true);
            builder.Property(c => c.Price).IsRequired().HasDefaultValue(0);
            builder.ToTable("Products");
        }
    }
}
