
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

        public BooksController(DataContext context){
            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Book>>> GetBooks(){

            var users = await _context.Books.ToListAsync();

            return users;
        }


    }
}