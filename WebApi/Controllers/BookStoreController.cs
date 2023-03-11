using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BookStoreController : ControllerBase
{
    private readonly BookStoreDbContext _context;

    public BookStoreController(BookStoreDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
        GetBooksQuery query = new GetBooksQuery(_context);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public Book GetById(int id)
    {
        var book = _context.Book.Where(x => x.Id == id).SingleOrDefault();
        return book;
    }
    /*
            [HttpGet]   
             public Book? Get([FromQuery] string id)
            {
                var book= BookList.Where(x=>x.Id==Convert.ToInt32(id)).SingleOrDefault();
                return book;
            }
    */
    //Post
    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookModel newBook)
    {
        CreateBookCommand command = new CreateBookCommand(_context);
        try
        {
            command.Model = newBook;
            command.Handle();
            
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok();
    }
    //Put
    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
    {
        var book = _context.Book.SingleOrDefault(x => x.Id == id);
        if (book is null) return BadRequest();

        book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
        book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
        book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
        book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
        book.Writer = updatedBook.Writer != default ? updatedBook.Writer : book.Writer;
        _context.SaveChanges();
        return Ok();
    }

    //Delete

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        var book = _context.Book.SingleOrDefault(x => x.Id == id);
        if (book is null) return BadRequest();

        _context.Book.Remove(book);
        _context.SaveChanges();
        return Ok();
    }

}
