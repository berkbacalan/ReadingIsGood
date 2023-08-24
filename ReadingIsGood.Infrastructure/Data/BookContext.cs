using Microsoft.EntityFrameworkCore;
using ReadingIsGood.Domain.Entities;

namespace ReadingIsGood.Infrastructure.Data;

public class BookContext : DbContext
{
    public BookContext(DbContextOptions<OrderContext> options) : base(options)
    {
        
    }
    
    public DbSet<Book> Books { get; set; }
}