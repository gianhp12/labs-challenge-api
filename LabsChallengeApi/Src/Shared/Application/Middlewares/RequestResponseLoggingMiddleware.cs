using System.Diagnostics;
using LabsChallengeApi.Src.Shared.Application.Exceptions;
using LabsChallengeApi.Src.Shared.Infrastructure.Logger;
using Newtonsoft.Json;

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
                ElapsedMilliseconds = watch.ElapsedMilliseconds
            });
        }
        catch (ValidationException ex)
        {
            watch.Stop();
            await HandleExceptionAsync(context, 400, "VALIDATION_ERROR", ex.Message, traceId);
            LogError(ex, context, traceId, watch.ElapsedMilliseconds);
        }
        catch (NotFoundException ex)
        {
            watch.Stop();
            await HandleExceptionAsync(context, 404, "NOTFOUND_ERROR", ex.Message, traceId);
            LogError(ex, context, traceId, watch.ElapsedMilliseconds);
        }
        catch (Exception ex)
        {
            watch.Stop();
            await HandleExceptionAsync(context, 500, "INTERNAL_SERVER_ERROR", ex.Message, traceId);
            LogError(ex, context, traceId, watch.ElapsedMilliseconds);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, int statusCode, string errorCode, string message, string traceId)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        var response = new
        {
            Error = errorCode,
            Message = message,
            StatusCode = statusCode,
            TraceId = traceId
        };
        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }

    private void LogError(Exception ex, HttpContext context, string traceId, long elapsedMilliseconds)
    {
        _logger.LogError("HTTP Request Failed", ex, new
        {
            TraceId = traceId,
            RequestMethod = context.Request.Method,
            RequestPath = context.Request.Path,
            RequestQueryString = context.Request.QueryString.ToString(),
            RequestHeaders = context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()),
            ResponseStatusCode = context.Response.StatusCode,
            ElapsedMilliseconds = elapsedMilliseconds,
            ExceptionMessage = ex.Message,
            ExceptionStackTrace = ex.StackTrace
        });
    }
}



