namespace AbyssiniaPocketLaw.API.Middlewares;

using AbyssiniaPocketLaw.API.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;

public class ExceptionHandler 
{
    private readonly RequestDelegate _next;
    public ExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var errorResponse = new ApiResponse
        {
            IsSuccess = false,
            Data = null,
            Count = 0,
            Error = exception.Message
    };
        switch (exception)
        {
            case ApplicationException ex:
                if (ex.Message.Contains("Invalid Token"))
                {
                    errorResponse.StatusCode = (int)HttpStatusCode.Forbidden; 
                    break;
                }
                errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            default:
                errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }
        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        var result = JsonSerializer.Serialize(errorResponse, serializeOptions);
        await context.Response.WriteAsync(result);
    }
}

