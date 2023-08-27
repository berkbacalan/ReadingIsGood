using ReadingIsGood.Domain.Entities;
using ReadingIsGood.Domain.Entities.Base;

namespace tests.DomainTests;

public class OrderTests
{
    [Test]
    public void Order_ShouldHaveCustomerIdProperty()
    {
        // Arrange
        var order = new Order();

        // Act & Assert
        Assert.NotNull(order.CustomerId);
    }

    [Test]
    public void Order_ShouldHaveOrderItemsProperty()
    {
        // Arrange
        var order = new Order();

        // Act & Assert
        Assert.NotNull(order.OrderItems);
        Assert.IsAssignableFrom<List<OrderItem>>(order.OrderItems);
    }

    [Test]
    public void Order_ShouldHaveTotalAmountProperty()
    {
        // Arrange
        var order = new Order();

        // Act & Assert
        Assert.That(order.TotalAmount, Is.EqualTo(0));
    }
}