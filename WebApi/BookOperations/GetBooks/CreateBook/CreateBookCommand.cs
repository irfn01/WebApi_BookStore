using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public CreateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var book = _dbContext.Book.SingleOrDefault(x => x.Title == Model.Title);
            if (book is not null) throw new InvalidOperationException("Kitap Zaten Mevcut.");
            book=new Book();
            book.Title=Model.Title;
            book.Writer=Model.Writer;
            book.GenreId=Model.GenreId;
            book.PageCount=Model.PageCount;
            book.PublishDate=Model.PublishDate;
            _dbContext.Book.Add(book);
            _dbContext.SaveChanges();
        }
        public class CreateBookModel
        {
            public string Title { get; set; }
            public string Writer { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }

}