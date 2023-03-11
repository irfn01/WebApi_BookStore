using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public GetBooksQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<BookViewModel> Handle()
        {
            var bookList = _dbContext.Book.OrderBy(x => x.Id).ToList<Book>();
            List<BookViewModel> vm = new List<BookViewModel>();
            foreach (var book in bookList)
            {
                vm.Add(new BookViewModel()
                {                
                Title = book.Title,
                Genre = ((GenreEnum)book.GenreId).ToString(),
                Writer = book.Writer,
                PageCount = book.PageCount,
                PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy")
                });
            }    
            return vm;
        }

public class BookViewModel
{
    public string Title { get; set; }
    public string Writer { get; set; }
    public string Genre { get; set; }
    public int PageCount { get; set; }
    public string PublishDate { get; set; }
}
}

}
