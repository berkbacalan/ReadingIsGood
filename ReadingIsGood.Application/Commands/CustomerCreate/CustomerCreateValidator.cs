using FluentValidation;

namespace ReadingIsGood.Application.Commands.CustomerCreate;

public class CustomerCreateValidator : AbstractValidator<CustomerCreateCommand>
{
    public CustomerCreateValidator()
    {
        RuleFor(c => c.Email).EmailAddress();
    }
}