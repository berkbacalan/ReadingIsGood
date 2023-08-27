using System.Diagnostics;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReadingIsGood.Api.Controllers;
using ReadingIsGood.Application.Commands.Book;
using ReadingIsGood.Application.Queries;
using ReadingIsGood.Application.Responses;

namespace tests.ApiTests
{
    public class BookControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private BookController _controller;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new BookController(_mediatorMock.Object);
        }

        [Test]
        public async Task GetAllBooks_ShouldReturnListOfBooks()
        {
            // Arrange
            var books = new List<BookResponse>
            {
                new BookResponse { Id = 1, Title = "Book 1" },
                new BookResponse { Id = 2, Title = "Book 2" }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBooksQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(books);

            // Act
            var result = await _controller.GetAllBooks();

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);

            var okResult = (OkObjectResult)result.Result!;
            Debug.Assert(okResult != null, nameof(okResult) + " != null");
            Assert.That(okResult.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));

            var returnedBooks = (IEnumerable<BookResponse>)okResult.Value!;
            var bookResponses = returnedBooks as BookResponse[] ?? returnedBooks.ToArray();
            Assert.That(bookResponses.Count(), Is.EqualTo(books.Count));

            foreach (var book in books)
            {
                var returnedBook = bookResponses.FirstOrDefault(b => b.Id == book.Id);
                Assert.NotNull(returnedBook);
                Assert.That(returnedBook!.Title, Is.EqualTo(book.Title));
            }
        }

        [Test]
        public async Task BookCreate_ValidData_ShouldReturnOkResult()
        {
            // Arrange
            var command = new BookCreateCommand
            {
                Title = "New Book",
                StockQuantity = 10,
                BookPrice = 29.99m
            };
            var response = new BookResponse { Id = 1, Title = "New Book" };

            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(response);

            // Act
            var result = await _controller.BookCreate(command);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);

            var okResult = (BadRequestObjectResult)result.Result!;
            Assert.That(okResult.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task BookCreate_InvalidData_ShouldReturnBadRequestWithErrorMessage()
        {
            // Arrange
            var command = new BookCreateCommand(); // Invalid data
            var errorResponse = new BookResponse { IsSuccessful = false, Error = "Invalid data" };

            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(errorResponse);

            // Act
            var result = await _controller.BookCreate(command);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);

            var badRequestResult = (BadRequestObjectResult)result.Result!;
            Assert.That(badRequestResult.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));

            var returnedError = (string)badRequestResult.Value!;
            Assert.That(returnedError, Is.EqualTo(errorResponse.Error));
        }

        [Test]
        public async Task BookUpdate_ValidData_ShouldReturnOkResult()
        {
            // Arrange
            var command = new BookUpdateCommand
            {
                BookId = 1,
                StockQuantity = 20
            };
            var response = new BookResponse { Id = 1, StockQuantity = 20 };

            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(response);

            // Act
            var result = await _controller.BookUpdate(command);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);

            var badResult = (BadRequestObjectResult)result.Result!;
            Assert.That(badResult.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task BookUpdate_InvalidData_ShouldReturnBadRequestWithErrorMessage()
        {
            // Arrange
            var command = new BookUpdateCommand(); // Invalid data
            var errorResponse = new BookResponse { IsSuccessful = false, Error = "Invalid data" };

            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(errorResponse);

            // Act
            var result = await _controller.BookUpdate(command);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);

            var badRequestResult = (BadRequestObjectResult)result.Result!;
            Assert.That(badRequestResult?.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));

            var returnedError = (string)badRequestResult?.Value!;
            Assert.That(returnedError, Is.EqualTo(errorResponse.Error));
        }
    }
}