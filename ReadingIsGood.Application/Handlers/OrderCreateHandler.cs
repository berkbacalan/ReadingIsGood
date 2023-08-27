using System.ComponentModel.DataAnnotations;
using System.Transactions;
using AutoMapper;
using MediatR;
using ReadingIsGood.Application.Commands.OrderCreate;
using ReadingIsGood.Application.Responses;
using ReadingIsGood.Domain.Entities;
using ReadingIsGood.Domain.Repositories;

namespace ReadingIsGood.Application.Handlers;

public class OrderCreateHandler : IRequestHandler<OrderCreateCommand, OrderResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IBookRepository _bookRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public OrderCreateHandler(IOrderRepository orderRepository, IBookRepository bookRepository,
        ICustomerRepository customerRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _bookRepository = bookRepository;
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<OrderResponse> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
    {
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            try
            {
                var orderEntity = _mapper.Map<Order>(request);
                if (orderEntity == null)
                {
                    return new OrderResponse { IsSuccessful = false, Error = "Order Entity could not be mapped." };
                }


                var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
                if (customer is null)
                {
                    return new OrderResponse { IsSuccessful = false, Error = "Customer could not found." };
                }

                double totalAmount = 0;
                foreach (var orderItem in request.OrderItems)
                {
                    if (orderItem.Quantity <= 0)
                    {
                        return new OrderResponse
                        {
                            IsSuccessful = false,
                            Error = "Invalid operation. Quantity should be greater than 0 for all items."
                        };
                    }
                    var book = await _bookRepository.GetByIdAsync(orderItem.BookId);
                    if (book is null)
                    {
                        return new OrderResponse { IsSuccessful = false, Error = $"Could not find book with id: {orderItem.BookId}" };
                    }
                    if (book.StockQuantity < orderItem.Quantity)
                    {
                        return new OrderResponse { IsSuccessful = false, Error = $"Not enough stocks were found for book id: {orderItem.BookId}." };
                    }

                    totalAmount += orderItem.Quantity * book.BookPrice;
                }

                if (totalAmount <= 0)
                {
                    return new OrderResponse
                        { IsSuccessful = false, Error = "Total Order amount could not be equal or less than 0." };
                }

                orderEntity.TotalAmount = totalAmount;
                var order = await _orderRepository.AddAsync(orderEntity);

                foreach (var orderItem in request.OrderItems)
                {
                    var book = await _bookRepository.GetByIdAsync(orderItem.BookId);
                    if (book is null)
                    {
                        return new OrderResponse { IsSuccessful = false, Error = $"Could not find book with id: {orderItem.BookId}" };
                    }
                    book.StockQuantity -= orderItem.Quantity;
                    await _bookRepository.UpdateAsync(book);
                }

                scope.Complete();

                var orderResponse = _mapper.Map<OrderResponse>(order);
                orderResponse.IsSuccessful = true;
                return orderResponse;
            }
            catch (Exception e)
            {
                scope.Dispose();
                return new OrderResponse
                    { IsSuccessful = false, Error = $"Error happened during Order creation, Error: {e}" };
            }
        }
    }
}