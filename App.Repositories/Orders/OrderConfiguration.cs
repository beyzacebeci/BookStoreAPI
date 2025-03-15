using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Repositories.Orders;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.TotalPrice).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(o => o.OrderDate).IsRequired();
        builder.Property(o => o.Status).IsRequired().HasConversion<string>();

        // Books ile many-to-many ilişkisini kaldırın
        // builder.HasMany(o => o.Books).WithMany(b => b.Orders);

        // OrderItems ile one-to-many ilişkisi
        builder.HasMany(o => o.OrderItems)
               .WithOne(oi => oi.Order)
               .HasForeignKey(oi => oi.OrderId);

        // Seed Data
        builder.HasData(
            new Order
            {
                Id = 1,
                TotalPrice = 99.98m, // 2 adet Suç ve Ceza
                OrderDate = new DateTime(2024, 3, 15, 10, 30, 0, DateTimeKind.Utc),
                Status = Order.OrderStatus.COMPLETED
            },
            new Order
            {
                Id = 2,
                TotalPrice = 59.99m, // 1 adet Dune
                OrderDate = new DateTime(2024, 3, 16, 14, 45, 0, DateTimeKind.Utc),
                Status = Order.OrderStatus.PENDING
            },
            new Order
            {
                Id = 3,
                TotalPrice = 209.97m, // 3 adet Nutuk
                OrderDate = new DateTime(2024, 3, 17, 9, 15, 0, DateTimeKind.Utc),
                Status = Order.OrderStatus.COMPLETED
            }
        );
    }
}

