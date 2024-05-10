
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
              .Property(b => b.Type)
              .HasConversion(
                  convertToProviderExpression: t => t.ToString(),
                  convertFromProviderExpression: s => (Type)Enum.Parse(typeof(Type), s)
              );
            modelBuilder.Entity<Book>()
              .Property(b => b.Quantity)
              .ValueGeneratedOnAdd();
        }
    }

}