using Moq;
using ReadingIsGood.Application.Commands.Book;
using ReadingIsGood.Application.Handlers;
using ReadingIsGood.Domain.Entities;
using ReadingIsGood.Domain.Repositories;
using AutoMapper;
using ReadingIsGood.Application.Mapper;

namespace tests.ApplicationTests
{
    public class BookCreateHandlerTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly IMapper _mapperMock;
        private readonly BookCreateHandler _handler;

        public BookCreateHandlerTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<BookMappingProfile>());
            _mapperMock = config.CreateMapper();
            _bookRepositoryMock = new Mock<IBookRepository>();
            _handler = new BookCreateHandler(_bookRepositoryMock.Object, _mapperMock);
        }

        [Test]
        public async Task Handle_WithValidBook_ShouldCreateBookSuccessfully()
        {
            // Arrange
            var command = new BookCreateCommand
            {
                Title = "Sample Book",
                StockQuantity = 10,
                BookPrice = new decimal(29.99)
            };

            _bookRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Book>())).ReturnsAsync(new Book { Id = 1 });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccessful);
            Assert.Null(result.Error);
        }

        [Test]
        public async Task Handle_WithException_ShouldFailWithErrorMessage()
        {
            // Arrange
            var command = new BookCreateCommand
            {
                Title = "Sample Book",
                StockQuantity = 10,
                BookPrice = new decimal(29.99)
            };

            _bookRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Book>())).ThrowsAsync(new Exception("Some error"));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccessful);
            Assert.That(result.Error, Is.EqualTo("Unknown error occured."));
        }
    }
}