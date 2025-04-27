using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace App.Repositories.Books;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(150);
        builder.Property(x => x.ISBN).IsRequired();
        builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");

        builder.Property(x => x.StockQuantity).HasDefaultValue(0);
        builder.Property(x => x.PublicationYear).IsRequired();


        builder.HasOne(b => b.Author)   // Book'un bir Author'u vardır
             .WithMany()              // Author'un birden çok Book'u olabilir (isteğe göre)
            .HasForeignKey(b => b.AuthorId);

        builder.HasOne(x => x.Category)
            .WithMany(c => c.Books)
            .HasForeignKey(x => x.CategoryId);

        builder.HasData(
            new Book
            {
                Id = 1,
                CategoryId = 1, // Roman
                AuthorId = 1,
                Title = "Suç ve Ceza",
                ISBN = "9789750719387",
                Price = 49.99m,
                StockQuantity = 50,
                PublicationYear = 1866
            },
            new Book
            {
                Id = 2,
                CategoryId = 2, // Bilim Kurgu
                AuthorId = 2,
                Title = "Dune",
                ISBN = "9789753421851",
                Price = 59.99m,
                StockQuantity = 30,
                PublicationYear = 1965
            },
            new Book
            {
                Id = 3,
                CategoryId = 3,
                AuthorId = 3,
                Title = "Nutuk",
                ISBN = "9789944885348",
                Price = 69.99m,
                StockQuantity = 40,
                PublicationYear = 1927
            }
        );
    }
}

