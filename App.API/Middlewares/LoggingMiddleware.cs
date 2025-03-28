namespace App.API.Middlewares;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        // 🟢 Gelen HTTP isteğini logla
        _logger.LogInformation($"[Request] {context.Request.Method} {context.Request.Path} from {context.Connection.RemoteIpAddress}");

        // 🟡 İstek gövdesini oku (Opsiyonel)
        if (context.Request.ContentLength > 0 && context.Request.Body.CanSeek)
        {
            context.Request.Body.Position = 0;
            using var reader = new StreamReader(context.Request.Body);
            var requestBody = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;
            _logger.LogInformation($"[Request Body] {requestBody}");
        }

        // ⚡ Sonraki Middleware'e geç
        await _next(context);

        stopwatch.Stop();

        // 🔴 Yanıtı logla
        _logger.LogInformation($"[Response] {context.Response.StatusCode} ({stopwatch.ElapsedMilliseconds} ms)");
    }
}