using FluentValidation;
using ReadingIsGood.Domain.Entities;

namespace ReadingIsGood.Application.Commands.Book;

public class BookUpdateValidator : AbstractValidator<BookUpdateCommand>
{
    public BookUpdateValidator()
    {

        RuleFor(v => v.BookId).GreaterThan(0)
            .WithMessage("BookId should be greater than 0.");
        RuleFor(v => v.StockQuantity).GreaterThanOrEqualTo(0)
            .WithMessage("Book's stock quantity must be equal or greater tan 0.");
    }
}