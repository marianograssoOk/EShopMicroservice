using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(or => or.Id);

        builder.Property(or => or.Id).HasConversion(
            orderItemId => orderItemId.Value,
            dbId => OrderItemId.Of(dbId));

        builder.HasOne<Product>().WithMany().HasForeignKey(or => or.ProductId);

        builder.Property(or => or.Price).IsRequired();

        builder.Property(or => or.Quantity).IsRequired();
    }
}