using FluentValidation;
using ReadingIsGood.Domain.Entities;

namespace ReadingIsGood.Application.Commands.OrderCreate;

public class OrderCreateValidator : AbstractValidator<OrderCreateCommand>
{
    public OrderCreateValidator()
    {
        RuleFor(v => v.OrderDate).NotEmpty();
        RuleFor(v => v.CustomerId).NotEmpty();
        RuleFor(v => v.OrderDate).NotEmpty();
        RuleFor(v => v.OrderDate > DateTime.UtcNow);
        RuleFor(v => v.OrderItems.Count).GreaterThanOrEqualTo(0);
    }
}