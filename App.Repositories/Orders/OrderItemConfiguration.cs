using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Repositories.Orders;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        // Primary Key
        builder.HasKey(oi => oi.Id);

        // Properties
        builder.Property(oi => oi.Quantity)
            .IsRequired();

        builder.Property(oi => oi.UnitPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        // Relationships
        builder.HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oi => oi.Book)
            .WithMany()
            .HasForeignKey(oi => oi.BookId)
            .OnDelete(DeleteBehavior.Restrict);

        // Index
        builder.HasIndex(oi => new { oi.OrderId, oi.BookId });

        // Örnek Seed Data
        builder.HasData(
            new OrderItem
            {
                Id = 1,
                OrderId = 1,
                BookId = 1,
                Quantity = 2,
                UnitPrice = 49.99m
            },
            new OrderItem
            {
                Id = 2,
                OrderId = 2,
                BookId = 2,
                Quantity = 1,
                UnitPrice = 59.99m
            },
            new OrderItem
            {
                Id = 3,
                OrderId = 3,
                BookId = 3,
                Quantity = 3,
                UnitPrice = 69.99m
            }
        );
    }
}