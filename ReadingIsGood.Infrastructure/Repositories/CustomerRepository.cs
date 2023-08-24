using ReadingIsGood.Domain.Entities;
using ReadingIsGood.Domain.Repositories;
using ReadingIsGood.Domain.Repositories.Base;
using ReadingIsGood.Infrastructure.Data;
using ReadingIsGood.Infrastructure.Repositories.Base;

namespace ReadingIsGood.Infrastructure.Repositories;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(AppDbContext context) : base(context)
    {
    }
}