﻿using MediatR;

namespace TodoApp.Core.Contexts.AccountContext.UseCases.Create;
public record Request(
    string Name,
    string Email,
    string Password
) : IRequest<Response>;