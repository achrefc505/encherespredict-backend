using EncheresPredict.Domain.Exceptions;
using FluentValidation;
using System.Text.Json;

namespace EncheresPredict.Api.Middleware;

public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext ctx)
    {
        try
        {
            await next(ctx);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur non gérée : {Message}", ex.Message);
            await HandleExceptionAsync(ctx, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext ctx, Exception ex)
    {
        ctx.Response.ContentType = "application/json";

        var (status, message) = ex switch
        {
            AuctionNotFoundException => (StatusCodes.Status404NotFound, ex.Message),
            KeyNotFoundException     => (StatusCodes.Status404NotFound, ex.Message),
            ValidationException ve  => (StatusCodes.Status422UnprocessableEntity,
                string.Join("; ", ve.Errors.Select(e => e.ErrorMessage))),
            ArgumentException       => (StatusCodes.Status400BadRequest, ex.Message),
            _                       => (StatusCodes.Status500InternalServerError, "Une erreur interne est survenue.")
        };

        ctx.Response.StatusCode = status;
        await ctx.Response.WriteAsync(JsonSerializer.Serialize(new { erreur = message }));
    }
}
