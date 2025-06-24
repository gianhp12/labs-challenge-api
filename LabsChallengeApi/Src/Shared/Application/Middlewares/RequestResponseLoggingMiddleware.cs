using System.Diagnostics;
using System.Text;
using LabsChallengeApi.Src.Shared.Application.Exceptions;
using LabsChallengeApi.Src.Shared.Infrastructure.Logger;
using Newtonsoft.Json;

namespace LabsChallengeApi.Src.Shared.Application.Middlewares;

public class RequestLoggingMiddleware : IMiddleware
{
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(ILogger<RequestLoggingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var watch = Stopwatch.StartNew();
        var traceId = context.TraceIdentifier;
        var requestBody = await ReadRequestBodyAsync(context);
        var originalBodyStream = context.Response.Body;
        await using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;
        try
        {
            await next(context);
            watch.Stop();
            var responseText = await ReadResponseBodyAsync(responseBody);
            _logger.LogInformation(@"
                HTTP Request Completed
                TraceId: {TraceId}
                Method: {Method}
                Path: {Path}
                QueryString: {QueryString}
                StatusCode: {StatusCode}
                ElapsedMilliseconds: {ElapsedMilliseconds}ms
                RequestBody: {RequestBody}
                ResponseBody: {ResponseBody}",
                    traceId,
                    context.Request.Method,
                    context.Request.Path,
                    context.Request.QueryString.ToString(),
                    context.Response.StatusCode,
                    watch.ElapsedMilliseconds,
                    requestBody,
                    responseText);

            await CopyResponseBodyAsync(responseBody, originalBodyStream);
        }
        catch (Exception ex)
        {
            watch.Stop();
            context.Response.Clear();
            context.Response.StatusCode = MapExceptionToStatusCode(ex);
            context.Response.ContentType = "application/json";
            var errorResponse = new
            {
                Message = ex.Message,
                StatusCode = context.Response.StatusCode,
                TraceId = traceId
            };
            var responseJson = JsonConvert.SerializeObject(errorResponse);
            await context.Response.WriteAsync(responseJson);
            _logger.LogError(ex, @"
                HTTP Request Failed
                TraceId: {TraceId}
                Method: {Method}
                Path: {Path}
                QueryString: {QueryString}
                StatusCode: {StatusCode}
                ElapsedMilliseconds: {ElapsedMilliseconds}ms
                RequestBody: {RequestBody}
                RequestHeaders: {RequestHeaders}
                ExceptionMessage: {ExceptionMessage}
                ExceptionStackTrace: {ExceptionStackTrace}",
                    traceId,
                    context.Request.Method,
                    context.Request.Path,
                    context.Request.QueryString.ToString(),
                    context.Response.StatusCode,
                    watch.ElapsedMilliseconds,
                    requestBody,
                    JsonConvert.SerializeObject(context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString())),
                    ex.Message,
                    ex.StackTrace);
            await CopyResponseBodyAsync(responseBody, originalBodyStream);
        }
    }

    private int MapExceptionToStatusCode(Exception ex)
    {
        return ex switch
        {
            ValidationException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };
    }

    private async Task<string> ReadRequestBodyAsync(HttpContext context)
    {
        context.Request.EnableBuffering();
        context.Request.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
        var body = await reader.ReadToEndAsync();
        context.Request.Body.Seek(0, SeekOrigin.Begin);
        return body;
    }

    private async Task<string> ReadResponseBodyAsync(MemoryStream responseBody)
    {
        responseBody.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(responseBody);
        var text = await reader.ReadToEndAsync();
        responseBody.Seek(0, SeekOrigin.Begin);
        return text;
    }

    private async Task CopyResponseBodyAsync(MemoryStream responseBody, Stream originalBodyStream)
    {
        responseBody.Seek(0, SeekOrigin.Begin);
        await responseBody.CopyToAsync(originalBodyStream);
    }
}






