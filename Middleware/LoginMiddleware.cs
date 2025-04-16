using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Diagnostics;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        await _next(context);
        stopwatch.Stop();
        Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path} - {context.Response.StatusCode} - {stopwatch.ElapsedMilliseconds}ms");
    }
}
