using MediatR;
using ReadingIsGood.Application.Responses;

namespace ReadingIsGood.Application.Commands.CustomerCreate;

public class CustomerCreateCommand : IRequest<CustomerResponse>
{
    public string Email { get; set; } = "";
}