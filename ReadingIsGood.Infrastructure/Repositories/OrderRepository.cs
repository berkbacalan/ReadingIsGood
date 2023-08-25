using Microsoft.EntityFrameworkCore;
using ReadingIsGood.Domain.Entities;
using ReadingIsGood.Domain.Repositories;
using ReadingIsGood.Infrastructure.Data;
using ReadingIsGood.Infrastructure.Repositories.Base;

namespace ReadingIsGood.Infrastructure.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
        
    }

    public async Task<IEnumerable<Order>> GetOrdersByCustomerId(int customerId)
    {
        var orderList = await _dbContext.Orders.Where(o => o.CustomerId == customerId)
            .ToListAsync();
        return orderList;
    }
}