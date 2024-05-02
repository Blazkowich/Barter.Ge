﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace Barter.Ge.Api.Bootstrapping.Middleware;

public class GlobalExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionHandlingMiddleware> logger,
    GlobalExceptionHandler globalExceptionHandler)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger = logger;
    private readonly GlobalExceptionHandler _globalExceptionHandler = globalExceptionHandler;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error occurred");
            await _globalExceptionHandler.TryHandleAsync(context, exception, context.RequestAborted);
        }
    }
}
