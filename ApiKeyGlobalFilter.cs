using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ApiKeyGlobalFilter : IActionFilter
{
    private const string ApiKeyHeaderName = "X-API-KEY";

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var configApiKey = Environment.GetEnvironmentVariable("API_KEY");
        if (string.IsNullOrEmpty(configApiKey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 500,
                Content = "API_KEY não configurada no ambiente."
            };
            return;
        }

        if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var providedApiKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!configApiKey.Equals(providedApiKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        
    }
}
