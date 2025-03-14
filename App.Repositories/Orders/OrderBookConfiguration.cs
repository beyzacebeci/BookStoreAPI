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
                    // Order 1: 2 farklı kitap
                    new { OrdersId = 1, BooksId = 1 },  // Suç ve Ceza
                    new { OrdersId = 1, BooksId = 2 },  // Dune

                    // Order 2: 1 kitap
                    new { OrdersId = 2, BooksId = 2 },  // Dune

                    // Order 3: 2 farklı kitap
                    new { OrdersId = 3, BooksId = 1 },  // Suç ve Ceza
                    new { OrdersId = 3, BooksId = 3 }   // Nutuk
                ));
        }
    }
}