
using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;

        public Type Type { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

    }
}