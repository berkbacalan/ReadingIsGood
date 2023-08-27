using ReadingIsGood.Domain.Entities;

namespace tests.DomainTests;

public class BookTests
{
    [Test]
    public void Book_WithValidData_CreatedSuccessfully()
    {
        // Arrange & Act
        var book = new Book
        {
            Title = "Sample Book",
            StockQuantity = 10,
            BookPrice = new decimal(29.99)
        };

        // Assert
        Assert.That(book.Title, Is.EqualTo("Sample Book"));
        Assert.That(book.StockQuantity, Is.EqualTo(10));
        Assert.That(book.BookPrice, Is.EqualTo(29.99));
    }
}