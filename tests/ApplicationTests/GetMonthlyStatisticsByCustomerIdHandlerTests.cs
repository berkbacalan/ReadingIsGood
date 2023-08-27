using AutoMapper;
using Moq;
using ReadingIsGood.Application.Handlers;
using ReadingIsGood.Application.Queries;
using ReadingIsGood.Application.Responses;
using ReadingIsGood.Domain.Entities;
using ReadingIsGood.Domain.Repositories;

namespace ReadingIsGood.Tests.ApplicationTests.Handlers
{
    [SetCulture("en-EN")]
    [TestFixture]
    public class GetMonthlyStatisticsByCustomerIdHandlerTests
    {
        private Mock<IOrderRepository> _orderRepositoryMock;
        private Mock<IMapper> _mapper;
        private GetMonthlyStatisticsByCustomerIdHandler _handler;

        [SetUp]
        public void Setup()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _mapper = new Mock<IMapper>();
            _handler = new GetMonthlyStatisticsByCustomerIdHandler(_orderRepositoryMock.Object, _mapper.Object);
        }

        [Test]
        public async Task Handle_WithValidQuery_ShouldReturnStatistics()
        {
            // Arrange
            var customerId = 1;
            var orders = new List<Order>
            {
                new Order
                {
                    TotalAmount = 100,
                    OrderItems = new List<OrderItem> { new OrderItem { Quantity = 2 } }
                },
                new Order
                {
                    TotalAmount = 50,
                    OrderItems = new List<OrderItem> { new OrderItem { Quantity = 1 } }
                }
            };

            _orderRepositoryMock.Setup(repo => repo.GetOrdersByCustomerId(customerId)).ReturnsAsync(orders);

            var expectedResponse = new List<CustomerStatisticsResponse>
            {
                new CustomerStatisticsResponse
                {
                    Year = 2023,
                    Month = "August",
                    TotalOrderCount = 2,
                    TotalBookCount = 3,
                    TotalPurchasedAmount = 150
                }
            };

            var query = new GetMonthlyStatisticsByCustomerIdQuery(customerId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            var customerStatisticsResponses = result as CustomerStatisticsResponse[] ?? result.ToArray();
            Assert.IsNotNull(result);
            Assert.That(customerStatisticsResponses.Count(), Is.EqualTo(expectedResponse.Count));
            Assert.That(customerStatisticsResponses.First().Year, Is.EqualTo(expectedResponse.First().Year));
            Assert.That(customerStatisticsResponses.First().Month, Is.EqualTo(expectedResponse.First().Month));
            Assert.That(customerStatisticsResponses.First().TotalOrderCount, Is.EqualTo(expectedResponse.First().TotalOrderCount));
            Assert.That(customerStatisticsResponses.First().TotalBookCount, Is.EqualTo(expectedResponse.First().TotalBookCount));
            Assert.That(customerStatisticsResponses.First().TotalPurchasedAmount, Is.EqualTo(expectedResponse.First().TotalPurchasedAmount));
        }
    }
}
