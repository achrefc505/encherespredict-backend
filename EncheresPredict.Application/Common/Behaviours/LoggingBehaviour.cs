using MediatR;
using Microsoft.Extensions.Logging;

namespace EncheresPredict.Application.Common.Behaviours;

public sealed class LoggingBehaviour<TRequest, TResponse>(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        var name = typeof(TRequest).Name;
        logger.LogInformation("Traitement de la requête {Name}", name);
        var result = await next();
        logger.LogInformation("Requête {Name} traitée avec succès", name);
        return result;
    }
}
