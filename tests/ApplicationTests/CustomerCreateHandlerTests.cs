using Moq;
using ReadingIsGood.Application.Commands.CustomerCreate;
using ReadingIsGood.Application.Handlers;
using ReadingIsGood.Domain.Entities;
using ReadingIsGood.Domain.Repositories;
using AutoMapper;
using ReadingIsGood.Application.Mapper;

namespace tests.ApplicationTests
{
    public class CustomerCreateHandlerTests
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly IMapper _mapperMock;
        private readonly CustomerCreateHandler _handler;

        public CustomerCreateHandlerTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<CustomerMappingProfile>());
            _mapperMock = config.CreateMapper();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _handler = new CustomerCreateHandler(_customerRepositoryMock.Object, _mapperMock);
        }

        [Test]
        public async Task Handle_WithValidCustomer_ShouldCreateCustomerSuccessfully()
        {
            // Arrange
            var command = new CustomerCreateCommand
            {
                Email = "test@example.com"
            };

            Customer customerExisting = null;
            _customerRepositoryMock.Setup(repo => repo.GetCustomerByEmail(It.IsAny<string>())).ReturnsAsync(customerExisting);
            _customerRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Customer>())).ReturnsAsync(new Customer { Id = 1 });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccessful);
            Assert.Null(result.Error);
        }

        [Test]
        public async Task Handle_WithExistingCustomer_ShouldFailWithErrorMessage()
        {
            // Arrange
            var command = new CustomerCreateCommand
            {
                Email = "test@example.com"
            };

            var existingCustomer = new Customer { Id = 1, Email = "test@example.com" };
            _customerRepositoryMock.Setup(repo => repo.GetCustomerByEmail(It.IsAny<string>())).ReturnsAsync(existingCustomer);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccessful);
            Assert.That(result.Error, Is.EqualTo($"Customer already exists with email: {existingCustomer.Email}"));
        }

        [Test]
        public async Task Handle_WithException_ShouldFailWithErrorMessage()
        {
            // Arrange
            var command = new CustomerCreateCommand
            {
                Email = "test@example.com"
            };

            _customerRepositoryMock.Setup(repo => repo.GetCustomerByEmail(It.IsAny<string>())).ReturnsAsync((Customer)null);
            _customerRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Customer>())).ThrowsAsync(new Exception("Some error"));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccessful);
            Assert.That(result.Error, Is.EqualTo("Unknown error occured."));
        }
    }
}
