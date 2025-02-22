using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Repositories.EFCore.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book() { Id = 1, Name = "Karagör ve Hacivat", Price = 100 },
                new Book() { Id = 2, Name = "küçük Prens", Price = 200 },
                new Book() { Id = 3, Name = "Mordor", Price = 300 }
            );
        }
    }
}
