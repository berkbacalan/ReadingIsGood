using AutoMapper;
using Moq;
using ReadingIsGood.Application.Commands.Book;
using ReadingIsGood.Application.Handlers;
using ReadingIsGood.Domain.Entities;
using ReadingIsGood.Domain.Repositories;
using ReadingIsGood.Application.Mapper;

namespace tests.ApplicationTests
{
    public class BookUpdateHandlerTests
    {
        private Mock<IBookRepository> _bookRepositoryMock;
        private IMapper _mapperMock;
        private BookUpdateHandler _handler;

        [SetUp]
        public void Setup()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<BookMappingProfile>());
            _mapperMock = config.CreateMapper();
            _handler = new BookUpdateHandler(_bookRepositoryMock.Object, _mapperMock);
        }

        [Test]
        public async Task Handle_WithValidBookUpdate_ShouldUpdateBookSuccessfully()
        {
            // Arrange
            var command = new BookUpdateCommand
            {
                BookId = 1,
                StockQuantity = 20
            };

            var book = new Book { Id = 1, StockQuantity = 10 };
            _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(book);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccessful);
            Assert.That(result.StockQuantity, Is.EqualTo(command.StockQuantity));
            Assert.Null(result.Error);
        }

        [Test]
        public async Task Handle_WithInvalidBookId_ShouldFailWithErrorMessage()
        {
            // Arrange
            var command = new BookUpdateCommand
            {
                BookId = 1,
                StockQuantity = 20
            };

            _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Book)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccessful);
            Assert.That(result.Error, Is.EqualTo($"Book could not found with given Id: {command.BookId}"));
        }

        [Test]
        public async Task Handle_WithException_ShouldFailWithErrorMessage()
        {
            // Arrange
            var command = new BookUpdateCommand
            {
                BookId = 1,
                StockQuantity = 20
            };

            var book = new Book { Id = 1, StockQuantity = 10 };
            _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(book);
            _bookRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Book>())).ThrowsAsync(new Exception("Test Exception"));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccessful);
            Assert.That(result.Error, Contains.Substring("Unknown error occured"));
        }
    }
}
