﻿namespace CustomTicketStore.Shared.Abstractions.CQRS.QueryHandling;

using MediatR;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>;
