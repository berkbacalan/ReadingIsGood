using System.Collections;
using ReadingIsGood.Domain.Entities;
using ReadingIsGood.Domain.Repositories.Base;

namespace ReadingIsGood.Domain.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    public Task<IEnumerable<Order>> GetOrdersByCustomerId(int customerId);

    public Task<IEnumerable<Order>> GetOrdersByDate(DateTime? startDate, DateTime? endDate);
}