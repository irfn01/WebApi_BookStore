using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context=new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
        {
            if(context.Book.Any()) return;

            context.Book.AddRange(
            new Book
                {
                // Id=1,
                Title="Kürk Mantolu Madonna",
                Writer="Sabahattin Ali",
                GenreId=1,
                PageCount=160,
                PublishDate=new DateTime(1998,05,12)
                },
            new Book
                {
                // Id=2,
                Title="Küçük Prens",
                Writer="Antoine de Saint-Exupery",
                GenreId=2,
                PageCount=96,
                PublishDate=new DateTime(2015,05,12)
                },
            new Book
                {
                // Id=3,
                Title="Sefiller",
                Writer="Victor Hugo",
                GenreId=2,
                PageCount=111,
                PublishDate=new DateTime(2003,05,12)
                });
                context.SaveChanges();            
        }
    }
}