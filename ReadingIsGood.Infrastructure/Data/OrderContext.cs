using Microsoft.EntityFrameworkCore;
using ReadingIsGood.Domain.Entities;

namespace ReadingIsGood.Infrastructure.Data;

public class OrderContext : DbContext
{
    public OrderContext(DbContextOptions<OrderContext> options) : base(options)
    {
        
    }
    
    public DbSet<Order> Orders { get; set; }
}