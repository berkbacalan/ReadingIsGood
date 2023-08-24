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
}