using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NZWalks.API.CustomActionFilters;

public class LogResultActionFilter : IAsyncActionFilter
{
    private readonly ILogger<LogResultActionFilter> _logger;
    private const int MaxLoggedLength = 2000; // prevent huge payloads in log

    public LogResultActionFilter(ILogger<LogResultActionFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var executed = await next();

        if (executed.Exception != null && !executed.ExceptionHandled)
        {
            _logger.LogError(executed.Exception, "Action {ActionName} threw an exception", context.ActionDescriptor.DisplayName);
            return;
        }

        object? value = null;
        int? statusCode = null;

        switch (executed.Result)
        {
            case ObjectResult orResult:
                value = orResult.Value;
                statusCode = orResult.StatusCode ?? context.HttpContext.Response.StatusCode;
                break;
            case JsonResult jsonResult:
                value = jsonResult.Value;
                statusCode = jsonResult.StatusCode ?? context.HttpContext.Response.StatusCode;
                break;
            case ContentResult contentResult:
                value = contentResult.Content;
                statusCode = contentResult.StatusCode ?? context.HttpContext.Response.StatusCode;
                break;
            case StatusCodeResult scResult:
                statusCode = scResult.StatusCode;
                break;
        }

        if (statusCode == null)
        {
            statusCode = context.HttpContext.Response.StatusCode;
        }

        string? serialized = null;
        if (value != null)
        {
            try
            {
                serialized = JsonSerializer.Serialize(value, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = false,
                    MaxDepth = 6
                });
                if (serialized.Length > MaxLoggedLength)
                {
                    serialized = serialized.Substring(0, MaxLoggedLength) + "... (truncated)";
                }
            }
            catch (Exception ex)
            {
                serialized = $"<Serialization failed: {ex.Message}>";
            }
        }

        _logger.LogInformation("Action {ActionName} completed with {StatusCode}. Result={Result}", context.ActionDescriptor.DisplayName, statusCode, serialized ?? "<no body>");
    }
}
