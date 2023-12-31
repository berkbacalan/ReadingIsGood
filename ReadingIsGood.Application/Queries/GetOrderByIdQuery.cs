﻿using MediatR;
using ReadingIsGood.Application.Responses;

namespace ReadingIsGood.Application.Queries;

public class GetOrderByIdQuery : IRequest<OrderResponse>
{
    public int OrderId { get; set; }

    public GetOrderByIdQuery(int orderId)
    {
        OrderId = orderId;
    }
}