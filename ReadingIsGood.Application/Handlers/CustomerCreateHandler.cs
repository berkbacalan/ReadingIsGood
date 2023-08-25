using AutoMapper;
using FluentValidation;
using MediatR;
using ReadingIsGood.Application.Commands.CustomerCreate;
using ReadingIsGood.Application.Responses;
using ReadingIsGood.Domain.Entities;
using ReadingIsGood.Domain.Repositories;

namespace ReadingIsGood.Application.Handlers;

public class CustomerCreateHandler : IRequestHandler<CustomerCreateCommand, CustomerResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomerCreateHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<CustomerResponse> Handle(CustomerCreateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var customerEntity = _mapper.Map<Customer>(request);
            if (customerEntity == null)
            {
                return new CustomerResponse { IsSuccessful = false, Error = "Entity could not mapped." };
            }

            var customer = await _customerRepository.AddAsync(customerEntity);

            var customerResponse = _mapper.Map<CustomerResponse>(customer);
            return customerResponse;
        }
        catch (Exception e)
        {
            return new CustomerResponse { IsSuccessful = false, Error = $"Unknown error occured. Error: {e}" };
        }
    }
}