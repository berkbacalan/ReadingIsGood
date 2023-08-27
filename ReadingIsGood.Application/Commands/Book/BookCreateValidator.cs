using FluentValidation;
using ReadingIsGood.Domain.Entities;

namespace ReadingIsGood.Application.Commands.Book;

public class BookCreateValidator : AbstractValidator<BookCreateCommand>
{
    public BookCreateValidator()
    {
        RuleFor(v => v.Title).NotEmpty();
        RuleFor(v => v.BookPrice).GreaterThan(0)
            .WithMessage("Book Price must be greater than 0.");
        RuleFor(v => v.StockQuantity).GreaterThanOrEqualTo(0)
            .WithMessage("Book's stock quantity must be equal or greater than 0.");
    }
}