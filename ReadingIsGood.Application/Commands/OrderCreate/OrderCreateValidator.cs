using FluentValidation;
using ReadingIsGood.Domain.Entities;

namespace ReadingIsGood.Application.Commands.OrderCreate;

public class OrderCreateValidator : AbstractValidator<OrderCreateCommand>
{
    public OrderCreateValidator()
    {
        RuleFor(v => v.CustomerId).NotEmpty();
        RuleFor(v => v.OrderItems.Count).GreaterThanOrEqualTo(0);
        RuleForEach(v => v.OrderItems)
            .Must(item => item.Quantity >= 1)
            .WithMessage("Quantity must be greater than or equal to 1 for each OrderItem.");
        RuleForEach(v => v.OrderItems)
            .Must(item => item.BookId >= 1)
            .WithMessage("BookId must be greater than or equal to 1 for each OrderItem.");
    }
}