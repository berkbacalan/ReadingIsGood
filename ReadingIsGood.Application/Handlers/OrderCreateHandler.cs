﻿using System.ComponentModel.DataAnnotations;
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

    public OrderCreateHandler(IOrderRepository orderRepository, IBookRepository bookRepository, ICustomerRepository customerRepository, IMapper mapper)
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
                    return new OrderResponse{IsSuccessful = false, Error = "Order Entity could not be mapped."};
                }

                try
                {
                    var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
                }
                catch (Exception e)
                {
                    return new OrderResponse{IsSuccessful = false, Error = "Customer could not found."};
                }

                foreach (var orderItem in request.OrderItems)
                {
                    var book = await _bookRepository.GetByIdAsync(orderItem.BookId);
                    if (book.StockQuantity < orderItem.Quantity)
                    {
                        return new OrderResponse{IsSuccessful = false, Error = "Not enough stocks were found."};
                    }
                }

                var order = await _orderRepository.AddAsync(orderEntity);

                foreach (var orderItem in request.OrderItems)
                {
                    var book = await _bookRepository.GetByIdAsync(orderItem.BookId);
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
                return new OrderResponse{IsSuccessful = false, Error = $"Error happened during Order creation, Error: {e}"};
            }
        }
    }
}