using MediatR;
using ReadingIsGood.Application.Responses;
using ReadingIsGood.Domain.Entities;

namespace ReadingIsGood.Application.Commands.OrderCreate;

public class OrderCreateCommand : IRequest<OrderResponse>
{
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();
}