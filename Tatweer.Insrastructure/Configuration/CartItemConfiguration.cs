using Tatweer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tatweer.Insrastructure.Configuration
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.Property(c => c.ProductId).IsRequired();
            builder.Property(c => c.Qty).HasDefaultValue(0); 
            builder.ToTable("CartItems");
        }
    }
}
