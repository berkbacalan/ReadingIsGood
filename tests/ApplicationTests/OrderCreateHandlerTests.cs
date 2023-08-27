using AutoMapper;
using Moq;
using ReadingIsGood.Application.Commands.OrderCreate;
using ReadingIsGood.Application.Handlers;
using ReadingIsGood.Application.Mapper;
using ReadingIsGood.Domain.Entities;
using ReadingIsGood.Domain.Repositories;

namespace tests.ApplicationTests;

public class OrderCreateHandlerTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly IMapper _mapperMock;
    private readonly OrderCreateHandler _handler;

    public OrderCreateHandlerTests()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<OrderMappingProfile>());
        _mapperMock = config.CreateMapper();

        _orderRepositoryMock = new Mock<IOrderRepository>();
        _bookRepositoryMock = new Mock<IBookRepository>();
        _customerRepositoryMock = new Mock<ICustomerRepository>();
        // _mapperMock = new Mock<IMapper>();
        _handler = new OrderCreateHandler(
            _orderRepositoryMock.Object, _bookRepositoryMock.Object,
            _customerRepositoryMock.Object, _mapperMock);
    }

    [Test]
    public async Task Handle_WithValidOrder_ShouldCreateOrderSuccessfully()
    {
        // Arrange
        var command = new OrderCreateCommand
        {
            CustomerId = 1,
            OrderItems = new List<OrderItem>
            {
                new OrderItem { BookId = 1, Quantity = 2 }
            }
        };
        var order = new Order
        {
            Id = 1,
            CustomerId = 1,
            OrderItems = new List<OrderItem>
            {
                new OrderItem { BookId = 1, Quantity = 2 }
            }
        };
        var customer = new Customer { Id = 1 };
        var book = new Book { Id = 1, StockQuantity = 10, BookPrice = new decimal(29.99) };

        _customerRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(customer);
        _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(book);
        _orderRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Order>())).ReturnsAsync(order);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccessful);
        Assert.That(result.CustomerId, Is.EqualTo(1));
        Assert.That(result.OrderItems.Count, Is.EqualTo(1));
        Assert.Null(result.Error);
    }

    [Test]
    public async Task Handle_WithInsufficientStock_ShouldFailWithErrorMessage()
    {
        // Arrange
        var command = new OrderCreateCommand
        {
            CustomerId = 1,
            OrderItems = new List<OrderItem>
            {
                new OrderItem { BookId = 1, Quantity = 10 }
            }
        };

        var customer = new Customer { Id = 1 };
        var book = new Book { Id = 1, StockQuantity = 5, BookPrice = new decimal(29.99) };

        _customerRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(customer);
        _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(book);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccessful);
        Assert.That(result.Error, Is.EqualTo($"Not enough stocks were found for book id: {book.Id}."));
    }

    [Test]
    public async Task Handle_WithInvalidCustomerId_ShouldFailWithErrorMessage()
    {
        // Arrange
        var command = new OrderCreateCommand
        {
            CustomerId = 1,
            OrderItems = new List<OrderItem>
            {
                new OrderItem { BookId = 1, Quantity = 1 }
            }
        };

        _customerRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Customer)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccessful);
        Assert.That(result.Error, Is.EqualTo("Customer could not found."));
    }

    [Test]
    public async Task Handle_WithInvalidBookId_ShouldFailWithErrorMessage()
    {
        // Arrange
        var command = new OrderCreateCommand
        {
            CustomerId = 1,
            OrderItems = new List<OrderItem>
            {
                new OrderItem { BookId = 1, Quantity = 1 }
            }
        };

        var customer = new Customer { Id = 1 };
        _customerRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(customer);
        _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Book)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccessful);
        Assert.That(result.Error, Is.EqualTo("Could not find book with id: 1."));
    }

    [Test]
    public async Task Handle_WithInvalidOrderItemQuantity_ShouldFailWithErrorMessage()
    {
        // Arrange
        var command = new OrderCreateCommand
        {
            CustomerId = 1,
            OrderItems = new List<OrderItem>
            {
                new OrderItem { BookId = 1, Quantity = 0 }
            }
        };

        var customer = new Customer { Id = 1 };
        var book = new Book { Id = 1, StockQuantity = 10, BookPrice = new decimal(29.99) };

        _customerRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(customer);
        _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(book);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccessful);
        Assert.That(result.Error, Is.EqualTo("Invalid operation. Quantity should be greater than 0 for all items."));
    }

    [Test]
    public async Task Handle_WithZeroTotalAmount_ShouldFailWithErrorMessage()
    {
        // Arrange
        var command = new OrderCreateCommand
        {
            CustomerId = 1,
            OrderItems = new List<OrderItem>
            {
                new OrderItem { BookId = 1, Quantity = 1 }
            }
        };

        var customer = new Customer { Id = 1 };
        var book = new Book { Id = 1, StockQuantity = 10, BookPrice = 0 };

        _customerRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(customer);
        _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(book);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccessful);
        Assert.That(result.Error, Is.EqualTo("Total Order amount could not be equal or less than 0."));
    }
}