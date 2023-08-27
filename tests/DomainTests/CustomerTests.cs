using ReadingIsGood.Domain.Entities;

namespace tests.DomainTests;

public class CustomerTests
{
    [Test]
    public void Customer_Email_DefaultValue_ShouldBeEmptyString()
    {
        // Arrange
        var customer = new Customer();

        // Assert
        Assert.That(customer.Email, Is.EqualTo(""));
    }

    [Test]
    public void Customer_Id_ShouldBeGenerated()
    {
        // Arrange
        var customer = new Customer();

        // Assert
        Assert.That(customer.Id, Is.EqualTo(0));
        Assert.That(customer.Id, Is.GreaterThanOrEqualTo(0));
    }

    [Test]
    public void Customer_CreatedOn_ShouldBeUtcNow()
    {
        // Arrange
        var customer = new Customer();

        // Assert
        Assert.That(customer.CreatedOn.Kind, Is.EqualTo(DateTimeKind.Utc));
    }

    [Test]
    public void Customer_UpdatedOn_DefaultValue_ShouldBeUtcNow()
    {
        // Arrange
        var customer = new Customer();

        // Assert
        Assert.That(customer.UpdatedOn.Kind, Is.EqualTo(DateTimeKind.Utc));
    }
}