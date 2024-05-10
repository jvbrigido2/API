
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly DataContext _context;

        public BooksController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("list-books")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        public async Task<ActionResult<IEnumerable<Book>>> ListBooks()
        {

            var books = await _context.Books.ToListAsync();

            return books;
        }

        [HttpGet("get-book/{id}")]
        [ProducesResponseType(200, Type = typeof(Book))]
        [ProducesResponseType(404, Type = typeof(string))] 
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return BadRequest("Book not found");
            }
            return book;
        }


        [HttpPost("create-book")]
        [ProducesResponseType(201, Type = typeof(Book))]

        public async Task<ActionResult<Book>> CreateBook(Book book)
        {

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return Created();
        }

        [HttpPut("edit/{id}")]
        [ProducesResponseType(204)] 
        public async Task<ActionResult<Book>> EditBook(Book book, int id)
        {

            if (id != book.Id)
            {
                return BadRequest("ID do recurso nao corresponde ao ID na url");
            }

            var existingBook = await _context.Books.FindAsync(id);

            if (existingBook == null)
            {
                return NotFound("Recurso nao encontrado");
            }

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Genre = book.Genre;
            existingBook.Price = book.Price;
            existingBook.Quantity = book.Quantity;

            _context.Entry(existingBook).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(204)] 
        [ProducesResponseType(404, Type = typeof(string))] 
        public async Task<ActionResult<Book>> DeleteBook(int id){

            var book = await _context.Books.FindAsync(id);

            if(book == null){
                return NotFound("Nao existe esse livro");
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}