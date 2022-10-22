namespace AbyssiniaPocketLaw.API.DTOs;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;

public class ApiResponse : ResponseDto<object>
{
    public ApiResponse(object? data=null,int count=0, int? statusCode = StatusCodes.Status200OK, string? error = null)
        : base(data, count, statusCode, error)
    {

    }

}
public class ResponseDto<T> : IActionResult, IDisposable, IStatusCodeActionResult
{
    public T? Data { get; set; }
    public int Count { get; set; }
    public bool IsSuccess { get; set; } = true;
    public int? StatusCode {get; set; }
    public string? Error { get; set; }

    public ResponseDto(T? data,int count=0, int? statusCode = StatusCodes.Status200OK, string? error = null)
    {
        Data = data;
        Count = count;
        StatusCode = statusCode;
        Error = error;
    }

    public async Task ExecuteResultAsync(ActionContext context) =>
        await new OkObjectResult(this).ExecuteResultAsync(context);
    public void Dispose()
    {
        if (Data != null && typeof(T).GetInterfaces().Contains(typeof(IDisposable)))
        {
            ((IDisposable)Data).Dispose();
        }
    }

}
