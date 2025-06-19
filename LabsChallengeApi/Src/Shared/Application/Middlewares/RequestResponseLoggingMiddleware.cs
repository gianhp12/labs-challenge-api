using System.Diagnostics;
using LabsChallengeApi.Src.Shared.Infrastructure.Logger;


namespace LabsChallengeApi.Src.Shared.Application.Middlewares;

public class RequestLoggingMiddleware : IMiddleware
{
    private readonly ILoggerService _logger;

    public RequestLoggingMiddleware(ILoggerService logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var watch = Stopwatch.StartNew();
        var traceId = context.TraceIdentifier;
        try
        {
            await next(context);
            watch.Stop();
            _logger.LogInformation("HTTP Request Completed", new
            {
                TraceId = traceId,
                RequestMethod = context.Request.Method,
                RequestPath = context.Request.Path,
                RequestQueryString = context.Request.QueryString.ToString(),
                RequestHeaders = context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()),
                ResponseStatusCode = context.Response.StatusCode,
                watch.ElapsedMilliseconds
            });
        }
        catch (Exception ex)
        {
            watch.Stop();
            _logger.LogError("HTTP Request Failed", ex, new
            {
                TraceId = traceId,
                RequestMethod = context.Request.Method,
                RequestPath = context.Request.Path,
                RequestQueryString = context.Request.QueryString.ToString(),
                RequestHeaders = context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()),
                ResponseStatusCode = context.Response.StatusCode,
                watch.ElapsedMilliseconds,
                ExceptionMessage = ex.Message,
                ExceptionStackTrace = ex.StackTrace
            });
            throw;
        }
    }
}


