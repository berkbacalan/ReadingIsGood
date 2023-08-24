using ReadingIsGood.Domain.Entities.Base;

namespace ReadingIsGood.Domain.Entities;

public class Customer : Entity
{
    public string Email { get; set; } = "";
}