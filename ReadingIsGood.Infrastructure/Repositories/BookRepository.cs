using Microsoft.EntityFrameworkCore;
using ReadingIsGood.Domain.Entities;
using ReadingIsGood.Domain.Repositories;
using ReadingIsGood.Infrastructure.Data;
using ReadingIsGood.Infrastructure.Repositories.Base;

namespace ReadingIsGood.Infrastructure.Repositories;

public class BookRepository : Repository<Book>, IBookRepository
{
    public BookRepository(AppDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Book>> GetStockByBookId(int bookId)
    {
        var bookList = await _dbContext.Books.Where(o => o.Id == bookId)
            .ToListAsync();
        return bookList;
    }
}