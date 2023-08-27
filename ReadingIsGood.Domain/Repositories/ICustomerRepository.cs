using ReadingIsGood.Domain.Entities;
using ReadingIsGood.Domain.Repositories.Base;

namespace ReadingIsGood.Domain.Repositories;

public interface ICustomerRepository : IRepository<Customer>
{
    public Task<Customer?> GetCustomerByEmail(string email);
}