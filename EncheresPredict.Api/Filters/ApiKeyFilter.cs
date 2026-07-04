using EncheresPredict.Application.Common.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace EncheresPredict.Api.Filters;

public sealed class ApiKeyFilter(
    IOptions<InternalApiOptions> options)
    : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(
                "X-Api-Key",
                out var apiKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (apiKey != options.Value.ApiKey)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        await next();
    }
}