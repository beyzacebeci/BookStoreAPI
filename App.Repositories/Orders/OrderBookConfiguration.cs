using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Repositories.Orders
{
    public class OrderBookConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Order-Book many-to-many ilişkisi için seed data
            builder.HasMany(o => o.Books)
                .WithMany(b => b.Orders)
                .UsingEntity(j => j.HasData(
                    new { OrdersId = 1, BooksId = 1 }, // Suç ve Ceza siparişi
                    new { OrdersId = 1, BooksId = 1 }, // İkinci kopya
                    new { OrdersId = 2, BooksId = 2 }, // Dune siparişi
                    new { OrdersId = 3, BooksId = 3 }, // Nutuk siparişi
                    new { OrdersId = 3, BooksId = 3 }, // İkinci kopya
                    new { OrdersId = 3, BooksId = 3 }  // Üçüncü kopya
                ));
        }
    }
}