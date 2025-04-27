using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace App.Repositories.Authors;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(150);


        builder.HasData(
            new Author
            {
                Id = 1,
                Name ="Dostoyevski" , // Roman
            },
            new Author
            {
                Id = 2,
                Name = "Frank Herbert"
            },
            new Author
            {
                Id = 3,
                Name = "Atatürk"
            }
        );
    }
}

