using Microsoft.EntityFrameworkCore;
using ReadingIsGood.Domain.Entities;

namespace ReadingIsGood.Infrastructure.Data;

public class CustomerContext : DbContext
{
    public CustomerContext(DbContextOptions<OrderContext> options) : base(options)
    {
        
    }
    
    public DbSet<Customer> Customers { get; set; }
}